using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRandomZombie : MonoBehaviour
{
    private Transform randomChild;

    void Awake()
    {
        int randomChildIdx = Random.Range(0, gameObject.transform.childCount);
        randomChild = gameObject.transform.GetChild(randomChildIdx);
    }

    private void Start()
    {
        randomChild.gameObject.SetActive(true);
    }
}
