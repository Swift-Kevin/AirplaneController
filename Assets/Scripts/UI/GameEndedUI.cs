using UnityEngine;
using UnityEngine.UI;

public class GameEndedUI : MonoBehaviour
{
    [SerializeField] private GameObject gameLost;
    [SerializeField] private GameObject gameWon;
    
    [Seperator]
    [SerializeField] private Button replayWon;
    [SerializeField] private Button mainMenuWon;
    
    [Seperator]
    [SerializeField] private Button replayLost;
    [SerializeField] private Button mainMenuLost;

    private void Start()
    {
        replayWon.onClick.AddListener(GameManager.Instance.PlayGame);
        replayLost.onClick.AddListener(GameManager.Instance.PlayGame);

        mainMenuWon.onClick.AddListener(GameManager.Instance.ReturnToMainMenu);
        mainMenuLost.onClick.AddListener(GameManager.Instance.ReturnToMainMenu);
    }

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

    public void TurnOffUI()
    {
        gameLost.SetActive(false);
        gameWon.SetActive(false);
    }
}
