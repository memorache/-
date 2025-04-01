using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI相关")]
    public TextMeshProUGUI textLabel;       // 获取对话文本组件
    public Image faceImage;                 // 获取对应图片
    public PlayerInputControl inputControl;         // 用于控制对话一句句的进行
    [Header("文本文件")]
    public TextAsset textfile;              // 获取对话所需的文本文件
    public int index;                       // 获取每句对话的编号

    // 由于文本文件为多行，所以需要使用列表将文本根据标号分成多行，转换成文本
    // 并装入数组中
    List<string> textList = new List<string>();

    private void Awake()
    {
        inputControl = new PlayerInputControl();
        inputControl.GamePlay.Confirm.started += TextDisplay;
    }
    private void Start()
    {
        // 调用切割文本并放在文件中的方法
        GetTextFromFile(textfile);
        // 开始时将index归零
        index = 0;
    }
    private void Update()
    {
       
    }
    private void GetTextFromFile(TextAsset file)        // 将对应文件文本切割并放在列表中
    {
        // 清空列表，防止列表内文本堆积
        textList.Clear();
        // 编号归零
        index = 0;
        // 让文本中每一行以回车键为标志进行切割
        // text用于将文本内容转为字符类型，Split用于让文本按照指定符号切割
        var lineDate = file.text.Split("\n");
        // 将切割好的文本放入列表当中
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
