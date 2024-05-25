using UnityEngine;

public class GameEndedUI : MonoBehaviour
{
    [SerializeField] private GameObject gameLost;
    [SerializeField] private GameObject gameWon;


    public void DisplayGameWon()
    {
        gameLost.SetActive(false);
        gameWon.SetActive(true);
    }

    public void DisplayGameLost()
    {
        gameLost.SetActive(true);
        gameWon.SetActive(false);
    }
}
