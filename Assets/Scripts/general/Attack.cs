using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("基础攻击数值")]
    public int attack;      //攻击值(不用Damage是因为Damage常用于表示受到的攻击数值根据防御值等的减少后伤害)
    public float attackRange;       //攻击距离
    public float attackRate;        //攻击频率

    private void OnTriggerStay2D(Collider2D other)      //通过检测碰撞箱是否碰撞来确认是否被攻击
    {
        //通过调用Character中受到伤害的方法，将当前受到的伤害attack传递到Character中
        //加入?用于判断被碰撞体上有没有Character组件，如果没有就不执行TakeDamege，防止报错
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
