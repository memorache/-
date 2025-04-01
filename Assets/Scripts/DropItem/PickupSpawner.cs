using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour      // �����������ɵ���
{
    public PropPrefab[] dropPrefabs;        // �洢��ͬ���ߵ�Ԥ����
    // ��������������˶�
    public float spreadRadius = 2f;         // ������Ʒɢ���İ뾶
    public float minForce = 4f;             // ��С����
    public float maxForce = 10f;             // �������
    // ���ɵ������
    public void DropItems()
    {
        // ����
        foreach (var dropPrefab in dropPrefabs)
        {
            // ʵ����������Ʒ
            GameObject item = Instantiate(dropPrefab.prefab, transform.position, Quaternion.identity);
            // ��ȡ��Ʒ��Rigidbody2D���
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // ���������������ʹ�С
                Vector2 randomForce = Random.insideUnitCircle.normalized * Random.Range(minForce, maxForce);
                // Ӧ������
                rb.AddForce(randomForce, ForceMode2D.Impulse);
            }
        }
    }
}

[System.Serializable]       // û��MonoBehaviour�����ڱ༭������ʾ�ͼ�[System.Serializable]
public class PropPrefab
{
    // �������Ԥ����
    public GameObject prefab;
}