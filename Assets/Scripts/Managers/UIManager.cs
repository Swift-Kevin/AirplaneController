using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Button btn_Quit;

    [Seperator]
    [SerializeField] private GameObject objPauseUI;
    [SerializeField] private GameObject objPlayerUI;
    [SerializeField] private GameObject objMainMenuUI;

    [Seperator]
    [SerializeField] private Animator pauseAnimator;
    [SerializeField] private Animator mainmenuAnimator;

    [Seperator]
    [SerializeField] private PlayerUI playerUIScript;
    [SerializeField] private GameEndedUI gameEndedUI;

    public PlayerUI PlayerUI => playerUIScript;

    public bool isInGame = false;
    public bool isPauseOpened = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        btn_Quit.onClick.AddListener(QuitButtonPressed);

        DisplayMainMenu();
    }

    public void QuitButtonPressed()
    {
        GameManager.Instance.QuitApp();
    }

    public void HideAllMenus()
    {
        objPauseUI.SetActive(false);
        objPlayerUI.SetActive(false);
        objMainMenuUI.SetActive(false);
        gameEndedUI.TurnOffUI();
    }

    public void SetIsInGame(bool _status)
    {
        isInGame = _status;

        if (_status)
        {
            DisplayPlayerUI();
        }
        else
        {
            InputManager.Instance.Actions.Disable();
        }
    }

    public void DisplayPauseMenu()
    {
        HideAllMenus();
        objPauseUI?.SetActive(true);
        GameManager.Instance.MouseUnlockShow();
        InputManager.Instance.Actions.Disable();
        isPauseOpened = true;
        SwarmManager.Instance.ToggleSwarm(false);
    }

    public void DisplayPlayerUI()
    {
        HideAllMenus();
        objPlayerUI?.SetActive(true);
        GameManager.Instance.MouseLockHide();
        InputManager.Instance.Actions.Enable();
        SwarmManager.Instance.ToggleSwarm(true);
        isPauseOpened = false;
        isInGame = true;
    }

    public void DisplayMainMenu()
    {
        HideAllMenus();
        objMainMenuUI.SetActive(true);
        GameManager.Instance.MouseUnlockShow();
        SetIsInGame(false);
    }

    public void TogglePauseMenu()
    {
        if (!isInGame)
            return;

        if (objPauseUI.activeSelf)
        {
            DisplayPlayerUI();
        }
        else
        {
            DisplayPauseMenu();
        }
    }

    public void GameLostMenu()
    {
        HideAllMenus();
        InputManager.Instance.Actions.Disable();
        gameEndedUI.DisplayGameLost();
        SetIsInGame(false);
    }

    public void GameWonMenu()
    {
        HideAllMenus();
        InputManager.Instance.Actions.Disable();
        gameEndedUI.DisplayGameWon();
        SetIsInGame(false);
    }

    public void UpdateGameTimer(int _remain)
    {
        playerUIScript.UpdateRemainingTime(_remain);
    }
}
