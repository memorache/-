using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
    public SceneLoadEventSO loadEventSO;    // 用于将场景传递的参数转移到SceneLoadEventSO中的LoadRequestScene事件中
    public GameSceneSO sceneToGo;       // 指定传送的场景
    public Vector3 positionToGo;        // 指定传送点位置

    public void TriggerAction()
    {
        Debug.Log("Go!");
        // 激活事件进行广播
        loadEventSO.RaiseLoadRequestEvent(sceneToGo, positionToGo, true);
    }
}
