using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;      // 用于获取当前物体组件，用于修改图片
    public Sprite openSprite;       // 打开后的状态
    public Sprite closeSprite;      // 关闭的状态
    public AudioDefination audioDefination;        // 指定播放的音频
    public bool isDone;             // 记录所有可互动物品的状态

    private void Awake()
    {
        // 初始化变量spriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 初始化audioDefination
        audioDefination = GetComponent<AudioDefination>();
    }
    private void OnEnable()
    {
        // 在启用场景时根据isDone状态让可互动物体播放不同图片
        spriteRenderer.sprite = isDone ? openSprite : closeSprite;
    }
    public void TriggerAction()         // 实现接口的方法
    {
        // 如果宝箱可以互动，就执行开宝箱的方法
        if (!isDone)
        {
            OpenChest();
        }
    }
    public void OpenChest()         // 实现打开宝箱后进行的方法
    {
        // 将宝箱图片切换为打开状态
        spriteRenderer.sprite = openSprite;
        // 箱子打开后播放指定音效
        audioDefination.PlayAudioClip();
        // 将可互动状态改为false
        isDone = false;
        // 打开后让宝箱的标签改为别的，让宝箱宝箱无法被识别到
        this.transform.tag = "Untagged";
    }
}
