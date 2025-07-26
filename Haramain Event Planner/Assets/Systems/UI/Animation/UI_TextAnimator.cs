using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI_TextAnimator : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    [Header("Animation Settings")]
    public float animationDuration = 0.1f;
    public ScrambleMode scrambleMode = ScrambleMode.None;

    private float currentValue = 0f;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(string newText)
    {
        if (textMesh.text == newText)
        {
            return;
        }

        DOTween.Kill(textMesh);
        textMesh.DOText(newText, animationDuration, true, scrambleMode);
    }

    public void UpdateNumericalText(float newValue)
    {
        if (textMesh.text == newValue.ToString("F2"))
        {
            return;
        }

        DOTween.Kill(textMesh);
        DOTween.To(() => currentValue, value =>
        {
            currentValue = value;
            textMesh.text = "£" + value.ToString("F2");
        },
        newValue, animationDuration).SetId(textMesh).SetEase(Ease.Linear);
    }
}