using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public static Transform[] spawnPoints;
    public GameObject target; 

    private void Awake()
    {
        spawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }
    }

    private void Start()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(target, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
