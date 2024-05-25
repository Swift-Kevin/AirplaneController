using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private float safeZoneRange;
    [SerializeField] private float spawnZoneRadius = 500;
    [SerializeField, Range(0, 1000)] private float spawnAmt;
    [SerializeField] private List<Material> sphereMat;

    private void Start()
    {
        for (int i = 0; i < spawnAmt; ++i)
        {
            Vector3 point = Random.insideUnitSphere * spawnZoneRadius;
            GameObject go = Instantiate(m_Prefab, point, Random.rotation);

            float rndS = Random.Range(10f, 200f);
            Vector3 scale = new Vector3(rndS, rndS, rndS);
            go.transform.localScale = scale;

            go.GetComponent<MeshRenderer>().material = sphereMat[Random.Range(0, sphereMat.Count)];
        }
    }

}
