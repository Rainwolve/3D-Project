using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private List<IHurtable> hurtables = new List<IHurtable>();

    public List<IHurtable> Hurtables { get { return hurtables; } }
    private SphereCollider sphereCollider;
    public SphereCollider SphereCollider { get { return sphereCollider; }set { sphereCollider = value; } }

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHurtable>(out var hurtable))
        {
            hurtables.Add(hurtable);
        }

        if (other.CompareTag("NPC"))
        {
            Debug.Log("Hit NPC");
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
