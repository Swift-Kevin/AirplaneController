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

///            bool con1 = point.x >= -safeZoneRange && point.x <= safeZoneRange;
///            bool con2 = point.y >= -safeZoneRange && point.y <= safeZoneRange;
///            bool con3 = point.z >= -safeZoneRange && point.z <= safeZoneRange;
///
            //if (con1)
            //{
            //    point.x += 50;
            //}
            //else if (con2)
            //{
            //    point.y += 50;
            //}
            //else if (con3)
            //{
            //    point.z += 50;
            //}

            GameObject go = Instantiate(m_Prefab, point, Random.rotation);

            float rndS = Random.Range(0.5f, 200f);
            Vector3 scale = new Vector3(rndS, rndS, rndS);
            go.transform.localScale = scale;

            go.GetComponent<MeshRenderer>().material = sphereMat[Random.Range(0, sphereMat.Count)];
        }
    }

}
