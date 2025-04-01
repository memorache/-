using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour      // 怪物死亡生成道具
{
    public PropPrefab[] dropPrefabs;        // 存储不同道具的预制体
    // 用于让物体随机运动
    public float spreadRadius = 2f;         // 掉落物品散开的半径
    public float minForce = 4f;             // 最小推力
    public float maxForce = 10f;             // 最大推力
    // 生成掉落道具
    public void DropItems()
    {
        // 遍历
        foreach (var dropPrefab in dropPrefabs)
        {
            // 实例化掉落物品
            GameObject item = Instantiate(dropPrefab.prefab, transform.position, Quaternion.identity);
            // 获取物品的Rigidbody2D组件
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 计算随机推力方向和大小
                Vector2 randomForce = Random.insideUnitCircle.normalized * Random.Range(minForce, maxForce);
                // 应用推力
                rb.AddForce(randomForce, ForceMode2D.Impulse);
            }
        }
    }
}

[System.Serializable]       // 没有MonoBehaviour，想在编辑器上显示就加[System.Serializable]
public class PropPrefab
{
    // 掉落道具预制体
    public GameObject prefab;
}