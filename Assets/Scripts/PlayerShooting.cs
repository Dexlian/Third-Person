using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    CharacterAnimator characterAnimator;
    PlayerCameraAim playerCameraAim;

    public static Action shootInput;
    public static Action reloadInput;

    public bool playerShootingFullAuto;

    [Header("Keybinds")]
    public KeyCode reloadKey = KeyCode.R;

    private void Start()
    {
        characterAnimator = GetComponent<CharacterAnimator>();
        playerCameraAim = GetComponent<PlayerCameraAim>();
    }

    private void Update()
    {
        if (playerCameraAim.isAiming && characterAnimator.isAimedIn && Input.GetMouseButtonDown(0))
        {
            shootInput?.Invoke();
        }

        if (playerCameraAim.isAiming && characterAnimator.isAimedIn && Input.GetMouseButton(0))
        {
            playerShootingFullAuto = true;
        }
        else
        {
            playerShootingFullAuto = false;
        }

        if (playerCameraAim.isAiming && characterAnimator.isAimedIn && Input.GetKeyDown(reloadKey))
        {
            reloadInput?.Invoke();
        }
    }
}
