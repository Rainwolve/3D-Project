using UnityEngine;

public class CubeManager : MonoBehaviour
{
    private BoxCollider collider;
    [SerializeField] private float speed;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player collided");
            GameEvents.OnPlayerHurt.Invoke(1);
        }
    }

    private void FixedUpdate()
    {
        transform.position += speed * Time.fixedDeltaTime * Vector3.left;
        if (transform.position.x < -30f)
        {
            speed = -speed;
        }
        if (transform.position.x > 30f)
        {
            speed = -speed;
        }
    }
}