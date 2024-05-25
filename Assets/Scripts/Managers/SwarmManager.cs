using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwarmManager : MonoBehaviour
{
    public static SwarmManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject swarmPrefab;
    [SerializeField] private List<GameObject> swarms;
    [SerializeField] private Vector2 swarmRange;

    private bool hasStarted = false;
    private float spawnRange = 200f;
    private int swarmAmounts;
    private int currSwarmCount = 0;

    public void StartSwarm()
    {
        swarmAmounts = Random.Range((int)swarmRange.x, (int)swarmRange.y);
        currSwarmCount = swarmAmounts;

        for (int i = 0; i < swarmAmounts; i++)
        {
            GameObject go = Instantiate(swarmPrefab);
            go.transform.position = Random.insideUnitSphere * Random.Range(100f, spawnRange);
            
            BoidSwarm script = go.GetComponent<BoidSwarm>();
            script.cohesionStrength = Random.Range(2.25f, 2.75f);
            script.alignStrength = Random.Range(0f, 0.25f);
            script.separationStrength = Random.Range(4.5f, 5f);

            swarms.Add(go);
        }

        UIManager.Instance.PlayerUI.UpdateRemainingEnemies(swarms.Count);
        hasStarted = true;
    }

    private void Update()
    {
        if (currSwarmCount <= 0 && hasStarted)
        {
            GameManager.Instance.GameWon();

            foreach (GameObject go in swarms)
            {
                Destroy(go);
            }

            swarms.Clear();
            hasStarted = false;
        }
    }

    public void DecreaseSwarmCount()
    {
        currSwarmCount--;
        UIManager.Instance.PlayerUI.UpdateRemainingEnemies(currSwarmCount);
    }

    public void ToggleSwarm(bool _status)
    {
        foreach (GameObject go in swarms)
        {
            go.SetActive(_status);
        }
    }
}
