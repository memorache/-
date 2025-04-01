using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour, IInteractable
{
    public GameObject dialogueUI;       // 人物对话UI

    public void TriggerAction()
    {
        dialogueUI.SetActive(true);
    }
}
