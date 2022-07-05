using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitmarker : MonoBehaviour
{
    public GameObject hitmarker;
    public AudioManager audioManager;

    public float timer;

    void Start()
    {
        hitmarker.SetActive(false);

        timer = 0f;
    }

    public void SetTime()
    {
        timer = 0.2f;
        audioManager.Play("HitmarkerSound");
    }

    public void SetPosition(float x, float y)
    {
        hitmarker.transform.position = new Vector2(x, y);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SetTime();
        }

        if (timer > 0f)
        {
            hitmarker.SetActive(true);
            timer -= Time.deltaTime;
        }
        else
        {
            hitmarker.SetActive(false);
        }
    }
}
