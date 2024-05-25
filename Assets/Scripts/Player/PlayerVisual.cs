using System;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private CameraShaker shaker;

    [Seperator]
    [SerializeField] private GameObject particleBack;
    [SerializeField] private GameObject particleFrontLeft;
    [SerializeField] private GameObject particleFrontRight;
    [SerializeField] private GameObject particleBackLeft;
    [SerializeField] private GameObject particleBackRight;
    
    void Start()
    {
        // Turn all off
        TurnAllOff();
    }

    public void TurnAllOff()
    {
        particleBack.SetActive(false);
        particleFrontLeft.SetActive(false);
        particleFrontRight.SetActive(false);
        particleBackLeft.SetActive(false);
        particleBackRight.SetActive(false);
    }

    public void TurnOnRightSide()
    {
        particleBackRight.SetActive(true);
        particleFrontRight.SetActive(true);
    }

    public void TurnOffRightSide()
    {
        particleBackRight.SetActive(false);
        particleFrontRight.SetActive(false);
    }

    public void TurnOnLeftSide()
    {
        particleBackLeft.SetActive(true);
        particleFrontLeft.SetActive(true);
    }

    public void TurnOffLeftSide()
    {
        particleBackLeft.SetActive(false);
        particleFrontLeft.SetActive(false);
    }

    public void TurnOnBack()
    {
        shaker.TurnOnShaker();
        particleBack.SetActive(true);
    }

    public void TurnOffBack()
    {
        particleBack.SetActive(false);
    }

}
