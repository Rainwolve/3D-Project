using System;
using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    private bool isPlayerInAttackArea;

    public bool IsPlayerInAttackArea
    {
        get { return isPlayerInAttackArea; }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInAttackArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInAttackArea = false;
        }
    }
}
