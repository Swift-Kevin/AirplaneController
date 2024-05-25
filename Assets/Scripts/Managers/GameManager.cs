using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SettingsSO settingsObj;
    [SerializeField] private Transform playerObj;

    public static float SafeZoneDistance = 300f;
    public SettingsSO SettingsObj => settingsObj;
    public Vector3 PlayerPos => playerObj.position;

    public void QuitApp()
    {
        Application.Quit();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void MouseUnlockShow()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void MouseLockHide()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReturnToMainMenu()
    {
        MouseUnlockShow(); // show cursor and unlock it from center
        UIManager.Instance.DisplayMainMenu();
    }

    public void GameOver()
    {
        // lost the game
        MouseUnlockShow();
        UIManager.Instance.GameLostMenu();
    }

    public void GameWon()
    {
        // won the game
        MouseUnlockShow();
        UIManager.Instance.GameWonMenu();
    }

    public void PlayGame()
    {
        UIManager.Instance.DisplayPlayerUI();
        InputManager.Instance.Actions.Enable();
        SwarmManager.Instance.StartSwarm();
    }
}
