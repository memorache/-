using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;      // ���ڻ�ȡ��ǰ��������������޸�ͼƬ
    public Sprite openSprite;       // �򿪺��״̬
    public Sprite closeSprite;      // �رյ�״̬
    public AudioDefination audioDefination;        // ָ�����ŵ���Ƶ
    public bool isDone;             // ��¼���пɻ�����Ʒ��״̬

    private void Awake()
    {
        // ��ʼ������spriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        // ��ʼ��audioDefination
        audioDefination = GetComponent<AudioDefination>();
    }
    private void OnEnable()
    {
        // �����ó���ʱ����isDone״̬�ÿɻ������岥�Ų�ͬͼƬ
        spriteRenderer.sprite = isDone ? openSprite : closeSprite;
    }
    public void TriggerAction()         // ʵ�ֽӿڵķ���
    {
        // ���������Ի�������ִ�п�����ķ���
        if (!isDone)
        {
            OpenChest();
        }
    }
    public void OpenChest()         // ʵ�ִ򿪱������еķ���
    {
        // ������ͼƬ�л�Ϊ��״̬
        spriteRenderer.sprite = openSprite;
        // ���Ӵ򿪺󲥷�ָ����Ч
        audioDefination.PlayAudioClip();
        // ���ɻ���״̬��Ϊfalse
        isDone = false;
        // �򿪺��ñ���ı�ǩ��Ϊ��ģ��ñ��䱦���޷���ʶ��
        this.transform.tag = "Untagged";
    }
}
