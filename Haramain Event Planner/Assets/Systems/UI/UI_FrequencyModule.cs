using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_FrequencyModule : MonoBehaviour
{
    [Header("Text")]
    public Image frequencyDisplay;
    public TextMeshProUGUI frequencyText;

    [Header("Custom Frequency Display")]
    public Image customFrequencyDisplay;
    public TextMeshProUGUI customFrequencyText;

    [Header("Frequency Options")]
    public Color defaultColor;
    public Color[] frequencyColors;

    public void Initialize(Frequency frequency, int customCount = 0)
    {
        frequencyDisplay.color = GetFrequencyColor(frequency);
        frequencyText.text = GetFrequencyString(frequency);

        if (frequency == Frequency.Custom)
        {
            customFrequencyDisplay.gameObject.SetActive(true);
            customFrequencyDisplay.color = GetFrequencyColor(frequency);
            customFrequencyText.text = customCount.ToString();
        }
        else
        {
            customFrequencyDisplay.gameObject.SetActive(false);
        }
    }

    public string GetFrequencyString(Frequency frequency)
    {
        switch (frequency)
        {
            case Frequency.OneTime:
                return "One Time";
            case Frequency.Daily:
                return "Daily";
            case Frequency.Custom:
                return "Custom";
            default:
                return "Unknown";
        }
    }

    public Color GetFrequencyColor(Frequency frequency)
    {
        switch (frequency)
        {
            case Frequency.OneTime:
                return frequencyColors[0];
            case Frequency.Daily:
                return frequencyColors[1];
            case Frequency.Custom:
                return frequencyColors[2];
            default:
                return defaultColor;
        }
    }
}
