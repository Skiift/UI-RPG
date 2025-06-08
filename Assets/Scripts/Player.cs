using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private int maxHealth;
    [SerializeField] private string charName;
    private bool isShieldActive = false;
    [SerializeField] private float shieldBreakChance = 0.3f;

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public string CharName
    {
        get { return charName; }
    }

    public bool IsShieldActive
    {
        get { return isShieldActive; }
        private set { isShieldActive = value; }
    }
    
    void Awake()
    {
        Debug.Log($"Player Awake: Initial health (from Inspector): {health}, Max Health (from Inspector): {maxHealth}");
    }

    void Start()
    {
        health = maxHealth;
        Debug.Log($"Player Start: health set to maxHealth. Current health: {health}, Max Health: {maxHealth}");
    }

    public override void GetHit(Weapon weapon)
    {
        int damage = weapon.GetDamage();
        if (isShieldActive)
        {
            Debug.Log(charName + "'s shield is active!");
            damage = Mathf.RoundToInt(damage * 0.5f);
            if (Random.value < shieldBreakChance)
            {
                ToggleShield();
                Debug.Log(charName + "'s shield broke!");
            }
        }
        base.GetHit(damage);
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        Debug.Log(charName + " healed for " + amount + ". Current health: " + health);
    }

    public void ToggleShield()
    {
        IsShieldActive = !IsShieldActive;
        Debug.Log(charName + "'s shield is now: " + (IsShieldActive ? "ACTIVE" : "INACTIVE"));
    }

    protected override void Die()
    {
        Debug.Log(charName + " has been defeated! Game Over.");
        GameManager.Instance.EndGame();
    }
}
