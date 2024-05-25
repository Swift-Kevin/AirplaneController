using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button quitBtn;

    void Start()
    {
        playBtn.onClick.AddListener(GameManager.Instance.PlayGame);
    }
}
