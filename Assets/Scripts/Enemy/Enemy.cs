using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;     //������Rigidbody2D�ı���rb
    [HideInInspector]public Animator anim;      //������Animator�ı���anim,protected����ֻ���ڸ����б�����
    [HideInInspector]public PhysicsCheck physicsCheck;      //������PhysicsCheck�ı���physicsCheck
    [Header("��������")]
    public float normalSpeed;       //�����ٶ�
    public float chaseSpeed;        //׷���ٶ�
    public float currentSpeed;      //��ǰ�ٶ�
    public Vector3 faceDir;         //�泯����
    public Transform attacker;      //��¼������
    [Header("������")]
    public Vector2 centerOffSet;        //��������λ��ƫ��
    public Vector2 checkSize;           //�����С
    public float checkDistance;         //������
    public LayerMask attackLayer;       //ָ�����ĳ��ͼ��
    [Header("ײǽ�ȴ���ʱ��")]
    public float waitTimeDuration;      // ײǽ�ȴ�ʱ��
    public float waitTimeCounter;       // ײǽ�ȴ���ʱ��
    public bool waitTime;       // �Ƿ�ײǽ
    [Header("׷����ʧĿ���ʱ��")]
    public float chaseTimeDuration;     //׷������ʱ��
    public float chaseTimeCounter;      //׷��ʱ���ʱ��
    [Header("�ܻ����˲���")]
    public bool isHurt;     //����Ƿ񱻹���
    public float hurtForce;     //�ܻ���õ�����
    [Header("������ز���")]
    public bool isDead;     //����Ƿ�����
    public PickupSpawner pickupSpawner;     // ����������Ľű�����

    private BaseState currentState;         //��ǰ������״̬
    protected BaseState patrolState;        //Ѳ�߳�����״̬
    protected BaseState chaseState;         //׷��������״̬

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();       //��ʼ������rb
        anim = GetComponent<Animator>();        //��ʼ������anim
        physicsCheck = GetComponent<PhysicsCheck>();        //��ʼ������physicsCheck
        pickupSpawner = GetComponent<PickupSpawner>();      // ��ʼ������pickupSpawner
        currentSpeed = normalSpeed;             //��ʼʱ�ٶȱ���Ϊ�����ٶ�
        waitTimeCounter = waitTimeDuration;     //��ײǽ�ȴ���ʱ����Ϊײǽ�ȴ�ʱ�䣬���ڵ�һ��ײǽ���
        chaseTimeCounter = chaseTimeDuration;   //��׷��ʱ���ʱ������׷������ʱ�䣬���ڵ�һ��׷�����
    }
    private void OnEnable()         //����
    {
        //�õ�ǰ״̬����Ѳ��״̬
        currentState = patrolState;
        //����ǰ���ص�Ұ���ഫ�ݽ�ȥ��Ѳ��״̬����Ҫ�Ķ�������������
        currentState.OnEnter(this);
    }
    public void Update()
    {
        // ���ݵ����泯����ʵʱ������ͼ�泯����
        // ���ڵ���Scale.xΪ��ʱ�泯��ߣ�������Ҫ��xֵ�ϼ�һ���������ı䳯��
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        // Ѳ��״̬��ǰ����Ҫ���߼�������Update�и���
        currentState.LogicUpdate();
        // ����״̬��״̬ѡ�����ȴ�ʱ�仹�ǳ�ײ��ʱ״̬
        TimeCounter();
    }
    public void FixedUpdate()
    {
        //���û�����ˡ�û�мĵ�Ҳû�п�������£�ִ��Move����
        if (!isHurt && !isDead && !waitTime)
            Move();
        //�߼�״̬��ǰ����Ҫ�������߼�����FixedUpdate����
        currentState.PhysicsUpdate();
    }
    private void OnDisable()        //�ر�
    {
        //������ʧʱ�˳�����Ѳ��״̬
        currentState.OnExit();
    }
    public virtual void Move()      //virtual���������ڷ���ʱ��д���ຯ��
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }
    public void TimeCounter()      //�ȴ�ʱ���ʱ��
    {
        //�ȴ�ʱ���ʱ��
        if (waitTime)
        {
            //��ʼ��ʱ�ȴ�
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                waitTime = false;
                //��waitTimeCounter�ָ�����Ӧ����һ��ײǽ�ȴ�
                waitTimeCounter = waitTimeDuration;
                //Ұ��ײǽ��ı��泯����
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }
        //׷��ʱ���ʱ��
        if (!FoundPlayer())
        {
            //��ֹʱ����ٵ�0
            if(chaseTimeCounter > 0)
                //��ʼ׷��ʱ�䵹��ʱ
                chaseTimeCounter -= Time.deltaTime;
        }
        else
        {
            chaseTimeCounter = chaseTimeDuration;
        }
    }
    #region �ܻ��������¼�ִ�з���
    public void OnTakeDamage(Transform AttackTrans)         //���˺�ִ��ת��ͷ���
    {
        //��ײǽ�ȴ���ִ�У���ֹ������ת��󵹼�ʱ�����ٴ�ת��
        waitTime = false;
        waitTimeCounter = waitTimeDuration;
        //���ݹ����߷���ı���˷���
        attacker = AttackTrans;
        if (attacker.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(1, 1, 1); ;
        if (attacker.position.x - transform.position.x > 0)
            transform.localScale = new Vector3(-1, 1, 1); ;
        //�������˷���
        isHurt = true;
        anim.SetTrigger("hurt");
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - AttackTrans.position.x), 0).normalized;
        //����Э��
        StartCoroutine(OnHurt(dir));
    }
    private IEnumerator OnHurt(Vector2 dir)        //�������������ô����һ��ʱ���ִ�У�IEnumerator��int��voidһ�����ڷ�������
    {
        rb.AddForce(hurtForce * dir, ForceMode2D.Impulse);
        //���ϣ��������ʲô����ʱ�����÷���ֵΪnull,��˼Ϊ�ȴ�һ֡��ִ�к���Ĵ���
        //�����ȴ�����ʱ��Ļ�����newһ��ѡһ��������ѡWaitForSeconds���ڵȴ�����
        //�ȴ�������Ϊ֡��*������*0.01��Ұ������˶���Ϊ4֡��������Ϊ8�����Եȴ�0.32s����ȻҲ�������ô�һЩ
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }
    public void OnDie()       //������������
    {
        // ������������
        anim.SetTrigger("dead");
        isDead = true;
        // �������Ϲ��ص���Ʒ
        pickupSpawner.DropItems();
    }
    public void OnDestroyAfterAnimation()       //�����������������
    {
        //destroy��������������ڸú���������
        Destroy(this.gameObject);
    }
    #endregion
    public bool FoundPlayer()       //������ҵĺ���
    {
        //BoxCastֵ�ֱ�Ϊ���ĵ㣬�����С������ĽǶȣ����ʸ�������泯���򣩣����Ͷ����룬���ͼ��
        //BoxCast����ֵΪRaycastHit2D�����е�collider���Կ��Լ���Ƿ�������ײ������boolֵ����ͨ����ֵ���ж��Ƿ�����ײ
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffSet, checkSize, 0, faceDir, checkDistance, attackLayer);
    }

    public void SwitchState(NPCState state)       //ʹ��ö���л���ͬ״̬
    {
        //switch�﷨��д���������ڼ򵥵ı����л�
        //����⵽�������ı���ֵ��˭�󣬾ͽ���ͷ�����ֵ���ݸ�newState
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null
        };
        //���ݳɹ����˳�current֮ǰ��״̬��������״̬��������ִ��
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    public void OnDrawGizmosSelected()      //���Ƽ����ҵĿ���
    {
        //ʹ��Gizmos���ߣ�ȷ����ײ��Ĵ���λ��
        //��Ҫʹ�þ��μ��򣬻��ò���
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffSet + new Vector3(checkDistance * -transform.localScale.x, 0), 0.2f);
    }
}