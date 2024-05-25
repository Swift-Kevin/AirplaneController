using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image energyBar;
    [SerializeField] private GameObject warningUI;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI remainingEnemies;
    [SerializeField] private TextMeshProUGUI remainingTime;


    public void UpdateEnergyBar(float val)
    {
        energyBar.fillAmount = val;
    }

    public void UpdateDistanceText(int val)
    {
        distanceText.text = val.ToString();
    }

    public void ToggleDistanceWarning(bool _status)
    {
        warningUI.SetActive(_status);
    }

    public void UpdateRemainingEnemies(int count)
    {
        remainingEnemies.text = count.ToString();
    }

    public void UpdateRemainingTime(int time)
    {
        remainingTime.text = time.ToString() + "s\n";
    }
}
