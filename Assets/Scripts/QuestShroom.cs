using System;
using UnityEngine;

public class QuestShroom : MonoBehaviour
{
    [SerializeField] bool playerIsInArea = false;
    private bool allowedToPickUp;

    private void OnEnable()
    {
        GameEvents.OnQuestAccept += OnQuestAccept;
        GameEvents.OnTryToInteract += OnTryToPickUp;
    }

    private void OnDisable()
    {
        GameEvents.OnTryToInteract -= OnTryToPickUp;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsInArea = false;
        }
    }

    private void OnQuestAccept()
    {
        allowedToPickUp = true;
    }

    private void OnTryToPickUp()
    {
        if (playerIsInArea&&allowedToPickUp)
        {
            GameEvents.OnQuestFinish?.Invoke();
            Destroy(gameObject, 0.5f);
           
        }
    }
}