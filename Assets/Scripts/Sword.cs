using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private float stunChance = 0.1f;

    public override void ApplyEffect(Character character)
    {
        if (character is Enemy enemy && Random.value < stunChance)
        {
            Debug.Log(enemy.name + " was stunned by " + WeaponName + "!");
        }
    }
}
