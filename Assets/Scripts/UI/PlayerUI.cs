using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image energyBar;
    [SerializeField] private GameObject warningUI;
    [SerializeField] private TextMeshProUGUI distanceText;


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
}
