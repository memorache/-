using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    // 需要用事件将场景，坐标和是否有过渡场景传递过去
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestScene;
    // 创建一个用于启动LoadRequestScene的方法,加载的场景、要去的坐标位置、是否渐入渐出
    /// <summary>
    /// 场景加载需求
    /// </summary>
    /// <param name="sceneToGo">要去的场景</param>
    /// <param name="postionToGo">要去的坐标</param>
    /// <param name="fadeScreen">是否渐入渐出</param>
    public void RaiseLoadRequestEvent(GameSceneSO sceneToGo, Vector3 postionToGo, bool fadeScreen)
    {
        LoadRequestScene?.Invoke(sceneToGo, postionToGo, fadeScreen);
    }
}
