using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Player player;
    public List<Enemy> enemyPrefabs;
    private Enemy currentEnemy;

    private int currentEnemyIndex = -1;

    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private TMP_Text playerNameText, playerHealthText, enemyNameText, enemyHealthText, gameStatusText;
    [SerializeField] private Image enemyImage;
    [SerializeField] private GameObject attackButton, shieldButton;

    [SerializeField] private GameObject healButton;
    [SerializeField] private int healAmount = 20;
    [SerializeField] private int healCooldownTurns = 3;
    private int turnsSinceLastHeal = 0;

    [SerializeField] private AudioClip buttonClickSound;
    private AudioSource uiAudioSource;
    
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text killedMonstersText;
    private int monstersKilledCount = 0;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        uiAudioSource = GetComponent<AudioSource>();
        if (uiAudioSource == null)
        {
            uiAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        monstersKilledCount = 0;

        UpdatePlayerUI();
        SpawnNextEnemyInSequence();
        UpdateEnemyUI();
        gameStatusText.text = "Fight!";
        SetUIButtonsActive(true);
        UpdateHealButtonState();
    }

    void UpdatePlayerUI()
    {
        playerNameText.text = player.CharName;
        playerHealthText.text = "Health: " + player.Health.ToString() + "/" + player.MaxHealth.ToString();
    }

    void UpdateEnemyUI()
    {
        if (currentEnemy != null)
        {
            enemyNameText.text = currentEnemy.EnemyName;
            enemyHealthText.text = "Health: " + currentEnemy.Health.ToString();
            enemyImage.sprite = currentEnemy.EnemySprite;
            enemyImage.enabled = true;
            enemyImage.SetNativeSize();
        }
        else
        {
            enemyNameText.text = "No Enemy";
            enemyHealthText.text = "Health: 0";
            enemyImage.enabled = false;
        }
    }

    void UpdateHealButtonState()
    {
        if (healButton != null)
        {
            healButton.SetActive(turnsSinceLastHeal >= healCooldownTurns);
        }
    }

    void SpawnNextEnemyInSequence()
    {
        if (currentEnemy != null)
        {
            Destroy(currentEnemy.gameObject);
        }

        if (enemyPrefabs.Count == 0)
        {
            Debug.LogError("No enemy prefabs assigned in GameManager!");
            EndGame();
            return;
        }

        currentEnemyIndex = (currentEnemyIndex + 1) % enemyPrefabs.Count;
        Enemy selectedEnemyPrefab = enemyPrefabs[currentEnemyIndex];

        currentEnemy = Instantiate(selectedEnemyPrefab, enemySpawnPoint.position, Quaternion.identity);
        Debug.Log("New enemy spawned: " + currentEnemy.EnemyName + " (Index: " + currentEnemyIndex + ")");
        UpdateEnemyUI();
    }

    public void PlayerAttack()
    {
        if (buttonClickSound != null && uiAudioSource != null)
        {
            uiAudioSource.PlayOneShot(buttonClickSound);
        }

        if (player.Health <= 0 || currentEnemy == null || currentEnemy.Health <= 0) return;

        SetUIButtonsActive(false);

        int playerDamage = player.Attack();
        currentEnemy.GetHit(playerDamage);

        UpdateEnemyUI();

        turnsSinceLastHeal++;
        UpdateHealButtonState();

        if (currentEnemy.Health <= 0)
        {
            gameStatusText.text = "Enemy defeated!";
        }
        else
        {
            Invoke("EnemyTurn", 1f);
        }
    }

    public void PlayerToggleShield()
    {
        if (buttonClickSound != null && uiAudioSource != null)
        {
            uiAudioSource.PlayOneShot(buttonClickSound);
        }

        player.ToggleShield();
        UpdatePlayerUI();
        SetUIButtonsActive(false);

        turnsSinceLastHeal++;
        UpdateHealButtonState();

        Invoke("EnemyTurn", 0.5f);
    }

    public void PlayerHeal()
    {
        if (player.Health <= 0 || currentEnemy == null || currentEnemy.Health <= 0) return;

        if (turnsSinceLastHeal < healCooldownTurns)
        {
            Debug.Log("Heal is on cooldown!");
            return;
        }

        if (buttonClickSound != null && uiAudioSource != null)
        {
            uiAudioSource.PlayOneShot(buttonClickSound);
        }

        player.Heal(healAmount);
        UpdatePlayerUI();

        turnsSinceLastHeal = 0;
        UpdateHealButtonState();

        SetUIButtonsActive(false);
        Invoke("EnemyTurn", 0.5f);
    }

    void EnemyTurn()
    {
        if (player.Health <= 0 || currentEnemy == null || currentEnemy.Health <= 0) return;

        gameStatusText.text = "Enemy's turn!";
        player.GetHit(currentEnemy.ActiveWeapon);

        UpdatePlayerUI();

        if (player.Health <= 0)
        {
            EndGame();
        }
        else
        {
            SetUIButtonsActive(true);
            gameStatusText.text = "Your turn!";
        }
    }

    public void EnemyDefeated(Enemy defeatedEnemy)
    {
        monstersKilledCount++;
        Debug.Log("Monsters killed: " + monstersKilledCount);

        gameStatusText.text = "Enemy defeated! Spawning next one in sequence...";
        Invoke("SpawnNextEnemyAfterDelay", 1.5f);
    }

    private void SpawnNextEnemyAfterDelay()
    {
        SpawnNextEnemyInSequence();
        gameStatusText.text = "New enemy appeared! Your turn!";
        SetUIButtonsActive(true);
        UpdateHealButtonState();
    }

    public void EndGame()
    {
        gameStatusText.text = "Game Over!";
        SetUIButtonsActive(false);
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (killedMonstersText != null)
            {
                killedMonstersText.text = "Monsters Killed: " + monstersKilledCount.ToString();
            }
        }
        if (currentEnemy != null)
        {
            currentEnemy.gameObject.SetActive(false);
        }

        Debug.Log("Game Over!");
        enemyImage.enabled = false;
        if (healButton != null) healButton.SetActive(false);
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SetUIButtonsActive(bool active)
    {
        attackButton.SetActive(active);
        shieldButton.SetActive(active);
        if (healButton != null) healButton.SetActive(active && turnsSinceLastHeal >= healCooldownTurns);
    }
}
