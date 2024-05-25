using UnityEngine;

public class Wireframe : MonoBehaviour
{
    Material renderedMat;
    [SerializeField] private Color start;
    [SerializeField] private Color end;

    private void Start()
    {
        renderedMat = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (GameManager.Instance.PlayerScript.DistToDeath < 75f)
        {
            renderedMat.SetColor("_WireColor", Color.Lerp(start, end, GameManager.Instance.PlayerScript.DistToDeath / 75f));
            renderedMat.SetColor("_BaseColor", Color.Lerp(start, end, GameManager.Instance.PlayerScript.DistToDeath / 75f));
        }
        else
        {
            renderedMat.SetColor("_WireColor", start);
            renderedMat.SetColor("_BaseColor", start);
        }
    }
}
