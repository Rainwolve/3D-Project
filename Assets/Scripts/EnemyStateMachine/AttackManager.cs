using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private List<IHurtable> hurtables = new List<IHurtable>();

    public List<IHurtable> Hurtables { get { return hurtables; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHurtable>(out var hurtable))
        {
            hurtables.Add(hurtable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IHurtable>(out var hurtable))
        {
            hurtables.Remove(hurtable);
        }
    }
}
