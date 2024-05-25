using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button quitBtn;

    void Start()
    {
        resumeBtn.onClick.AddListener(UIManager.Instance.TogglePauseMenu);
        quitBtn.onClick.AddListener(UIManager.Instance.QuitButtonPressed);
    }
}
