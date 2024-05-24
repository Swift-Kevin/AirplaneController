using System;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IDamageable
{
    [Seperator]
    [SerializeField] private HealthPool health;

    public void TakeDamage(float damage)
    {
        health.Decrease(Time.deltaTime);
        if (!health.IsValid)
        {
            GameManager.Instance.GameOver();
        }
    }

    void Start()
    {
        health.SetMax();
    }
}
