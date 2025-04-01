using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    // ��Ҫ���¼���������������Ƿ��й��ɳ������ݹ�ȥ
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestScene;
    // ����һ����������LoadRequestScene�ķ���,���صĳ�����Ҫȥ������λ�á��Ƿ��뽥��
    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="sceneToGo">Ҫȥ�ĳ���</param>
    /// <param name="postionToGo">Ҫȥ������</param>
    /// <param name="fadeScreen">�Ƿ��뽥��</param>
    public void RaiseLoadRequestEvent(GameSceneSO sceneToGo, Vector3 postionToGo, bool fadeScreen)
    {
        LoadRequestScene?.Invoke(sceneToGo, postionToGo, fadeScreen);
    }
}
