using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_IncomeCreator : MonoBehaviour
{
    [Header("Buttons")]
    public Button[] closeButtons;
    public Button createButton;

    [Header("Input Fields")]
    public TMP_InputField incomeNameInput;
    public TMP_InputField incomeAmountInput;
    [Header("Frequency Dropdowns")]
    public TMP_Dropdown frequencyDropdown;
    public TMP_InputField frequencyAmountInput;

    private Event allocatedEvent;
    private Income allocatedIncome;

    public void Initialize(Event allocatedEvent = null, Income allocatedIncome = null)
    {
        if (TryGetComponent<UI_ModuleAnimator>(out UI_ModuleAnimator animator))
        {
            animator.Show();
        }

        this.allocatedEvent = allocatedEvent;
        this.allocatedIncome = allocatedIncome;

        foreach (Button button in closeButtons)
        {
            button.onClick.AddListener(CloseModule);
        }
        
        if (allocatedEvent == null && allocatedIncome != null)
        {
            createButton.onClick.AddListener(EditIncome);
            createButton.GetComponentInChildren<TextMeshProUGUI>().text = "Edit Income";
            incomeNameInput.text = allocatedIncome.name;
            incomeAmountInput.text = allocatedIncome.cost.ToString("F2");
        }
        else
        {
            createButton.onClick.AddListener(CreateIncome);
            incomeNameInput.text = string.Empty;
            incomeAmountInput.text = string.Empty;
            frequencyDropdown.value = 0;
            frequencyAmountInput.text = "0";
        }

        SetupFrequencyDropdown();
    }

    private void CloseModule()
    {
        if (TryGetComponent<UI_ModuleAnimator>(out UI_ModuleAnimator animator))
        {
            animator.Delete();
            return;
        }

        Destroy(gameObject);
    }

    public void CreateIncome()
    {
        string incomeName = incomeNameInput.text;
        string incomeAmountText = incomeAmountInput.text;
        float incomeAmount = float.TryParse(incomeAmountText, out incomeAmount) ? incomeAmount : 0f;
        int frequencyIndex = frequencyDropdown.value;
        string customFrequencyText = frequencyAmountInput.text;
        int customFrequency = int.TryParse(customFrequencyText, out customFrequency) ? customFrequency : 0;

        Income createdIncome = new Income
        {
            name = incomeName,
            cost = incomeAmount,
            frequency = (Frequency)frequencyIndex,
            customFrequency = customFrequency
        };

        allocatedEvent.AddIncome(createdIncome);
        CloseModule();
    }

    public void EditIncome()
    {
        string incomeName = incomeNameInput.text;
        string incomeAmountText = incomeAmountInput.text;
        float incomeAmount = float.TryParse(incomeAmountText, out incomeAmount) ? incomeAmount : 0f;
        int frequencyIndex = frequencyDropdown.value;
        string customFrequencyText = frequencyAmountInput.text;
        int customFrequency = int.TryParse(customFrequencyText, out customFrequency) ? customFrequency : 0;
        allocatedIncome.EditIncome(incomeName, incomeAmount, (Frequency)frequencyIndex, customFrequency);
        CloseModule();
    }

    #region Frequency Dropdown

    private void SetupFrequencyDropdown()
    {
        frequencyDropdown.ClearOptions();
        frequencyDropdown.AddOptions(new List<string> { "One Time", "Daily", "Custom" });
        frequencyDropdown.onValueChanged.AddListener(OnFrequencyChanged);

        if (allocatedIncome != null)
        {
            frequencyDropdown.value = (int)allocatedIncome.frequency;
        }
        else
        {
            frequencyDropdown.value = 0;
            frequencyAmountInput.transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnFrequencyChanged(int index)
    {
        if (index == (int)Frequency.Custom)
        {
            frequencyAmountInput.transform.parent.gameObject.SetActive(true);
            frequencyAmountInput.text = allocatedIncome != null ? allocatedIncome.customFrequency.ToString() : 0.ToString();
        }
        else
        {
            frequencyAmountInput.transform.parent.gameObject.SetActive(false);
        }
    }

    #endregion
}