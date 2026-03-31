using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.PlayerLoop;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject panel1;
    private TextMeshProUGUI text;
    private bool CanAcceptQuest = false;
    private bool questIsFinished = false;
    private int questTracker = 0;
    private bool playerCanTalk = false;

    private string message1 =
        "Could your bring me the SHROOMS over there?\n Im deathly Afraid of those Onimous Cubes." +
        "\n      (Press Interact To Accept The Quest?)";

    private string message2 = "PLEAAAASEEE I NEED TO FEEL SOMETHING \n after myy wife Miriam left me";
    private string message3 = "Thank you very much kind Stranger. \n For your Troubles have this as Gift!";
    public List<string> Message = new List<string>();


    void Start()
    {
        panel.SetActive(false);
        Message.Add(message1);
        Message.Add(message2);
        Message.Add(message3);
        Debug.Log(Message[0]);
        text = panel.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameEvents.OnTryToInteract += TalkToPlayer;
        GameEvents.OnQuestFinish += OnQuestFinish;
    }

    private void OnDisable()
    {
        GameEvents.OnTryToInteract -= TalkToPlayer;
        GameEvents.OnQuestFinish -= OnQuestFinish;
    }

    private void TalkToPlayer()
    {
        if (CanAcceptQuest && questTracker == 0 && playerCanTalk)
        {
            questTracker = 1;
            panel.SetActive(false);
            GameEvents.OnQuestAccept.Invoke();
        }
        else if (questTracker == 1 && questIsFinished && playerCanTalk)
        {
            questTracker = 2;
            EnableAndUpdateUI();
        }
        else if (questTracker == 2 && playerCanTalk)
        {
            StartCoroutine(EndScreen());
        }

        if (playerCanTalk)
        {
            StartCoroutine(SetQuestAccept());
            EnableAndUpdateUI();
        }
    }

    private void OnQuestFinish()
    {
        questIsFinished = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCanTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(false);
            CanAcceptQuest = false;
            playerCanTalk = false;
        }
    }

    private void EnableAndUpdateUI()
    {
        text.text = Message[questTracker];
        panel.SetActive(true);
    }

    IEnumerator SetQuestAccept()
    {
        yield return new WaitForSeconds(0.2f);
        CanAcceptQuest = true;
    }

    IEnumerator EndScreen()
    {
        yield return new WaitForSeconds(5f);
        panel.SetActive(false);
        panel1.SetActive(true);
    }
}