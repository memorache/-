using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
    public SceneLoadEventSO loadEventSO;    // ���ڽ��������ݵĲ���ת�Ƶ�SceneLoadEventSO�е�LoadRequestScene�¼���
    public GameSceneSO sceneToGo;       // ָ�����͵ĳ���
    public Vector3 positionToGo;        // ָ�����͵�λ��

    public void TriggerAction()
    {
        Debug.Log("Go!");
        // �����¼����й㲥
        loadEventSO.RaiseLoadRequestEvent(sceneToGo, positionToGo, true);
    }
}
