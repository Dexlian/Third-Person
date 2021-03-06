using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPopUpsFade : MonoBehaviour
{
    PlayerManager playerManager;
    CanvasGroup canvasGroup;
    [SerializeField] GameObject popUpFineFull;
    [SerializeField] GameObject popUpFineDamaged;
    [SerializeField] GameObject popUpCaution;
    [SerializeField] GameObject popUpDanger;

    public bool canFade;
    public bool isFading;

    [Header("Fade Options")]
    [SerializeField] float timeBeforeFadeOutBegins = 2;
    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        DisableAllObjects();
        canvasGroup.alpha = 1f;
        canFade = true;
        isFading = false;
    }

    private void Update()
    {
        if (isFading)
        {
            if (playerManager.playerHealth >= 100)
            {
                popUpFineFull.SetActive(true);
                popUpFineDamaged.SetActive(false);
                popUpCaution.SetActive(false);
                popUpDanger.SetActive(false);
            }
            else if (playerManager.playerHealth >= 66 && playerManager.playerHealth <= 99)
            {
                popUpFineFull.SetActive(false);
                popUpFineDamaged.SetActive(true);
                popUpCaution.SetActive(false);
                popUpDanger.SetActive(false);
            }
            else if (playerManager.playerHealth >= 33 && playerManager.playerHealth <= 65)
            {
                popUpFineFull.SetActive(false);
                popUpFineDamaged.SetActive(false);
                popUpCaution.SetActive(true);
                popUpDanger.SetActive(false);
            }
            else if (playerManager.playerHealth >= 01 && playerManager.playerHealth <= 32)
            {
                popUpFineFull.SetActive(false);
                popUpFineDamaged.SetActive(false);
                popUpCaution.SetActive(false);
                popUpDanger.SetActive(true);
            }
        }
    }

    private void DisableAllObjects()
    {
        popUpFineFull.SetActive(false);
        popUpFineDamaged.SetActive(false);
        popUpCaution.SetActive(false);
        popUpDanger.SetActive(false);
    }

    public void DisplayHealthPopUp(int playerHealthPercentage)
    {
        if (canFade && !isFading)
        {
            /*if (playerHealthPercentage >= 100)
            {
                popUpFineFull.SetActive(true);
            }
            else if (playerHealthPercentage >= 66 && playerHealthPercentage <= 99)
            {
                popUpFineDamaged.SetActive(true);
            }
            else if (playerHealthPercentage >= 33 && playerHealthPercentage <= 65)
            {
                popUpCaution.SetActive(true);
            }
            else if (playerHealthPercentage >= 01 && playerHealthPercentage <= 32)
            {
                popUpDanger.SetActive(true);
            }
            */
            StartCoroutine(FadeIn());
        }
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
