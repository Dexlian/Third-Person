using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCountFade : MonoBehaviour
{
    CanvasGroup canvasGroup;
    [SerializeField] GameObject ammoMagazineText;
    [SerializeField] GameObject ammoReserveText;
    [SerializeField] GameObject divider;

    public bool canFade;
    public bool isFading;

    [Header("Fade Options")]
    [SerializeField] float timeBeforeFadeOutBegins = 2;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        DisableAllObjects();
        canvasGroup.alpha = 0f;
        canFade = true;
        isFading = false;
    }

    public void CheckMagazine()
    {
        if (canFade && !isFading)
        {
            ammoMagazineText.SetActive(true);
            divider.SetActive(true);

            StartCoroutine(FadeIn());
        }
    }

    public void CheckMagazineAndReserve()
    {
        if (canFade && !isFading)
        {
            ammoMagazineText.SetActive(true);
            ammoReserveText.SetActive(true);
            divider.SetActive(true);

            StartCoroutine(FadeIn());
        }
    }

    private void DisableAllObjects()
    {
        ammoMagazineText.SetActive(false);
        ammoReserveText.SetActive(false);
        divider.SetActive(false);
    }

    IEnumerator FadeIn()
    {
        canFade = false;
        isFading = true;

        for (float fade = 0.05f; fade < 1; fade += 0.05f)
        {
            canvasGroup.alpha = fade;

            if (fade > 0.9f)
            {
                StartCoroutine(FadeOut());
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(timeBeforeFadeOutBegins);

        for (float fade = 1f; fade > 0; fade -= 0.05f)
        {
            canvasGroup.alpha = fade;

            if (fade <= 0.05f)
            {
                DisableAllObjects();
                canFade = true;
                isFading = false;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}
