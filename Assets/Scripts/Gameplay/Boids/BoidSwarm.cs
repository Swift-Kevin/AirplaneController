using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoidSwarm : MonoBehaviour, IDamageable
{
    internal struct Taskforce
    {
        public Boid boidScript;
        public GameObject boidObj;
    }

    [Range(0f, 5f)] public float alignStrength;
    [Range(0f, 5f)] public float cohesionStrength;
    [Range(0f, 5f)] public float separationStrength;

    [Seperator]
    [SerializeField] private float boidRadius = 50f;
    [SerializeField] private float maxBoidSpeed = 100f;
    [SerializeField] private float bufferRadius = 3f;
    [SerializeField, Min(0)] private int spawnAmount;
    [SerializeField, Min(0)] private float distanceToAttackPlayer;

    [Seperator]
    [SerializeField] private GameObject boidPrefab;
    [SerializeField] private ParticleSystem hitParticles;
    [SerializeField] private SphereCollider coll;

    private List<Taskforce> Boids = new List<Taskforce>();
    private Vector3 avgPos, avgFwd;

    void Start()
    {
        for (int i = 0; i < spawnAmount; ++i)
        {
            GameObject go = Instantiate(boidPrefab, transform.position, Quaternion.identity);
            go.transform.position = Random.insideUnitSphere + transform.position;
            Taskforce task;

            task.boidScript = go.GetComponent<Boid>();
            task.boidObj = go;
            task.boidScript.Startup(maxBoidSpeed, boidRadius);

            Boids.Add(task);
        }
    }

    void FixedUpdate()
    {
        if (UIManager.Instance.isInGame)
        {
            CalculateAverages();
            float longestRadius = 0.0f;

            foreach (var boid in Boids)
            {
                boid.boidScript.Velocity += (CalculateAlignmentAcceleration(boid.boidScript) +
                    CalculateCohesionAcceleration(boid.boidScript) +
                    CalculateSeparationAcceleration(boid.boidScript)) *
                    boid.boidScript.maxSpeed * Time.deltaTime;

                if (boid.boidScript.Velocity.magnitude > boid.boidScript.maxSpeed)
                {
                    boid.boidScript.Velocity.Normalize();
                    boid.boidScript.Velocity *= boid.boidScript.maxSpeed;
                }

                boid.boidScript.UpdateStatus();

                float currRadius = Vector3.Distance(boid.boidScript.Position, avgPos);

                if (currRadius > longestRadius)
                    longestRadius = currRadius;

            }

            coll.radius = longestRadius + bufferRadius;
            transform.position = avgPos;
        }
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


        avgPos = Vector3.ClampMagnitude(avgPos, 300);
        if (Vector3.Distance(avgPos, GameManager.Instance.PlayerPos) < distanceToAttackPlayer)
        {
            Vector3 dirToPlayer = GameManager.Instance.PlayerPos - avgFwd;
            avgFwd = Vector3.Lerp(avgFwd, dirToPlayer, Time.deltaTime);
        }

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

    public void TakeDamage()
    {
        Taskforce task = Boids[Boids.Count - 1];
        task.boidScript = null;
        task.boidObj.SetActive(false);
        Boids[Boids.Count - 1] = task;
        Boids.Remove(Boids[Boids.Count - 1]);

        if (Boids.Count <= 0)
        {
            // Double check for an odd number of boids
            SwarmManager.Instance.DecreaseSwarmCount();

            foreach (var c in Boids)
            {
                Destroy(c.boidObj);
            }

            gameObject.SetActive(false);
        }
        else
        {
            task = Boids[Boids.Count - 1];
            task.boidScript = null;
            task.boidObj.SetActive(false);
            Boids[Boids.Count - 1] = task;
            Boids.Remove(Boids[Boids.Count - 1]);

            if (Boids.Count <= 0)
            {
                SwarmManager.Instance.DecreaseSwarmCount();

                foreach (var c in Boids)
                {
                    Destroy(c.boidObj);
                }

                gameObject.SetActive(false);
            }

            var shape = hitParticles.shape;
            shape.radius = coll.radius;

            hitParticles.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage();
            }
        }
    }
}
