using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobEffect : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float bobRate = 1;
    [SerializeField] float amplitude = 0.25f;
    [SerializeField] private bool rotate = true;

    private Vector3 startingPos;
    private float randomOffset;

    private void Start()
    {
        startingPos = transform.position;
        randomOffset = Random.Range(-Mathf.PI, Mathf.PI);
    }

    private void Update()
    {
        if (rotate)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 50f);
        }

        float val = Mathf.Sin(Time.time * bobRate + randomOffset) * amplitude;
        transform.position = new Vector3(transform.position.x, startingPos.y + val, transform.position.z);
    }
}
