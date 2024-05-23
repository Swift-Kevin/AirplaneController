using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoidSwarm : MonoBehaviour
{
    internal struct Taskforce
    {
        public Boid boidScript;
        public GameObject boidObj;
    }

    [SerializeField, Range(0f,5f)] private float alignStrength;
    [SerializeField, Range(0f,5f)] private float cohesionStrength;
    [SerializeField, Range(0f,5f)] private float separationStrength;
    
    [Seperator]
    [SerializeField] private float boidRadius = 50f;
    [SerializeField] private float maxBoidSpeed = 100f;
    [SerializeField, Min(0)] private int spawnAmount; 
    [SerializeField] private GameObject boidPrefab; 

    private List<Taskforce> Boids = new List<Taskforce>();
    private Vector3 avgPos, avgFwd;

    void Start()
    {
        for (int i = 0; i < spawnAmount;  ++i)
        {
            GameObject go = Instantiate(boidPrefab);
            go.transform.position = Random.insideUnitSphere;
            Taskforce task;
            
            task.boidScript = go.GetComponent<Boid>();
            task.boidObj = go;
            task.boidScript.Startup(maxBoidSpeed, boidRadius);

            Boids.Add(task);
        }
    }

    void Update()
    {
        CalculateAverages();

        foreach (var boid in Boids)
        {
            boid.boidScript.Velocity += (CalculateAlignmentAcceleration(boid.boidScript) + // add Calc curr alignment accel
                             CalculateCohesionAcceleration(boid.boidScript) +  // add Calc curr cohesion accel
                             CalculateSeparationAcceleration(boid.boidScript)) * // add calc curr separation accel
                             boid.boidScript.maxSpeed * // mult curr Max Speed
                             Time.deltaTime; // mult curr deltaTime

            if (boid.boidScript.Velocity.magnitude > boid.boidScript.maxSpeed)
            {
                boid.boidScript.Velocity.Normalize();
                boid.boidScript.Velocity *= boid.boidScript.maxSpeed;
            }

            boid.boidScript.UpdateStatus();
        }
        return;
    }

    private void CalculateAverages()
    {
        int _bC = Boids.Count;

        avgPos = Vector3.zero;
        avgFwd = Vector3.zero;

        // Accumulate all vals
        for (int i = 0; i < _bC; ++i)
        {
            avgFwd += Boids[i].boidScript.Velocity;
            avgPos += Boids[i].boidScript.Position;
        }

        // Get Avg
        avgFwd /= _bC;
        avgPos /= _bC;

        return;
    }

    private Vector3 CalculateAlignmentAcceleration(Boid boid)
    {
        Vector3 _alignAccel = avgFwd / boid.maxSpeed;

        if (_alignAccel.magnitude > 1)
            _alignAccel.Normalize();

        return _alignAccel * alignStrength;
    }

    private Vector3 CalculateCohesionAcceleration(Boid boid)
    {
        Vector3 _cohesion = avgPos - boid.Position;
        float _distance = _cohesion.magnitude;
        _cohesion.Normalize();

        if (_distance < boidRadius)
            _cohesion *= _distance / boidRadius;

        return _cohesion * cohesionStrength;
    }

    private Vector3 CalculateSeparationAcceleration(Boid boid)
    {
        // Init Vals
        Vector3 newVec = Vector3.zero;
        Vector3 newPos = Vector3.zero;
        float dist = 0.0f;
        float leway = 0.0f;

        foreach (var otherBoid in Boids)
        {
            newPos = boid.Position - otherBoid.boidScript.Position;
            dist = newPos.magnitude;
            leway = boid.safeRadius + otherBoid.boidScript.safeRadius;

            if (dist < leway)
            {
                newPos.Normalize();
                newVec += (newPos * (leway - dist) / leway);
            }
        }

        if (newVec.magnitude > 1.0f)
            newVec.Normalize();

        return newVec * separationStrength;
    }
}
