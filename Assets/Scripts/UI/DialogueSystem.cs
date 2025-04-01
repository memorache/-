using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI���")]
    public TextMeshProUGUI textLabel;       // ��ȡ�Ի��ı����
    public Image faceImage;                 // ��ȡ��ӦͼƬ
    public PlayerInputControl inputControl;         // ���ڿ��ƶԻ�һ���Ľ���
    [Header("�ı��ļ�")]
    public TextAsset textfile;              // ��ȡ�Ի�������ı��ļ�
    public int index;                       // ��ȡÿ��Ի��ı��

    // �����ı��ļ�Ϊ���У�������Ҫʹ���б��ı����ݱ�ŷֳɶ��У�ת�����ı�
    // ��װ��������
    List<string> textList = new List<string>();

    private void Awake()
    {
        inputControl = new PlayerInputControl();
        inputControl.GamePlay.Confirm.started += TextDisplay;
    }
    private void Start()
    {
        // �����и��ı��������ļ��еķ���
        GetTextFromFile(textfile);
        // ��ʼʱ��index����
        index = 0;
    }
    private void Update()
    {
       
    }
    private void GetTextFromFile(TextAsset file)        // ����Ӧ�ļ��ı��и�����б���
    {
        // ����б���ֹ�б����ı��ѻ�
        textList.Clear();
        // ��Ź���
        index = 0;
        // ���ı���ÿһ���Իس���Ϊ��־�����и�
        // text���ڽ��ı�����תΪ�ַ����ͣ�Split�������ı�����ָ�������и�
        var lineDate = file.text.Split("\n");
        // ���и�õ��ı������б���
        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }
    private void TextDisplay(InputAction.CallbackContext context)
    {
        textLabel.text = textList[index];
        index++;
    }
}
