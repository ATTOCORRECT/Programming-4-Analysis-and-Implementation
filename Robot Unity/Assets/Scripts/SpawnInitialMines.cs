using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInitialMines : MonoBehaviour
{
    [SerializeField] GameObject MinePrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 100; i++)
        {
            Vector3 position = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));

            GameObject.Instantiate(MinePrefab, position, Quaternion.identity);
        }
    }
}
