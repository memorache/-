using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;      // 引入DoTween命名空间

public class FadeCanvas : MonoBehaviour
{
    public Image fadeImage;     // 获取淡入淡出的Image组件
    [Header("事件监听")]
    public FadeEventSO fadeEvent;     // 监听FadeEventSO事件
    private void OnEnable()
    {
        // 创建事件监听
        fadeEvent.OnRaisedEvent += OnFadeEvent;
    }
    private void OnDisable()
    {
        // 注销事件监听
        fadeEvent.OnRaisedEvent -= OnFadeEvent;
    }
    public void OnFadeEvent(Color target, float duration,bool fadeIn)       // 淡入淡出方法
    {
        // 使用DoTween插件的DOBlendableColor方法，实现颜色渐变
        fadeImage.DOBlendableColor(target, duration);
    }
}
