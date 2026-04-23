using UnityEngine;

public class DummyManager : MonoBehaviour
{
    private BoxCollider collider;
    private Animator animator;

    private int MaxHP = 10;
    [SerializeField] private int CurrentHP;

    void Start()
    {
        CurrentHP = MaxHP;
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CurrentHP <= 1f)
            {
                FuckingDie();   
            }
            else
            {
                LoseHP();
            }
        }
    }

    private void FuckingDie()
    {
        animator.SetBool("IsDead", true);
        Destroy(gameObject, 10f);
    }

    private void LoseHP()
    {
        
        animator.SetTrigger("IsHit");
        CurrentHP--;
    }
}