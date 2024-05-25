using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Slider volSlider;

    void Start()
    {
        resumeBtn.onClick.AddListener(UIManager.Instance.TogglePauseMenu);
        quitBtn.onClick.AddListener(UIManager.Instance.QuitButtonPressed);
        volSlider.onValueChanged.AddListener(AudioManager.Instance.UpdateMasterVolume);
    }
}
