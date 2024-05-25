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
            go.transform.position = Random.insideUnitSphere * Random.Range(50, spawnRange);

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
            swarms.Clear();
            hasStarted = false;
        }
    }

    public void DecreaseSwarmCount()
    {
        currSwarmCount--;
        UIManager.Instance.PlayerUI.UpdateRemainingEnemies(currSwarmCount);
    }
}
