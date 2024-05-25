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
    [SerializeField] private int swarmAmt;
    public List<GameObject> swarms;

    private bool hasStarted = false;
    private float spawnRange = 200f;
    private int currSwarmCount = 0;

    public void StartSwarm()
    {
        swarms.Clear();
        currSwarmCount = swarmAmt;

        for (int i = 0; i < swarmAmt; i++)
        {
            GameObject go = Instantiate(swarmPrefab);
            go.transform.position = Random.insideUnitSphere * Random.Range(100f, spawnRange);
            
            BoidSwarm script = go.GetComponent<BoidSwarm>();
            script.alignStrength = Random.Range(1f, 1f);
            script.cohesionStrength = Random.Range(2.25f, 2.75f);
            script.separationStrength = Random.Range(4.5f, 5f);

            swarms.Add(go);
        }

        UIManager.Instance.PlayerUI.UpdateRemainingEnemies(swarmAmt);
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
