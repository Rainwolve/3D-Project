using NUnit.Framework.Constraints;
using UnityEngine;


public class FlowerGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float spaceBetween = 1f;
    [SerializeField] private float spaceBetweenRandom = 0.1f;
    [SerializeField] private int flowerdensity;

    private Vector3[] directions;
    Vector3 currentPosition;
    private float plusminus = 1;


    void Start()
    {
        currentPosition = startPosition;

        if (prefabs.Length == 0) return;
        for (int i = 0; i < 99; i++)
        {
            for (int j = 0; j < 99; j++)
            {
                SpawnObject(currentPosition);
                currentPosition += Vector3.right;
            }

            currentPosition = startPosition + Vector3.forward * i;
        }
    }

    private void SpawnObject(Vector3 position)
    {
        position = position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1, 1));
        if (Random.Range(0, 100) >= flowerdensity) return;
        Instantiate(prefabs[Random.Range(0, prefabs.Length)], position, Quaternion.Euler(0, Random.Range(0, 360), 0),
            transform);
    }
}