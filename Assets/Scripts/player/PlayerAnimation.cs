using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;     //获取Animator变量，注意不要写成Animation
    private Rigidbody2D rb;     //获取Rigidbody2D变量，用于后续速度值的传递
    private PhysicsCheck physicsCheck;     //获取类PlayerCheck中的变量
    private PlayerController playerController;      //获取类PlayerController中的变量
    private void Awake()
    {
        anim = GetComponent<Animator>();    //初始化anim变量，获得Animator中变量的使用权
        rb = GetComponent<Rigidbody2D>();    //初始化rb变量，获得Rigidbody2D中变量的使用权
        physicsCheck = GetComponent<PhysicsCheck>();    //初始化physicsCheck变量，获得PhysicsCheck中变量的使用权
        playerController = GetComponent<PlayerController>();    //初始化PlayerController变量，获得PlayerController中变量的使用权
    }
    private void Update()
    {
        //重复更新动画运行
        SetAnimation();
    }
    public void SetAnimation()    //用于运行Player所有动画
    {
        //将人物移动的速度传递给Animator中的float变量
        //使用mathf.Abs让人物在左右跑时都能触发动画
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        //将人物跳跃的速度传递给Animator中的float变量
        anim.SetFloat("velocityY",rb.velocity.y);
        //将检查人物是否落地的bool传递给Animator中的bool变量
        anim.SetBool("isGround", physicsCheck.isGround);
        //将playerController中isDead的值传递给Animator中的bool变量
        anim.SetBool("isDead", playerController.isDead);
        //将playerController中isAttack的值传递给Animator中的bool变量
        anim.SetBool("isAttack", playerController.isAttack);
    }
    public void PlayerHurt()        //人物受伤函数方法
    {
        anim.SetTrigger("hurt");
    }
    public void PlayerAttack()      //人物攻击函数方法
    {
        anim.SetTrigger("attack");
    }
}