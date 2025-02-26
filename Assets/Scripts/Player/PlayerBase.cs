using System;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IDamageable
{
    [Seperator]
    [SerializeField] private HealthPool health;
    [SerializeField] private ParticleSystem hitParticles;
    [SerializeField] private PlayerMovement moveScript;

    public bool IsAlive => health.IsValid;
    public float DistToDeath => moveScript.distToDeathZone;

    public void TakeDamage()
    {
        if (UIManager.Instance.isInGame)
        {
            health.Decrease(Time.deltaTime * 2);

            hitParticles.Play();
            UIManager.Instance.PlayerUI.UpdateEnergyBar(health.Percent);

            if (!health.IsValid)
            {
                transform.position = Vector3.zero;
                GameManager.Instance.GameOver();
            }
        }
    }

    void Start()
    {
        health.SetMax();
        UIManager.Instance.PlayerUI.UpdateEnergyBar(health.Percent);
        health.OnDepleted += Health_OnDepleted;
    }

    private void Health_OnDepleted()
    {
        GameManager.Instance.GameOver();
    }

    public void ResetStats()
    {
        health.SetMax();
        UIManager.Instance.PlayerUI.UpdateEnergyBar(health.Percent);
        transform.position = Vector3.zero;
    }
}
