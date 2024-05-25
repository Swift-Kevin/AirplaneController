using System;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IDamageable
{
    [Seperator]
    [SerializeField] private HealthPool health;
    [SerializeField] private ParticleSystem hitParticles;

    public void TakeDamage()
    {
        health.Decrease(Time.deltaTime);

        hitParticles.Play();
        UIManager.Instance.PlayerUI.UpdateEnergyBar(health.Percent);

        if (!health.IsValid)
        {
            GameManager.Instance.GameOver();
        }
    }

    void Start()
    {
        health.SetMax();
        UIManager.Instance.PlayerUI.UpdateEnergyBar(health.Percent);
    }
}
