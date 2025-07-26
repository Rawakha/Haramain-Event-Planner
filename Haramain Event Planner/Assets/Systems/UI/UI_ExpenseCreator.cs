using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ExpenseCreator : MonoBehaviour
{
    [Header("Buttons")]
    public Button[] closeButtons;
    public Button createButton;

    [Header("Input Fields")]
    public TMP_InputField expenseNameInput;
    public TMP_InputField expenseAmountInput;

    [Header("Frequency Dropdowns")]
    public TMP_Dropdown frequencyDropdown;
    public TMP_InputField frequencyAmountInput;

    private Event allocatedEvent;
    private Expense allocatedExpense;

    public void Initialize(Event allocatedEvent = null, Expense allocatedExpense = null)
    {
        var animator = GetComponentInChildren<UI_ModuleAnimator>();
        if (animator != null)
        {
            animator.Show();
        }

        this.allocatedEvent = allocatedEvent;
        this.allocatedExpense = allocatedExpense;

        foreach (Button button in closeButtons)
        {
            button.onClick.AddListener(CloseModule);
        }

        if (allocatedEvent == null && allocatedExpense != null)
        {
            createButton.onClick.AddListener(EditExpense);
            createButton.GetComponentInChildren<TextMeshProUGUI>().text = "Edit Expense";
            expenseNameInput.text = allocatedExpense.name;
            expenseAmountInput.text = allocatedExpense.cost.ToString("F2");
            frequencyDropdown.value = (int)allocatedExpense.frequency;
            frequencyAmountInput.text = allocatedExpense.customFrequency.ToString();
        }
        else
        {
            createButton.onClick.AddListener(CreateExpense);
            expenseNameInput.text = string.Empty;
            expenseAmountInput.text = string.Empty;
            frequencyDropdown.value = 0;
            frequencyAmountInput.text = "0";
        }

        SetupFrequencyDropdown();
    }

    private void CloseModule()
    {
        var animator = GetComponentInChildren<UI_ModuleAnimator>();
        if (animator != null)
        {
            animator.Delete();
            return;
        }

        Destroy(gameObject);
    }

    public void CreateExpense()
    {
        string expenseName = expenseNameInput.text != string.Empty ? expenseNameInput.text : "New Expense";
        string expenseAmountText = expenseAmountInput.text;
        float expenseAmount = float.TryParse(expenseAmountText, out expenseAmount) ? expenseAmount : 0f;
        int frequencyIndex = frequencyDropdown.value;
        string customFrequencyText = frequencyAmountInput.text;
        int customFrequency = int.TryParse(customFrequencyText, out customFrequency) ? customFrequency : 0;

        Expense createdExpense = new Expense
        {
            name = expenseName,
            cost = expenseAmount,
            frequency = (Frequency)frequencyIndex,
            customFrequency = customFrequency
        };

        allocatedEvent?.AddExpense(createdExpense);
        CloseModule();
    }

    public void EditExpense()
    {
        string expenseName = expenseNameInput.text;
        string expenseAmountText = expenseAmountInput.text;
        float expenseAmount = float.TryParse(expenseAmountText, out expenseAmount) ? expenseAmount : 0f;
        int frequencyIndex = frequencyDropdown.value;
        string customFrequencyText = frequencyAmountInput.text;
        int customFrequency = int.TryParse(customFrequencyText, out customFrequency) ? customFrequency : 0;
        allocatedExpense.EditExpense(expenseName, expenseAmount, (Frequency)frequencyIndex, customFrequency);
        CloseModule();
    }

    private void SetupFrequencyDropdown()
    {
        frequencyDropdown.ClearOptions();
        List<string> options = new List<string> { "One-time", "Daily", "Custom"};
        frequencyDropdown.AddOptions(options);
        frequencyDropdown.value = 0; // Default to One-time
        frequencyDropdown.RefreshShownValue();
        
        frequencyAmountInput.transform.parent.gameObject.SetActive(false);
        frequencyDropdown.onValueChanged.AddListener(OnFrequencyChanged);
    }

    private void OnFrequencyChanged(int index)
    {
        if (index == (int)Frequency.Custom)
        {
            frequencyAmountInput.transform.parent.gameObject.SetActive(true);
            frequencyAmountInput.text = allocatedExpense != null ? allocatedExpense.customFrequency.ToString() : 0.ToString();
        }
        else
        {
            frequencyAmountInput.transform.parent.gameObject.SetActive(false);
        }
    }
}
