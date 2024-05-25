using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Slider volSlider;
    [SerializeField] private Slider sensSlider;

    void Start()
    {
        resumeBtn.onClick.AddListener(UIManager.Instance.TogglePauseMenu);
        quitBtn.onClick.AddListener(GameManager.Instance.ReturnToMainMenu);
        volSlider.onValueChanged.AddListener(AudioManager.Instance.UpdateMasterVolume);
        sensSlider.onValueChanged.AddListener(GameManager.Instance.UpdateMouseSens);

        volSlider.value = GameManager.Instance.SettingsObj.masterVol;
        sensSlider.value = GameManager.Instance.SettingsObj.mouseSensitivity;
    }
}
