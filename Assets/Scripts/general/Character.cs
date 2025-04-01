using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;       //调用Unity事件

public class Character : MonoBehaviour
{
    [Header("基础属性")]
    public float maxHealth;      //最大血量
    public float currentHealth;     //当前血量
    [Header("受伤无敌帧计数器")]
    public float invulnerableDuration;      //无敌时间
    private float invulnerableCounter;      //无敌时间计数器
    public bool invulnerable = false;      //是否无敌
    [Header("事件")]
    public UnityEvent<Character> OnHealthChange;    // 创建事件，血量变化时传递参数
    public UnityEvent<Transform> OnTakeDamage;      //创建OnTakeDamage事件表示受到伤害时会触发的方法，当事件被执行的时候，会执行其中添加的所有方法
    public UnityEvent Ondie;        //创建Ondie事件表示死亡后会触发的方法
    private void Start()
    {
        //游戏开始满血
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }
    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
                invulnerable = false;
        }
        
    }
    private void OnTriggerStay2D(Collider2D collider)        // 用于检测和水面的碰撞
    {
        if(collider.CompareTag("water"))
        {
            // 血量清零
            currentHealth = 0;
            // 更新血条
            OnHealthChange?.Invoke(this);
            // 执行死亡事件
            Ondie?.Invoke();
        }
    }
    public void TakeDamage(Attack attacker)       //撰写受到伤害的方法，创建一个Attack类的attacker值接收Attack传递过来的变量
    {
        //如果处于无敌时间，就不执行代码
        if (invulnerable)
            return;
        //测试受到的伤害是多少
        //Debug.Log(attacker.attack);
        //判断受伤后还有血量没
        if(currentHealth - attacker.attack > 0)
        {
            //受伤减少生命值
            currentHealth -= attacker.attack;
            //进入无敌时间
            TriggerInvulnerable();
            //启用事件，？确定其中有方法后启动受伤动画
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            //人物死亡
            currentHealth = 0;
            //确认死亡后启用事件，？确定其中有方法后启动死亡动画
            Ondie?.Invoke();
        }
        // 将受伤和死亡后的人物数据跟随事件一起传递
        OnHealthChange?.Invoke(this);
    }

    public void TriggerInvulnerable()       //无敌时间触发函数
    {
        invulnerable = true;
        invulnerableCounter = invulnerableDuration;
    }
}
