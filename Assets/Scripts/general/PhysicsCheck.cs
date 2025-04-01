using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("������")]
    public LayerMask groundLayer;   //ΪLayerMask�������ó�ʼ��������������Unity�༭����ָ����Щ���ǵ���
    [Header("���μ����Сƫ�Ʋ���")]
    //�ײ�������λ�õľ�����ײ���⣬��������ײ���bug��ʱͣ��
    //public Vector2[] boxSizes = new Vector2[2]; // 0: ����, 1: ����
    //public Vector2[] boxOffsets = new Vector2[2]; // 0: ����, 1: ����
    //����Բ����ײ���ⷽʽ
    public Vector2[] offsets = new Vector2[2];      //0: ����, 1: ����
    public float[] checkRadius = new float[2];      //0: ����, 1: ����
    [Header("״̬")]
    public bool isGround;    //������ײ����뾶�ж����Ƿ��ڵ��棬����������Ծ
    public bool touchWall;        //����������ײ���Ƿ���ǽ

    private void Start()
    {
        // ����δ֪ԭ��isGround�ڿ�ʼʱ�ᱻ�ж�Ϊfalse�����µ���Ѳ�߳������⣬������start��ǿ������isGroundΪtrue
        isGround = true;   
    }
    private void Update()   //��⺯������Update�г�������
    {
        CheckGrounded();
        CheckWalls();
    }
    private void CheckGrounded()        //������ο��⺯��
    {
        // ����ײ����ο�������Խǵ㣬��������ײ���bug��ʱͣ��
        // ʹ��Unity�е�Physics2D�ű���OverlapArea���������ײ�壬����boolֵ������������Ծ
        // ��һ�����ĵ�λ�ü���λ������������positonΪ��ά��������Ҫ����ת��Ϊ��ά����
        // ������groundLayer����ѡ����ͼ��
        //isGround = Physics2D.OverlapArea(
        //    (((Vector2)transform.position + boxOffsets[0]) * transform.localScale - boxSizes[0] / 2),
        //    (((Vector2)transform.position + boxOffsets[0]) * transform.localScale + boxSizes[0] / 2),
        //    groundLayer
        //);

        // ʹ��Unity�е�Physics2D�ű���OverlapCircle���������ײ�壬����boolֵ������������Ծ
        // ��һ�����ĵ�λ�ü���λ������������positonΪ��ά��������Ҫ����ת��Ϊ��ά����
        // ������groundLayer����ѡ����ͼ��
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + offsets[0] * transform.localScale, checkRadius[0], groundLayer);
    }
    private void CheckWalls()       //����ǽ�ھ��ο��⺯��
    {
        // ����һ����ο�������Խǵ㣬��������ײ���bug��ʱͣ��
        //touchWall = Physics2D.OverlapArea(
        //    (((Vector2)transform.position + boxOffsets[1]) * transform.localScale - boxSizes[1] / 2),
        //    (((Vector2)transform.position + boxOffsets[1]) * transform.localScale + boxSizes[1] / 2),
        //    groundLayer
        //);

        //ʹ����ͬ�İ취��������ĵ�
        touchWall = Physics2D.OverlapCircle((Vector2)transform.position + offsets[1] * transform.localScale, checkRadius[1], groundLayer);
    }
    private void OnDrawGizmosSelected()     //���ڶ�ѡ�еĶ������gizmos
    {
        // ���λ����������ο�ķ�Χ����������ײ���bug��ʱͣ��
        //for (int i = 0; i < boxSizes.Length; i++)
        // ���λ�������Բ�ο�ķ�Χ
        for (int i = 0; i < checkRadius.Length; i++)
        {
            //���ο���
            //Gizmos.DrawWireCube((Vector2)transform.position + boxOffsets[i] * transform.localScale, boxSizes[i]);

            //Բ�ο���
            Gizmos.DrawWireSphere((Vector2)transform.position + offsets[i] * transform.localScale, checkRadius[i]);
        }
    }
}