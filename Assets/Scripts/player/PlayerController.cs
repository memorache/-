using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;    //调用UnityEngine包中的InputSystem方法

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;     //可以调用InputSystem的代码，C#的每一个类要使用的时候，都需要一个专门的实例（new）去使用它
    public Vector2 inputDirection;     //创建Vector2变量InputDirection，让它在Unity中能被查看到
    public float inputLeftControl;      //创建float变量inputLeftControl，让它在Unity中能被查看到
    private Rigidbody2D rb;           //用于获取Rigidbody2D中的变量
    //private SpriteRenderer sr;       //用于获取SpriteRenderer中的变量
    private PlayerAnimation playerAnimation;        //创建获取playerAnimation中的变量
    private PhysicsCheck physicsCheck;     //创建PhysicsCheck中的变量，用于让人物只跳一次
    [Header("基础参数")]            //可用于分类不同数值的代码
    public float speed;             //控制人物速度的值
    public float jumpForce;         //跳跃力参数
    [Header("受击后退参数")]
    public float hurtForce;         //受伤后导致后退的力
    public bool isHurt;         //检测是否受伤
    [Header("死亡相关参数")]
    public bool isDead;         //检测人物是否死亡
    [Header("攻击相关参数")]
    public bool isAttack;       //检测人物是否按下按键进行攻击
    [Header("音效相关参数")]
    public AudioDefination audioDefination;     // 控制指定播放音效

    private void Awake()     //在start前运行，游戏的最开始，顺序为Awake > OnEnable > Start
    {
        //初始化变量rb，获取Rigidbody2D的使用权，可用变量的方式将Rigidbody2D中所有参数更改
        rb = GetComponent<Rigidbody2D>();
        //初始化变量physicsCheck变量，获取PhysicsCheck的使用权
        physicsCheck = GetComponent<PhysicsCheck>();
        //初始化变量playerAnimation变量，获取PlayerAnimation的使用权
        playerAnimation = GetComponent<PlayerAnimation>();
        //InputControl的实例化
        inputControl = new PlayerInputControl();
        // audioDefination的初始化
        audioDefination = GetComponent<AudioDefination>();
        //事件注册函数+=，表示把这个函数方法添加到按键结束的时候执行，可多写几行用于按键结束时同时执行
        //其中Move有started、canceled、performed，分别代表按键按下的一刻，按键松开的一刻和一直按键
        //这里为了跳跃能够快速响应选择started
        inputControl.GamePlay.Jump.started += Jump;
        //攻击的按键响应事件
        inputControl.GamePlay.Attack.started += PlayerAttack;
        //初始化变量sr，获取SpriteRenderer的使用权，可用变量的方式将Rigidbody2D中所有参数更改,用于实现人物反转
        //sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()     //物体开始时启动
    {
        inputControl.Enable();
    }

    private void OnDisable()    //物体关闭时启动
    {
        inputControl.Disable();
    }

    private void Update()     //启动后每一帧都会执行的函数
    {
        //让变量inputDirection读取到Move中左右移动的数值
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        //让变量inputLeftControl读取到按下Ctrl后的数值
        inputLeftControl = inputControl.GamePlay.Otherkeys.ReadValue<float>();
    }

    private void FixedUpdate()    //以一个固定的时间更新一次
    {
        //如果人物没有受击和在攻击，便可移动
        if(!isHurt && !isAttack)
            Move();             //调用Move函数
    }

    //碰撞体测试
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("collision.name");
    //}

    public void Move()     //自己写的函数，用于让角色左右正常移动起来
    {   
        //人物移动
        //X轴方向速度=Vector2中的X值*speed常量*时间修正（帮助我们在不同电脑上获得相同的效果）；y轴的0会降低下落速度，让其保证原来的速度不变即可
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        //如果按下Ctrl会让速度降低，用于在电脑上的也可以有的走路效果
        if (inputLeftControl != 0)
            rb.velocity /= 2;

        //人物翻转
        //Scale.X方法（常用）
        //在非零情况下，通过判断InputDirection的值的正负来决定人物的朝向
        if (inputDirection.x != 0)
            //使用条件运算符?进行判断
            //因为所以Unity组件中都有Transform值，所以可以直接调用，不需要使用getComponent
            transform.localScale = new Vector3(inputDirection.x < 0 ? -1 : 1, 1, 1);

        //Flip.X方法
        //在非零情况下，通过判断InputDirection的值的正负来决定人物的朝向（注意flipX的值为bool值）
        //if (InputDirection.x != 0)
        //    sr.flipX = InputDirection.x < 0 ? true : false;
    }
    private void PlayerAttack(InputAction.CallbackContext context)      //PlayerAttack的攻击方法
    {
        //攻击时速度减为0
        rb.velocity = Vector2.zero;
        //触发攻击动画
        playerAnimation.PlayerAttack();
        isAttack = true;
    }
    private void Jump(InputAction.CallbackContext context)   //Jump的函数方法
    {
        //Debug方法
        //Debug.Log("JUMP");
        //使用AddForce方法让人物有向上的瞬时力，让人物能够跳起来
        //使用if进行判断
        //这里输入AddForce后会出现函数重载（同名函数有不同方法），这里我们选择第二种，加上Impulse表示瞬时力
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            // 跳起来后播放跳跃音效
            audioDefination.PlayAudioClip();
        }
    }

    //在UnityEvent中执行的函数方法
    public void GetHurt(Transform Attacker)      //在受伤时执行反弹方法
    {
        //受击后受击判定为true
        isHurt = true;
        //受击后x,y速度归零，减去惯性影响
        rb.velocity = Vector2.zero;
        //判断受击后人物与攻击者的位置
        //使用Vector2使变量值为向量，通过人物与攻击者距离的差值决定向量的正负
        //由于人物和攻击者之间距离过远会导致dir值过大而我们只需要一个正负值，所以使用normalized归一化
        Vector2 dir = new Vector2((transform.position.x - Attacker.position.x), 0).normalized;
        //根据向量方向施加力
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }
    public void PlayerDead()        //死亡时禁止操作方法
    {
        //判断死亡
        isDead = true;
        //此时禁止玩家操作，但是UI东西会保留
        inputControl.GamePlay.Disable();
    }
}