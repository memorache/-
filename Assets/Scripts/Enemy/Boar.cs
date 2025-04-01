using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boar : Enemy       //继承于父类Enemy
{
    protected override void Awake()
    {
        base.Awake();
        //为野猪的巡逻进行赋值进行
        patrolState = new BoarPatrolState();
        chaseState = new BoarChaseState();
    }
}
