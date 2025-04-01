using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;        // Addressable��

[CreateAssetMenu(menuName="Game Scene/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    public SceneType sceneType;                 // ָ����ǰ����������
    public AssetReference sceneReference;       // ʹ�ñ���ѡ��Ҫʹ�õĳ���
}
