using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;     //声明类Rigidbody2D的变量rb
    [HideInInspector]public Animator anim;      //声明类Animator的变量anim,protected让其只能在父类中被访问
    [HideInInspector]public PhysicsCheck physicsCheck;      //声明类PhysicsCheck的变量physicsCheck
    [Header("基础参数")]
    public float normalSpeed;       //正常速度
    public float chaseSpeed;        //追击速度
    public float currentSpeed;      //当前速度
    public Vector3 faceDir;         //面朝方向
    public Transform attacker;      //记录攻击者
    [Header("检测玩家")]
    public Vector2 centerOffSet;        //检测框中心位置偏移
    public Vector2 checkSize;           //检测框大小
    public float checkDistance;         //检测距离
    public LayerMask attackLayer;       //指定检测某个图层
    [Header("撞墙等待计时器")]
    public float waitTimeDuration;      // 撞墙等待时间
    public float waitTimeCounter;       // 撞墙等待计时器
    public bool waitTime;       // 是否撞墙
    [Header("追击丢失目标计时器")]
    public float chaseTimeDuration;     //追击持续时间
    public float chaseTimeCounter;      //追击时间计时器
    [Header("受击后退参数")]
    public bool isHurt;     //检测是否被攻击
    public float hurtForce;     //受击获得的推力
    [Header("死亡相关参数")]
    public bool isDead;     //检测是否死亡
    public PickupSpawner pickupSpawner;     // 怪物死亡后的脚本调用

    private BaseState currentState;         //当前抽象类状态
    protected BaseState patrolState;        //巡逻抽象类状态
    protected BaseState chaseState;         //追击抽象类状态

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();       //初始化变量rb
        anim = GetComponent<Animator>();        //初始化变量anim
        physicsCheck = GetComponent<PhysicsCheck>();        //初始化变量physicsCheck
        pickupSpawner = GetComponent<PickupSpawner>();      // 初始化变量pickupSpawner
        currentSpeed = normalSpeed;             //开始时速度变量为正常速度
        waitTimeCounter = waitTimeDuration;     //让撞墙等待计时器改为撞墙等待时间，用于第一次撞墙检测
        chaseTimeCounter = chaseTimeDuration;   //让追击时间计时器等于追击持续时间，用于第一次追击检测
    }
    private void OnEnable()         //启用
    {
        //让当前状态进入巡逻状态
        currentState = patrolState;
        //将当前挂载的野猪类传递进去，巡逻状态所需要的都会在这里启用
        currentState.OnEnter(this);
    }
    public void Update()
    {
        // 根据敌人面朝方面实时更改贴图面朝方向
        // 由于敌人Scale.x为正时面朝左边，所以需要在x值上加一个负号来改变朝向
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        // 巡逻状态当前所需要的逻辑变量在Update中更新
        currentState.LogicUpdate();
        // 根据状态机状态选择进入等待时间还是冲撞计时状态
        TimeCounter();
    }
    public void FixedUpdate()
    {
        //如果没有受伤、没有寄掉也没有快掉到悬崖，执行Move函数
        if (!isHurt && !isDead && !waitTime)
            Move();
        //逻辑状态当前所需要的物理逻辑都在FixedUpdate更新
        currentState.PhysicsUpdate();
    }
    private void OnDisable()        //关闭
    {
        //物体消失时退出所有巡逻状态
        currentState.OnExit();
    }
    public virtual void Move()      //virtual用于子类在访问时复写父类函数
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }
    public void TimeCounter()      //等待时间计时器
    {
        //等待时间计时器
        if (waitTime)
        {
            //开始计时等待
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                waitTime = false;
                //让waitTimeCounter恢复，以应对下一次撞墙等待
                waitTimeCounter = waitTimeDuration;
                //野猪撞墙后改变面朝方向
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }
        //追击时间计时器
        if (!FoundPlayer())
        {
            //防止时间减少到0
            if(chaseTimeCounter > 0)
                //开始追击时间倒计时
                chaseTimeCounter -= Time.deltaTime;
        }
        else
        {
            chaseTimeCounter = chaseTimeDuration;
        }
    }
    #region 受击和死亡事件执行方法
    public void OnTakeDamage(Transform AttackTrans)         //受伤后执行转身和反弹
    {
        //让撞墙等待不执行，防止被攻击转身后倒计时结束再次转身
        waitTime = false;
        waitTimeCounter = waitTimeDuration;
        //根据攻击者方向改变敌人方向
        attacker = AttackTrans;
        if (attacker.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(1, 1, 1); ;
        if (attacker.position.x - transform.position.x > 0)
            transform.localScale = new Vector3(-1, 1, 1); ;
        //敌人受伤反弹
        isHurt = true;
        anim.SetTrigger("hurt");
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - AttackTrans.position.x), 0).normalized;
        //启用协程
        StartCoroutine(OnHurt(dir));
    }
    private IEnumerator OnHurt(Vector2 dir)        //迭代器，可以让代码隔一段时间后执行，IEnumerator和int，void一样属于返回类型
    {
        rb.AddForce(hurtForce * dir, ForceMode2D.Impulse);
        //如果希望不返回什么东西时，就让返回值为null,意思为等待一帧后执行后面的代码
        //如果想等待其它时间的话，就new一下选一个，这里选WaitForSeconds用于等待秒数
        //等待的秒数为帧数*采样率*0.01，野猪的受伤动画为4帧，采样率为8，所以等待0.32s，当然也可以设置大一些
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }
    public void OnDie()       //敌人死亡方法
    {
        // 播放死亡动画
        anim.SetTrigger("dead");
        isDead = true;
        // 掉落身上挂载的物品
        pickupSpawner.DropItems();
    }
    public void OnDestroyAfterAnimation()       //用于死亡后清除敌人
    {
        //destroy函数用于清除搭在该函数的物体
        Destroy(this.gameObject);
    }
    #endregion
    public bool FoundPlayer()       //发现玩家的函数
    {
        //BoxCast值分别为中心点，检测框大小，盒体的角度，检测矢量方向（面朝方向），检测投射距离，检测图层
        //BoxCast返回值为RaycastHit2D，其中的collider属性可以检测是否发生了碰撞并返回bool值，可通过此值来判断是否发生碰撞
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffSet, checkSize, 0, faceDir, checkDistance, attackLayer);
    }

    public void SwitchState(NPCState state)       //使用枚举切换不同状态
    {
        //switch语法糖写法，可用于简单的变量切换
        //当检测到传进来的变量值是谁后，就将箭头后面的值传递给newState
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null
        };
        //传递成功后，退出current之前的状态，导入新状态，并让其执行
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    public void OnDrawGizmosSelected()      //绘制检测玩家的框线
    {
        //使用Gizmos画线，确定碰撞体的大致位置
        //不要使用矩形检测框，会变得不幸
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffSet + new Vector3(checkDistance * -transform.localScale.x, 0), 0.2f);
    }
}