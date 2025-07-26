using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_IncomeModule : MonoBehaviour
{
    [Header("Income Module Text Elements")]
    public TextMeshProUGUI incomeNameText;
    public TextMeshProUGUI incomeCostText;
    public TextMeshProUGUI incomeTotalCost;
    public UI_FrequencyModule frequencyModule;

    [Header("Income Module Buttons")]
    public Button deleteButton;
    public Button editButton;

    [Header("Prefabs")]
    public GameObject incomeCreatorPrefab;

    private Income allocatedIncome;

    public void Initialize(Income income)
    {
        allocatedIncome = income;
        UpdateTextElements();
        deleteButton.onClick.AddListener(DeleteIncome);
        editButton.onClick.AddListener(EditIncome);
        frequencyModule.Initialize(income.frequency, income.customFrequency);
    }

    private void UpdateTextElements()
    {
        if (allocatedIncome == null)
            return;

        var name = allocatedIncome.name;
        var cost = allocatedIncome.cost;
        var total = allocatedIncome.total;

        incomeNameText.text = name;
        incomeCostText.text = "£" + cost.ToString("F2");
        incomeTotalCost.text = "£" + total.ToString("F2");
    }

    #region Editing / Deleting Income

    public void DeleteIncome()
    {
        if (allocatedIncome != null)
        {
            allocatedIncome.Delete();
        }
    }

    public void EditIncome()
    {
        // Open the income creator module with the allocated income for editing
        var eventPage = GetComponentInParent<UI_EventPage>();
        UI_IncomeCreator incomeCreator = Instantiate(incomeCreatorPrefab, eventPage.transform, false).GetComponent<UI_IncomeCreator>();
        incomeCreator.Initialize(null, allocatedIncome);
    }

    #endregion
}