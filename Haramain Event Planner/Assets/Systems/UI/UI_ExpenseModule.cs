using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ExpenseModule : MonoBehaviour
{
    [Header("Expense Module Text Elements")]
    public TextMeshProUGUI expenseNameText;
    public TextMeshProUGUI expenseCostText;
    public TextMeshProUGUI expenseTotalCost;
    public UI_FrequencyModule frequencyModule;

    [Header("Expense Module Buttons")]
    public Button deleteButton;
    public Button editButton;

    [Header("Prefabs")]
    public GameObject expenseCreatorPrefab;
    private Expense allocatedExpense;

    public void Initialize(Expense expense)
    {
        allocatedExpense = expense;
        UpdateTextElements();
        deleteButton.onClick.AddListener(DeleteExpense);
        editButton.onClick.AddListener(EditExpense);
        frequencyModule.Initialize(expense.frequency, expense.customFrequency);
    }

    private void UpdateTextElements()
    {
        if (allocatedExpense == null)
            return;
        var name = allocatedExpense.name;
        var cost = allocatedExpense.cost;
        var total = allocatedExpense.total;
        expenseNameText.text = name;
        expenseCostText.text = "£" + cost.ToString("F2");
        expenseTotalCost.text = "£" + total.ToString("F2");
    }

    #region Editing / Deleting Expense
    public void DeleteExpense()
    {
        if (allocatedExpense != null)
        {
            allocatedExpense.Delete();
        }
    }

    public void EditExpense()
    {
        // Open the expense creator module with the allocated expense for editing
        var eventPage = GetComponentInParent<UI_EventPage>();
        UI_ExpenseCreator expenseCreator = Instantiate(expenseCreatorPrefab, eventPage.transform, false).GetComponent<UI_ExpenseCreator>();
        expenseCreator.Initialize(null, allocatedExpense);
    }
    #endregion
}