using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerVisual visuals;
    [SerializeField] private float forceAmnt;
    Vector2 inp;

    private void Update()
    {
        inp = InputManager.Instance.MoveVec;
    }

    void FixedUpdate()
    {
        Vector3 force = (Camera.main.transform.forward * inp.y * forceAmnt) + (Camera.main.transform.right * inp.x * forceAmnt);
        rb.AddForce(force, ForceMode.Impulse);
        rb.angularVelocity = Vector3.zero;

        ToggleVisuals();
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
