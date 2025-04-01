using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)       //野猪巡逻状态机的进入，执行传递进来的enemy用法
    {
        //获取当前enemy是谁
        currentEnemy = enemy;
        //让速度在调整回来的时候变回巡逻速度
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }
    public override void LogicUpdate()          //所有野猪巡逻的bool值的判断都会放在这里
    {
        // 发现敌人后切换到chase形态
        if (currentEnemy.FoundPlayer())
        {
            // 确认敌人切换到追击状态后，将chase变量传递给SwitchState
            currentEnemy.SwitchState(NPCState.Chase);
        }
        // 检测敌人是否撞墙或是否在悬崖边上
        // 由于野猪改变方向后会使背后检测框碰到墙壁而导致再次转向，所以让监测点为面朝方向时执行计时器函数
        // isGround用于防止野猪坠崖
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
    public override void OnExit()       //野猪巡逻状态机退出
    {
        currentEnemy.anim.SetBool("walk", false);
    }
}
