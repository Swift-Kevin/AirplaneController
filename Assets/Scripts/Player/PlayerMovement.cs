using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerVisual visuals;
    [SerializeField] private float forceAmnt;
    [SerializeField] private PlayerBase playerBaseScript;

    Vector2 inp;
    private int distanceFromCenter;

    private void Update()
    {
        inp = InputManager.Instance.MoveVec;
    }

    void FixedUpdate()
    {
        if (UIManager.Instance.isInGame && playerBaseScript.IsAlive)
        {
            Vector3 force = (Camera.main.transform.forward * inp.y * forceAmnt) + (Camera.main.transform.right * inp.x * forceAmnt);
            rb.AddForce(force, ForceMode.Impulse);
            rb.angularVelocity = Vector3.zero;

            UpdateDistances();

            ToggleVisuals();
        }
    }

    private void UpdateDistances()
    {
        distanceFromCenter = (int)Vector3.Distance(transform.position, Vector3.zero);
        float distToDeathZone = GameManager.SafeZoneDistance - distanceFromCenter;
        
        if (distToDeathZone <= 0)
        {
            playerBaseScript.TakeDamage();
        }
        else if (distToDeathZone <= 100f)
        {
            UIManager.Instance.PlayerUI.ToggleDistanceWarning(true);
        }
        else
        {
            UIManager.Instance.PlayerUI.ToggleDistanceWarning(false);
        }

        UIManager.Instance.PlayerUI.UpdateDistanceText((int)distToDeathZone);
    }

    private void ToggleVisuals()
    {
        if (inp.x < 0)
        {
            visuals.TurnOnRightSide();
            visuals.TurnOffLeftSide();
        }
        else if (inp.x > 0)
        {
            visuals.TurnOnLeftSide();
            visuals.TurnOffRightSide();
        }
        else
        {
            visuals.TurnOffRightSide();
            visuals.TurnOffLeftSide();
        }

        if (inp.y > 0)
        {

            visuals.TurnOnBack();
        }
        else
        {
            visuals.TurnOffBack();
        }
    }
}
