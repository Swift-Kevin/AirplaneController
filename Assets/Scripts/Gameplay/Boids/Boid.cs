using System;
using System.Security.Cryptography;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float maxSpeed;
    public float safeRadius;
    public Vector3 Velocity;

    public Vector3 Position { get => transform.position; set { transform.position = value; } }

    public void UpdateStatus()
    {
        if (Velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            Velocity = Vector3.Normalize(Velocity);
            Velocity *= maxSpeed;
        }

        transform.rotation = Quaternion.LookRotation(Velocity);
        Position += Velocity * Time.deltaTime;
    }

    public void Startup(float _maxSpeed, float _safeRadius)
    {
        maxSpeed = _maxSpeed;
        safeRadius = _safeRadius;

        Velocity = Vector3.zero;
    }
}
