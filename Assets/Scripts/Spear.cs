using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon
{
    [SerializeField] private float bleedChance = 0.2f;

    public override void ApplyEffect(Character character)
    {
        if (Random.value < bleedChance)
        {
            int bleedDamage = Random.Range(2, 5);
            character.GetHit(bleedDamage);
            Debug.Log(character.name + " got a bleed effect from " + WeaponName + " for " + bleedDamage + " damage!");
        }
    }
}
