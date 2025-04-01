using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)       //Ұ��Ѳ��״̬���Ľ��룬ִ�д��ݽ�����enemy�÷�
    {
        //��ȡ��ǰenemy��˭
        currentEnemy = enemy;
        //���ٶ��ڵ���������ʱ����Ѳ���ٶ�
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }
    public override void LogicUpdate()          //����Ұ��Ѳ�ߵ�boolֵ���ж϶����������
    {
        // ���ֵ��˺��л���chase��̬
        if (currentEnemy.FoundPlayer())
        {
            // ȷ�ϵ����л���׷��״̬�󣬽�chase�������ݸ�SwitchState
            currentEnemy.SwitchState(NPCState.Chase);
        }
        // �������Ƿ�ײǽ���Ƿ������±���
        // ����Ұ��ı䷽����ʹ�����������ǽ�ڶ������ٴ�ת�������ü���Ϊ�泯����ʱִ�м�ʱ������
        // isGround���ڷ�ֹҰ��׹��
        if (!currentEnemy.physicsCheck.isGround || currentEnemy.physicsCheck.touchWall)
        {
            currentEnemy.waitTime = true;
            currentEnemy.anim.SetBool("walk", false);
        }
        else
        {
            currentEnemy.anim.SetBool("walk", true);
        }
    }
    public override void PhysicsUpdate()
    {

    }
    public override void OnExit()       //Ұ��Ѳ��״̬���˳�
    {
        currentEnemy.anim.SetBool("walk", false);
    }
}
