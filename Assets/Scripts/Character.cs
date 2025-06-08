using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Character : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected Weapon activeWeapon;
    
    public int Health
    {
        get { return health; }
        protected set { health = value; }
    }
    
    public Weapon ActiveWeapon
    {
        get { return activeWeapon; }
        protected set { activeWeapon = value; }
    }
    
    public virtual int Attack()
    {
        Debug.Log(name + " attacking with " + activeWeapon.name + "!");
        return activeWeapon.GetDamage();
    }
    
    public void GetHit(int damage)
    {
        health -= damage;
        Debug.Log(name + " took " + damage + " damage. Current health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }
    
    public virtual void GetHit(Weapon weapon)
    {
        int damage = weapon.GetDamage();
        health -= damage;
        Debug.Log(name + " got hit by " + weapon.name + " for " + damage + " damage. Current health: " + health);
        weapon.ApplyEffect(this);
        if (health <= 0)
        {
            Die();
        }
    }
    
    protected abstract void Die();
    
}
