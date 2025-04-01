using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        //设置速度为追击速度
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        //让野猪跑起来!!!
        currentEnemy.anim.SetBool("run", true);
    }
    public override void LogicUpdate()
    {
        //如果计时器归零，就切换回巡逻状态
        if (currentEnemy.chaseTimeCounter <= 0)
            currentEnemy.SwitchState(NPCState.Patrol);
        // 检测敌人是否撞墙或是否在悬崖边上
        // 由于野猪改变方向后会使背后检测框碰到墙壁而导致再次转向，所以让监测点为面朝方向时执行计时器函数
        // isGround用于防止野猪坠崖
        if (!currentEnemy.physicsCheck.isGround || currentEnemy.physicsCheck.touchWall)
        {
            //让野猪在撞墙或靠崖后转身
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
        }
    }
    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        //当追击时间结束时，返回跑步状态,切换回巡逻状态
        currentEnemy.anim.SetBool("run", false);
    }
}
