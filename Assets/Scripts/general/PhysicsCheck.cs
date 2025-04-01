using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    public LayerMask groundLayer;   //为LayerMask类型设置初始化变量，用于在Unity编辑器中指定哪些层是地面
    [Header("矩形检测框大小偏移参数")]
    //底部和两侧位置的矩形碰撞箱检测，由于有碰撞检测bug暂时停用
    //public Vector2[] boxSizes = new Vector2[2]; // 0: 底面, 1: 两侧
    //public Vector2[] boxOffsets = new Vector2[2]; // 0: 底面, 1: 两侧
    //两侧圆形碰撞箱检测方式
    public Vector2[] offsets = new Vector2[2];      //0: 底面, 1: 两侧
    public float[] checkRadius = new float[2];      //0: 底面, 1: 两侧
    [Header("状态")]
    public bool isGround;    //根据碰撞体检测半径判断其是否在地面，用来限制跳跃
    public bool touchWall;        //根据两侧碰撞体是否碰墙

    private void Start()
    {
        // 由于未知原因，isGround在开始时会被判定为false，导致敌人巡逻出现问题，所以在start中强制设置isGround为true
        isGround = true;   
    }
    private void Update()   //检测函数放在Update中持续更新
    {
        CheckGrounded();
        CheckWalls();
    }
    private void CheckGrounded()        //地面矩形框检测函数
    {
        // 计算底部矩形框的两个对角点，由于有碰撞检测bug暂时停用
        // 使用Unity中的Physics2D脚本的OverlapArea方法检测碰撞体，返回bool值，用于限制跳跃
        // 第一个中心点位置加上位移向量，由于positon为三维向量，需要将其转换为二维向量
        // 第三个groundLayer用于选择检测图层
        //isGround = Physics2D.OverlapArea(
        //    (((Vector2)transform.position + boxOffsets[0]) * transform.localScale - boxSizes[0] / 2),
        //    (((Vector2)transform.position + boxOffsets[0]) * transform.localScale + boxSizes[0] / 2),
        //    groundLayer
        //);

        // 使用Unity中的Physics2D脚本的OverlapCircle方法检测碰撞体，返回bool值，用于限制跳跃
        // 第一个中心点位置加上位移向量，由于positon为三维向量，需要将其转换为二维向量
        // 第三个groundLayer用于选择检测图层
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + offsets[0] * transform.localScale, checkRadius[0], groundLayer);
    }
    private void CheckWalls()       //左右墙壁矩形框检测函数
    {
        // 计算一侧矩形框的两个对角点，由于有碰撞检测bug暂时停用
        //touchWall = Physics2D.OverlapArea(
        //    (((Vector2)transform.position + boxOffsets[1]) * transform.localScale - boxSizes[1] / 2),
        //    (((Vector2)transform.position + boxOffsets[1]) * transform.localScale + boxSizes[1] / 2),
        //    groundLayer
        //);

        //使用相同的办法计算两侧的点
        touchWall = Physics2D.OverlapCircle((Vector2)transform.position + offsets[1] * transform.localScale, checkRadius[1], groundLayer);
    }
    private void OnDrawGizmosSelected()     //用于对选中的对象绘制gizmos
    {
        // 依次绘制两个矩形框的范围，由于有碰撞检测bug暂时停用
        //for (int i = 0; i < boxSizes.Length; i++)
        // 依次绘制两个圆形框的范围
        for (int i = 0; i < checkRadius.Length; i++)
        {
            //矩形框检测
            //Gizmos.DrawWireCube((Vector2)transform.position + boxOffsets[i] * transform.localScale, boxSizes[i]);

            //圆形框检测
            Gizmos.DrawWireSphere((Vector2)transform.position + offsets[i] * transform.localScale, checkRadius[i]);
        }
    }
}