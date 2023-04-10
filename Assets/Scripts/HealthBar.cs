using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public HealthSystem healthSystem = null;

    public void Setup(HealthSystem newHealthSystem)
    {
        this.healthSystem = newHealthSystem;
    }

    public void UpdateBar()
    {
        transform.GetComponentInChildren<Transform>().localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }
}
