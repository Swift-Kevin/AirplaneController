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
    [SerializeField] private Animator pauseAnimator;
    
    [Seperator]
    [SerializeField] private GameObject objPlayerUI;
    
    [Seperator]
    [SerializeField] private GameObject objMainMenuUI;
    [SerializeField] private Animator mainmenuAnimator;

    [Seperator]
    [SerializeField] private PlayerUI energyUIScript;

    public PlayerUI PlayerUI => energyUIScript;

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
    }

    public void SetIsInGame(bool _status)
    {
        isInGame = _status;

        if (_status)
        {
            InputManager.Instance.Actions.Enable();
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
        isPauseOpened = true;
    }

    public void DisplayPlayerUI()
    {
        HideAllMenus();
        objPlayerUI?.SetActive(true);
        GameManager.Instance.MouseLockHide();
        isPauseOpened = false;
        isInGame = true;
    }

    public void DisplayMainMenu()
    {
        HideAllMenus();
        objMainMenuUI.SetActive(true);
        GameManager.Instance.MouseUnlockShow();
        SetIsInGame(false);
        mainmenuAnimator.SetTrigger("MainMenuTrigger");
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

        SetIsInGame(false);
    }

    public void GameWonMenu()
    {
        HideAllMenus();

        SetIsInGame(false);
    }
}
