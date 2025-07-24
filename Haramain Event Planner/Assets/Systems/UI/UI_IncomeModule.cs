using TMPro;
using UnityEngine;

public class UI_IncomeModule : MonoBehaviour
{
    public TextMeshProUGUI incomeNameText;
    public TextMeshProUGUI incomeCostText;
    public TextMeshProUGUI incomeTotalCost;

    private Income allocatedIncome;

    public void Initialize(Income income)
    {
        allocatedIncome = income;
        UpdateElements();
    }

    private void UpdateElements()
    {
        if (allocatedIncome == null)
            return;

        var name = allocatedIncome.name;
        var cost = allocatedIncome.cost;
        var total = allocatedIncome.total;

        incomeNameText.text = name;
        incomeCostText.text = "�" + cost.ToString("F2");
        incomeTotalCost.text = "�" + total.ToString("F2");
    }
}