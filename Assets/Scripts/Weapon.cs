using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class Weapon : MonoBehaviour
{
    [SerializeField] protected int minDamage, maxDamage;
    [SerializeField] protected string weaponName;
    
    public string WeaponName
    {
        get { return weaponName; }
    }
    
    public int GetDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }
    
    public abstract void ApplyEffect(Character character);
}
