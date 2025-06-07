using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int health;
    [SerializeField] private Weapon activeWeapon;

    public Weapon ActiveWeapon
    {
        get { return activeWeapon; }
    }

    public virtual int Attack()
    {
        Debug.Log(name+"attacking!");
        return activeWeapon.GetDamage();
    }

    public void GetHit(int damage)
    {
        health -= damage;
        Debug.Log(name+" current health: " + health);
    }

    public void GetHit(Weapon weapon)
    {
        health-=weapon.GetDamage();
        Debug.Log("Got hit by:" + weapon.name);
        Debug.Log(name+" current health: " + health);
    }
}
