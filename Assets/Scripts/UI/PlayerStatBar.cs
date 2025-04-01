using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // 调用Unity中UI包

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;       // 血条绿色图层
    public Image healthDelayImage;      // 延迟血条红色图层
    public Image powerImage;        // 能量条黄色图层
    private void Update()
    {
        // 让红色血条跟随绿色血条减少，实现渐变的效果
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime;
        }
    }
    public void OnHealthChange(float persentage)        // 将血条百分比传递到血条上
    {
        // 将persentage的值调用给fillAmount
        healthImage.fillAmount = persentage;
    }
}
