using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;     //��ȡAnimator������ע�ⲻҪд��Animation
    private Rigidbody2D rb;     //��ȡRigidbody2D���������ں����ٶ�ֵ�Ĵ���
    private PhysicsCheck physicsCheck;     //��ȡ��PlayerCheck�еı���
    private PlayerController playerController;      //��ȡ��PlayerController�еı���
    private void Awake()
    {
        anim = GetComponent<Animator>();    //��ʼ��anim���������Animator�б�����ʹ��Ȩ
        rb = GetComponent<Rigidbody2D>();    //��ʼ��rb���������Rigidbody2D�б�����ʹ��Ȩ
        physicsCheck = GetComponent<PhysicsCheck>();    //��ʼ��physicsCheck���������PhysicsCheck�б�����ʹ��Ȩ
        playerController = GetComponent<PlayerController>();    //��ʼ��PlayerController���������PlayerController�б�����ʹ��Ȩ
    }
    private void Update()
    {
        //�ظ����¶�������
        SetAnimation();
    }
    public void SetAnimation()    //��������Player���ж���
    {
        //�������ƶ����ٶȴ��ݸ�Animator�е�float����
        //ʹ��mathf.Abs��������������ʱ���ܴ�������
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        //��������Ծ���ٶȴ��ݸ�Animator�е�float����
        anim.SetFloat("velocityY",rb.velocity.y);
        //����������Ƿ���ص�bool���ݸ�Animator�е�bool����
        anim.SetBool("isGround", physicsCheck.isGround);
        //��playerController��isDead��ֵ���ݸ�Animator�е�bool����
        anim.SetBool("isDead", playerController.isDead);
        //��playerController��isAttack��ֵ���ݸ�Animator�е�bool����
        anim.SetBool("isAttack", playerController.isAttack);
    }
    public void PlayerHurt()        //�������˺�������
    {
        anim.SetTrigger("hurt");
    }
    public void PlayerAttack()      //���﹥����������
    {
        anim.SetTrigger("attack");
    }
}