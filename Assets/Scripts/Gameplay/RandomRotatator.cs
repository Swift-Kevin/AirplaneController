using UnityEngine;

public class RandomRotatator : MonoBehaviour
{
    Quaternion destRot;
    Quaternion oldRot;


    private void Start()
    {
        destRot = Random.rotation;
        oldRot = transform.rotation;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (destRot == transform.rotation)
        {
            destRot = Random.rotation;
            oldRot = transform.rotation;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, destRot, Time.deltaTime * 5);
    }
}
