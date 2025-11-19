using NUnit.Framework.Constraints;
using UnityEngine;


public class EdgeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float spaceBetween = 1f;
    [SerializeField] private bool generateBorder = true;

    private Vector3[] directions;
    Vector3 currentPosition;
    private float rotationAngle;
    private Vector3 currentRotation;

    void Start()
    {
        currentPosition = startPosition;

        if (prefabs.Length == 0) return;
        currentRotation = Vector3.right;
        for (int i = 0; i < 4; i++)
        {
            while (Vector3.Distance(currentPosition, startPosition) < Mathf.Abs((startPosition.x*2)-1))
            {
                SpawnObject(currentPosition);
                currentPosition += currentRotation * spaceBetween;
            }

            SpawnHitbox(startPosition, currentPosition, i);
            startPosition = currentPosition;
            currentRotation = Quaternion.Euler(0, -90, 0) * currentRotation;
        }
    }

    private void SpawnObject(Vector3 position)
    {
        position = position + new Vector3(Random.Range(-1.5f, 1.5f),0, Random.Range(-1.5f, 1.5f));
        Instantiate(prefabs[Random.Range(0, prefabs.Length)], position, Quaternion.Euler(0, Random.Range(0, 360), 0),
            transform);
    }

    private void SpawnHitbox(Vector3 position, Vector3 position2, int i)
    {
        Vector3 direction = position2 - position;

        BoxCollider hedgeCollider = gameObject.AddComponent<BoxCollider>();
        if(i%2==0)
        {
            hedgeCollider.size = new Vector3(direction.magnitude, 4, 1);
        }
        else
        {
            hedgeCollider.size = new Vector3(1, 4, direction.magnitude);
        }
      
        hedgeCollider.center = Quaternion.Euler(0, i * 90f, 0)* new Vector3(0, 2, 45);
    }
}