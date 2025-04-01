using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;    //����UnityEngine���е�InputSystem����

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;     //���Ե���InputSystem�Ĵ��룬C#��ÿһ����Ҫʹ�õ�ʱ�򣬶���Ҫһ��ר�ŵ�ʵ����new��ȥʹ����
    public Vector2 inputDirection;     //����Vector2����InputDirection��������Unity���ܱ��鿴��
    public float inputLeftControl;      //����float����inputLeftControl��������Unity���ܱ��鿴��
    private Rigidbody2D rb;           //���ڻ�ȡRigidbody2D�еı���
    //private SpriteRenderer sr;       //���ڻ�ȡSpriteRenderer�еı���
    private PlayerAnimation playerAnimation;        //������ȡplayerAnimation�еı���
    private PhysicsCheck physicsCheck;     //����PhysicsCheck�еı���������������ֻ��һ��
    [Header("��������")]            //�����ڷ��಻ͬ��ֵ�Ĵ���
    public float speed;             //���������ٶȵ�ֵ
    public float jumpForce;         //��Ծ������
    [Header("�ܻ����˲���")]
    public float hurtForce;         //���˺��º��˵���
    public bool isHurt;         //����Ƿ�����
    [Header("������ز���")]
    public bool isDead;         //��������Ƿ�����
    [Header("������ز���")]
    public bool isAttack;       //��������Ƿ��°������й���
    [Header("��Ч��ز���")]
    public AudioDefination audioDefination;     // ����ָ��������Ч

    private void Awake()     //��startǰ���У���Ϸ���ʼ��˳��ΪAwake > OnEnable > Start
    {
        //��ʼ������rb����ȡRigidbody2D��ʹ��Ȩ�����ñ����ķ�ʽ��Rigidbody2D�����в�������
        rb = GetComponent<Rigidbody2D>();
        //��ʼ������physicsCheck��������ȡPhysicsCheck��ʹ��Ȩ
        physicsCheck = GetComponent<PhysicsCheck>();
        //��ʼ������playerAnimation��������ȡPlayerAnimation��ʹ��Ȩ
        playerAnimation = GetComponent<PlayerAnimation>();
        //InputControl��ʵ����
        inputControl = new PlayerInputControl();
        // audioDefination�ĳ�ʼ��
        audioDefination = GetComponent<AudioDefination>();
        //�¼�ע�ắ��+=����ʾ���������������ӵ�����������ʱ��ִ�У��ɶ�д�������ڰ�������ʱͬʱִ��
        //����Move��started��canceled��performed���ֱ���������µ�һ�̣������ɿ���һ�̺�һֱ����
        //����Ϊ����Ծ�ܹ�������Ӧѡ��started
        inputControl.GamePlay.Jump.started += Jump;
        //�����İ�����Ӧ�¼�
        inputControl.GamePlay.Attack.started += PlayerAttack;
        //��ʼ������sr����ȡSpriteRenderer��ʹ��Ȩ�����ñ����ķ�ʽ��Rigidbody2D�����в�������,����ʵ�����ﷴת
        //sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()     //���忪ʼʱ����
    {
        inputControl.Enable();
    }

    private void OnDisable()    //����ر�ʱ����
    {
        inputControl.Disable();
    }

    private void Update()     //������ÿһ֡����ִ�еĺ���
    {
        //�ñ���inputDirection��ȡ��Move�������ƶ�����ֵ
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        //�ñ���inputLeftControl��ȡ������Ctrl�����ֵ
        inputLeftControl = inputControl.GamePlay.Otherkeys.ReadValue<float>();
    }

    private void FixedUpdate()    //��һ���̶���ʱ�����һ��
    {
        //�������û���ܻ����ڹ���������ƶ�
        if(!isHurt && !isAttack)
            Move();             //����Move����
    }

    //��ײ�����
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("collision.name");
    //}

    public void Move()     //�Լ�д�ĺ����������ý�ɫ���������ƶ�����
    {   
        //�����ƶ�
        //X�᷽���ٶ�=Vector2�е�Xֵ*speed����*ʱ�����������������ڲ�ͬ�����ϻ����ͬ��Ч������y���0�ή�������ٶȣ����䱣֤ԭ�����ٶȲ��伴��
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        //�������Ctrl�����ٶȽ��ͣ������ڵ����ϵ�Ҳ�����е���·Ч��
        if (inputLeftControl != 0)
            rb.velocity /= 2;

        //���﷭ת
        //Scale.X���������ã�
        //�ڷ�������£�ͨ���ж�InputDirection��ֵ����������������ĳ���
        if (inputDirection.x != 0)
            //ʹ�����������?�����ж�
            //��Ϊ����Unity����ж���Transformֵ�����Կ���ֱ�ӵ��ã�����Ҫʹ��getComponent
            transform.localScale = new Vector3(inputDirection.x < 0 ? -1 : 1, 1, 1);

        //Flip.X����
        //�ڷ�������£�ͨ���ж�InputDirection��ֵ����������������ĳ���ע��flipX��ֵΪboolֵ��
        //if (InputDirection.x != 0)
        //    sr.flipX = InputDirection.x < 0 ? true : false;
    }
    private void PlayerAttack(InputAction.CallbackContext context)      //PlayerAttack�Ĺ�������
    {
        //����ʱ�ٶȼ�Ϊ0
        rb.velocity = Vector2.zero;
        //������������
        playerAnimation.PlayerAttack();
        isAttack = true;
    }
    private void Jump(InputAction.CallbackContext context)   //Jump�ĺ�������
    {
        //Debug����
        //Debug.Log("JUMP");
        //ʹ��AddForce���������������ϵ�˲ʱ�����������ܹ�������
        //ʹ��if�����ж�
        //��������AddForce�����ֺ������أ�ͬ�������в�ͬ����������������ѡ��ڶ��֣�����Impulse��ʾ˲ʱ��
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            // �������󲥷���Ծ��Ч
            audioDefination.PlayAudioClip();
        }
    }

    //��UnityEvent��ִ�еĺ�������
    public void GetHurt(Transform Attacker)      //������ʱִ�з�������
    {
        //�ܻ����ܻ��ж�Ϊtrue
        isHurt = true;
        //�ܻ���x,y�ٶȹ��㣬��ȥ����Ӱ��
        rb.velocity = Vector2.zero;
        //�ж��ܻ��������빥���ߵ�λ��
        //ʹ��Vector2ʹ����ֵΪ������ͨ�������빥���߾���Ĳ�ֵ��������������
        //��������͹�����֮������Զ�ᵼ��dirֵ���������ֻ��Ҫһ������ֵ������ʹ��normalized��һ��
        Vector2 dir = new Vector2((transform.position.x - Attacker.position.x), 0).normalized;
        //������������ʩ����
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }
    public void PlayerDead()        //����ʱ��ֹ��������
    {
        //�ж�����
        isDead = true;
        //��ʱ��ֹ��Ҳ���������UI�����ᱣ��
        inputControl.GamePlay.Disable();
    }
}