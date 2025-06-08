using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] internal int agression = 10;
    [SerializeField] private string enemyName;
    [SerializeField] private Sprite enemySprite;
    
    [SerializeField] private AudioClip attackSound;
    private AudioSource enemyAudioSource;

    public string EnemyName
    {
        get { return enemyName; }
    }

    public Sprite EnemySprite
    {
        get { return enemySprite; }
    }
    
    void Awake()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        if (enemyAudioSource == null)
        {
            enemyAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }
   
    public override int Attack()
    {
        if (attackSound != null && enemyAudioSource != null)
        {
            enemyAudioSource.PlayOneShot(attackSound);
        }

        Debug.Log(name + " attacking with " + activeWeapon.name + "!");
        return activeWeapon.GetDamage();
    }

    protected override void Die()
    {
        Debug.Log(EnemyName + " has been defeated!");
        GameManager.Instance.EnemyDefeated(this);
    }
}
