using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boar : Enemy       //�̳��ڸ���Enemy
{
    protected override void Awake()
    {
        base.Awake();
        //ΪҰ���Ѳ�߽��и�ֵ����
        patrolState = new BoarPatrolState();
        chaseState = new BoarChaseState();
    }
}
