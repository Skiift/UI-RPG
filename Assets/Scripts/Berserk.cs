using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk : Enemy
{
    [SerializeField] private int AgressionGain = 5;

    public override int Attack()
    {
        agression += AgressionGain;
        return agression / 10;
    }
}
