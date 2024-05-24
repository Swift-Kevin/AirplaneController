using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SettingsSO settingsObj;
    public SettingsSO SettingsObj => settingsObj;

    public void QuitApp()
    {
        Application.Quit();
    }

    private void Start()
    {
        MouseLockHide();
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
        UIManager.Instance.SetIsInGame(false); // disable turning on pause menu
    }

    public void GameOver()
    {

    }
}
