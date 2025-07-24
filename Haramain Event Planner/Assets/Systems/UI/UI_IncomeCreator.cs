using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_IncomeCreator : MonoBehaviour
{
    [Header("Buttons")]
    public Button[] closeButtons;
    public Button createButton;

    [Header("Input Fields")]
    public TMP_InputField incomeNameInput;
    public TMP_InputField incomeAmountInput;

    private Event allocatedEvent;
    private Income allocatedIncome;

    public void Initialize(Event allocatedEvent = null, Income allocatedIncome = null)
    {
        this.allocatedEvent = allocatedEvent;
        this.allocatedIncome = allocatedIncome;

        foreach (Button button in closeButtons)
        {
            button.onClick.AddListener(CloseModule);
        }
        
        createButton.onClick.AddListener(CreateIncome);
        incomeNameInput.text = string.Empty;
        incomeAmountInput.text = string.Empty;
    }

    public void CloseModule()
    {
        Destroy(gameObject);
    }

    public void CreateIncome()
    {
        string incomeName = incomeNameInput.text;
        string incomeAmountText = incomeAmountInput.text;
        float incomeAmount = float.TryParse(incomeAmountText, out incomeAmount) ? incomeAmount : 0f;

        Income createdIncome = new Income
        {
            name = incomeName,
            cost = incomeAmount,
            frequency = Frequency.OneTime
        };

        allocatedEvent.AddIncome(createdIncome);
        CloseModule();
    }
}