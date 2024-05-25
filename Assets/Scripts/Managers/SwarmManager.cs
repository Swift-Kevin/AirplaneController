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
    [Seperator]
    [SerializeField, Min(0)] private float spawnRange;
    [SerializeField] private float swarmAmounts;

    private bool hasStarted = false;

    public void StartSwarm()
    {
        for (int i = 0; i < swarmAmounts; i++)
        {
            GameObject go = Instantiate(swarmPrefab);
            go.transform.position = Random.insideUnitSphere * Random.Range(0, spawnRange);

            swarms.Add(go);
        }

        hasStarted = true;
    }

    private void Update()
    {
        if (swarms.Count <= 0 && hasStarted)
        {
            GameManager.Instance.GameWon();
        }
    }
}
