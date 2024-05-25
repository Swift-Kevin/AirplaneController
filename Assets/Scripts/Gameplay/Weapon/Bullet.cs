using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float waitToDestroyTime = 3f;
    [SerializeField] private AudioSource audSource;

    private void Start()
    {
        audSource.Play();
        Destroy(gameObject, waitToDestroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage();
            }
        }
    }
}
