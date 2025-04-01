using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResetItemsOnStartup        // 用于在编辑器启动时重置物品数量
{
    [InitializeOnLoadMethod]
    private static void OnEditorStartup()
    {
        ResetItemCounts();
    }
    private static void ResetItemCounts()
    {
        // 获取所有Item资源的路径
        string[] guids = AssetDatabase.FindAssets("t:Item", new[] { "Assets/Inventory/Items" });
        foreach (string guid in guids)
        {
            // 根据GUID获取资产的路径
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            // 加载资产
            Item item = AssetDatabase.LoadAssetAtPath<Item>(assetPath);
            if (item != null)
            {
                // 设置itemHold为0
                item.itemHold = 0;
                // 保存修改后的资产
                EditorUtility.SetDirty(item);
            }
        }
        // 保存所有修改
        AssetDatabase.SaveAssets();
    }
}
