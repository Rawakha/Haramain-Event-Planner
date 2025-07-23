using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Event : MonoBehaviour
{
    public Event allocatedEvent;

    [Header("Event Page")]
    public TMP_InputField eventNameInput;

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
        // Event Name Setup
        eventNameInput.onValueChanged.AddListener(OnEventNameChanged);

        // Event Duration Setup
        UpdateDurationInputField();
        increaseButton.onClick.AddListener(IncreaseEventDuration);
        decreaseButton.onClick.AddListener(DecreaseEventDuration);
        eventDurationInput.onValueChanged.AddListener(OnDurationInputFieldChanged);

        UpdateTotals(allocatedEvent);
    }

    private void Update()
    {
        UpdateTotals(allocatedEvent);
    }

    #region Event Name

    private void UpdateNameInputField()
    {
        eventNameInput.text = allocatedEvent.eventName;
    }

    private void OnEventNameChanged(string newName)
    {
        allocatedEvent.eventName = newName;
        UpdateNameInputField();
    }

    #endregion

    #region Event Duration

    private void UpdateDurationInputField()
    {
        eventDurationInput.text = allocatedEvent.eventDuration.ToString();
    }

    private void IncreaseEventDuration()
    {
        allocatedEvent.eventDuration++;
        UpdateDurationInputField();
    }

    private void DecreaseEventDuration()
    {
        allocatedEvent.eventDuration = Mathf.Max(0, allocatedEvent.eventDuration - 1);
        UpdateDurationInputField();
    }

    private void OnDurationInputFieldChanged(string input)
    {
        if (int.TryParse(input, out int newValue))
        {
            allocatedEvent.eventDuration = Mathf.Max(0, newValue);
        }
        UpdateDurationInputField();
    }

    #endregion

    #region Totals
    public void UpdateTotals(Event currentEvent)
    {
        totalIncomeText.text = "£" + currentEvent.totalIncome.ToString("F2");
        totalExpensesText.text = "£" + currentEvent.totalExpenses.ToString("F2");
        balanceText.text = "£" + currentEvent.balance.ToString("F2");
    }
    #endregion
}