using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour, IInteractable
{
    public GameObject dialogueUI;       // ����Ի�UI

    public void TriggerAction()
    {
        dialogueUI.SetActive(true);
    }
}
