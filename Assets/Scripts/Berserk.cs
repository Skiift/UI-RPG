using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk : Enemy
{
    [SerializeField] private int agressionGain = 5;
    
    public override int Attack()
    {
        agression += agressionGain;
        int damage = agression / 10;
        Debug.Log(EnemyName + " attacks with increased aggression! Damage: " + damage);
        return damage;
    }
}
