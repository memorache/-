using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
    // 需要传递变换颜色，变换时间以及用bool值判断渐入还是渐出
    public UnityAction<Color, float, bool> OnRaisedEvent;
    // 由于有渐入渐出两个需求方法，所以在这里分别创建两个方法
    /// <summary>
    /// 逐渐变黑,渐入
    /// </summary>
    /// <param name="duration"></param>
    public void FadeIn(float duration)
    {
        // 自带方法Color.black实现屏幕变黑
        RaiseEvent(Color.black, duration, true);
    }
    /// <summary>
    /// 逐渐变透明，渐出
    /// </summary>
    /// <param name="duration"></param>
    public void FadeOut(float duration)
    {
        // 自带方法Color.clear实现屏幕透明
        RaiseEvent(Color.clear, duration, false);
    }
    public void RaiseEvent(Color target,float duration,bool fadeIn)
    {
        // fadeIn为true时渐入，为false时渐出
        OnRaisedEvent?.Invoke(target, duration, fadeIn);
    }
}
