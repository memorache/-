using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResetItemsOnStartup        // �����ڱ༭������ʱ������Ʒ����
{
    [InitializeOnLoadMethod]
    private static void OnEditorStartup()
    {
        ResetItemCounts();
    }
    private static void ResetItemCounts()
    {
        // ��ȡ����Item��Դ��·��
        string[] guids = AssetDatabase.FindAssets("t:Item", new[] { "Assets/Inventory/Items" });
        foreach (string guid in guids)
        {
            // ����GUID��ȡ�ʲ���·��
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            // �����ʲ�
            Item item = AssetDatabase.LoadAssetAtPath<Item>(assetPath);
            if (item != null)
            {
                // ����itemHoldΪ0
                item.itemHold = 0;
                // �����޸ĺ���ʲ�
                EditorUtility.SetDirty(item);
            }
        }
        // ���������޸�
        AssetDatabase.SaveAssets();
    }
}
