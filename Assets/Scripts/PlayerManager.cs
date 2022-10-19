using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public InputManager inputManager;
    Animator animator;
    public PlayerInventory playerInventory;
    public PlayerUIManager playerUIManager;
    PlayerSounds playerSounds;

    public int playerMaxHealth = 100;
    public int playerHealth = 100;

    [Header("Player Flags")]
    public bool isPerformingAction;
    public bool isPerformingQuickTurn;
    public bool isAiming;
    public bool isAimedIn;
    public bool isShooting;
    public bool isReloading;
    public bool canInteract;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        animator = GetComponent<Animator>();
        playerInventory = GetComponent<PlayerInventory>();
        playerUIManager = GetComponent<PlayerUIManager>();
        playerSounds = GetComponent<PlayerSounds>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        inputManager.HandleAllInputs();

        isPerformingAction = animator.GetBool("isPerformingAction");
        isPerformingQuickTurn = animator.GetBool("isPerformingQuickTurn");
        isAiming = animator.GetBool("isAiming");
        isAimedIn = animator.GetBool("isAimedIn");
        isShooting = animator.GetBool("isShooting");
        isReloading = animator.GetBool("isReloading");
    }

    public void TakeDamageZombieHit(int damage)
    {
        playerHealth -= damage;

        if (playerHealth > 0)
        {
            playerUIManager.DisplayHealthPopUp();
            playerSounds.PlaySoundPlayerTakesHit();
        }
        else
        {
            //Kill the player
            playerSounds.PlaySoundPlayerDies();
        }
    }
}
