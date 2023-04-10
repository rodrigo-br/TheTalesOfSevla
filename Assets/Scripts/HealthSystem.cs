using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem
{
    int healthPoints;
    int healthMax;

    public HealthSystem(int maxHP)
    {
        this.healthMax = maxHP;
        healthPoints = maxHP;
    }

    public int GetHealthPoints()
    {
        return healthPoints;
    }

    public float GetHealthPercent()
    {
        return (float)healthPoints / healthMax;
    }

    public void Damage(int damageAmount)
    {
        healthPoints -= damageAmount;
        if (healthPoints < 0)
        {
            healthPoints = 0;
        }
    }

    public void Heal(int healAmount)
    {
        healthPoints += healAmount;
        if (healthPoints > healthMax)
        {
            healthPoints = healthMax;
        }
    }
}
