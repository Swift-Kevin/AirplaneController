using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipWeaponSystem : MonoBehaviour
{
    [SerializeField] private List<Transform> turretShootPoints;
    [SerializeField] private CustomTimer shootTimer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce;

    void Start()
    {
        shootTimer.OnStart += ShootTimer_OnStart;
        shootTimer.OnEnd += ShootTimer_OnEnd;

        InputManager.Instance.Actions.Shoot.performed += Shoot_performed;
    }

    private void OnDestroy()
    {
        shootTimer.OnStart -= ShootTimer_OnStart;
        shootTimer.OnEnd -= ShootTimer_OnEnd;

        InputManager.Instance.Actions.Shoot.performed -= Shoot_performed;
    }

    private void Shoot()
    {
        if (!shootTimer.RunTimer)
        {
            shootTimer.StartTimer();

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f)), out hit))
            {
                if (hit.collider != null)
                {
                    Transform rndTransform = turretShootPoints[UnityEngine.Random.Range(0, turretShootPoints.Count)];
                    GameObject go = Instantiate(bulletPrefab, rndTransform.position, Quaternion.identity);

                    go.transform.LookAt(hit.collider.transform.position);
                    go.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce, ForceMode.Impulse);
                }
            }
        }
    }


    private void Shoot_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!shootTimer.RunTimer)
        {
            Shoot();
        }
    }

    private void ShootTimer_OnEnd()
    {

    }

    private void ShootTimer_OnStart()
    {

    }
}
