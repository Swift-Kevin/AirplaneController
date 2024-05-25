using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Transform camTransform;
    [SerializeField] private float maxShakeAmt = 5;
    [SerializeField] private float minShakeAmt = 0.25f;
    [SerializeField] private float shakeDuration;

    private bool runShaker = false;
    private float currShakeDur = 3;
    private Vector3 camOrig;
    private float curShakeAmt;

    void Start()
    {
        runShaker = false;
        camOrig = camTransform.localPosition;
        curShakeAmt = maxShakeAmt;
    }

    void Update()
    {
        if (runShaker)
        {
            if (currShakeDur > 0)
            {
                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, camOrig + Random.insideUnitSphere * curShakeAmt, Time.deltaTime * 3);
                currShakeDur -= Time.deltaTime;
                curShakeAmt = Mathf.Clamp(curShakeAmt - (Time.deltaTime), minShakeAmt, maxShakeAmt);
            }
            else
            {
                currShakeDur = shakeDuration;
                curShakeAmt = maxShakeAmt;
                camTransform.localPosition = camOrig;
                runShaker = false;
            }
        }
    }

    public void TurnOnShaker()
    {
        runShaker = true;
        currShakeDur = shakeDuration;
    }
}
