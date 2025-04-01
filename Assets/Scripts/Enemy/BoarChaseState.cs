using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        //�����ٶ�Ϊ׷���ٶ�
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        //��Ұ��������!!!
        currentEnemy.anim.SetBool("run", true);
    }
    public override void LogicUpdate()
    {
        //�����ʱ�����㣬���л���Ѳ��״̬
        if (currentEnemy.chaseTimeCounter <= 0)
            currentEnemy.SwitchState(NPCState.Patrol);
        // �������Ƿ�ײǽ���Ƿ������±���
        // ����Ұ��ı䷽����ʹ�����������ǽ�ڶ������ٴ�ת�������ü���Ϊ�泯����ʱִ�м�ʱ������
        // isGround���ڷ�ֹҰ��׹��
        if (!currentEnemy.physicsCheck.isGround || currentEnemy.physicsCheck.touchWall)
        {
            //��Ұ����ײǽ���º�ת��
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
        }
    }
    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        //��׷��ʱ�����ʱ�������ܲ�״̬,�л���Ѳ��״̬
        currentEnemy.anim.SetBool("run", false);
    }
}
