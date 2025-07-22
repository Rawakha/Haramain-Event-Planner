using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Event : MonoBehaviour
{
    public Event allocatedEvent;

    [Header("Event Page")]
    [Header("Event Duration")]
    public TMP_InputField eventDurationInput;
    public Button increaseButton;
    public Button decreaseButton;

    [Header("Totals")]
    public TextMeshProUGUI totalIncomeText;
    public TextMeshProUGUI totalExpensesText;
    public TextMeshProUGUI balanceText;

    private void Awake()
    {
        allocatedEvent = AppManager.Instance.currentEvent;
    }

    private void Start()
    {
        UpdateInputField();

        increaseButton.onClick.AddListener(IncreaseEventDuration);
        decreaseButton.onClick.AddListener(DecreaseEventDuration);

        eventDurationInput.onValueChanged.AddListener(OnInputFieldChanged);
        UpdateTotals(allocatedEvent);
    }

    private void Update()
    {
        UpdateTotals(allocatedEvent);
    }

    #region Event Duration

    private void UpdateInputField()
    {
        eventDurationInput.text = allocatedEvent.eventDuration.ToString();
    }

    private void IncreaseEventDuration()
    {
        allocatedEvent.eventDuration++;
        UpdateInputField();
    }

    private void DecreaseEventDuration()
    {
        allocatedEvent.eventDuration = Mathf.Max(0, allocatedEvent.eventDuration - 1);
        UpdateInputField();
    }

    private void OnInputFieldChanged(string input)
    {
        if (int.TryParse(input, out int newValue))
        {
            allocatedEvent.eventDuration = Mathf.Max(0, newValue);
        }
        UpdateInputField();
    }

    #endregion

    public void UpdateTotals(Event currentEvent)
    {
        totalIncomeText.text = "£" + currentEvent.totalIncome.ToString("F2");
        totalExpensesText.text = "£" + currentEvent.totalExpenses.ToString("F2");
        balanceText.text = "£" + currentEvent.balance.ToString("F2");
    }
}