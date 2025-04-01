using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;        // Addressable包

[CreateAssetMenu(menuName="Game Scene/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    public SceneType sceneType;                 // 指定当前场景的类型
    public AssetReference sceneReference;       // 使用变量选定要使用的场景
}
