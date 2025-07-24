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

    [Header("Add Income/Expense Buttons")]
    public Button addIncomeButton;
    public Button addExpenseButton;
    [Header("Creation Modules")]
    public GameObject incomeCreatorPrefab;
    public GameObject expenseCreatorPrefab;

    [Header("Display Incomes/Expenses")]
    [Header("Tabs")]
    public Button incomeTabButton;
    public Button expenseTabButton;
    [Header("Tab Colors")]
    public Color selectedTabColor = Color.blue;
    public Color unselectedTabColor = Color.white;
    [Header("Text Colors")]
    public Color selectedTextColor = Color.white;
    public Color unselectedTextColor = Color.blue;

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

        // Income/Expense Buttons Setup
        SetupCreatorButtons();

        // Tab Setup
        SetupTabButton();

        // Update Totals
        UpdateTotals(allocatedEvent);
    }

    private void OnEnable()
    {
        allocatedEvent.OnEventUpdated += OnEventUpdated;
    }

    private void OnDisable()
    {
        allocatedEvent.OnEventUpdated -= OnEventUpdated;
    }

    private void Update()
    {
        UpdateTotals(allocatedEvent);
    }

    private void OnEventUpdated()
    {
        // Tab Button Text Updates
        UpdateIncomeButtonText();
        UpdateExpenseButtonText();
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

    #region Add Income/Expense Buttons

    private void SetupCreatorButtons()
    {
        addIncomeButton.onClick.AddListener(OpenIncomeCreator);
        addExpenseButton.onClick.AddListener(OpenExpenseCreator);
    }

    private void OpenIncomeCreator()
    {
        GameObject incomeCreator = Instantiate(incomeCreatorPrefab, transform, false);
        UI_IncomeCreator incomeCreatorScript = incomeCreator.GetComponent<UI_IncomeCreator>();
        incomeCreatorScript.Initialize(allocatedEvent);
    }

    private void OpenExpenseCreator()
    {
        Instantiate(expenseCreatorPrefab);
    }

    #endregion

    #region Income/Expense Tabs

    private void SetupTabButton()
    {
        incomeTabButton.onClick.AddListener(OpenIncomeTab);
        expenseTabButton.onClick.AddListener(OpenExpenseTab);

        OpenIncomeTab();
    }

    #region Opening and Closing Tabs
    private void OpenIncomeTab()
    {
        CloseExpenseTab();
        HandleButtonVisual(incomeTabButton, true);

        // Activate Tab Content
    }

    private void CloseIncomeTab()
    {
        HandleButtonVisual(incomeTabButton, false);

        // Deactivate Tab Content
    }

    private void OpenExpenseTab()
    {
        CloseIncomeTab();
        HandleButtonVisual(expenseTabButton, true);

        // Activate Tab Content
    }

    private void CloseExpenseTab()
    {
        HandleButtonVisual(expenseTabButton, false);

        // Deactivate Tab Content
    }
    #endregion

    #region Tab Button Visuals
    private void HandleButtonVisual(Button button, bool isOpen)
    {
        var image = button.GetComponent<Image>();
        var text = button.GetComponentInChildren<TextMeshProUGUI>();

        image.color = isOpen ? selectedTabColor : unselectedTabColor;
        text.color = isOpen ? selectedTextColor : unselectedTextColor;
    }

    private void UpdateIncomeButtonText()
    {
        var textModule = incomeTabButton.GetComponentInChildren<TextMeshProUGUI>();
        var incomeCount = allocatedEvent.incomes.Count;

        textModule.text = $"Incomes ({incomeCount})";
    }

    private void UpdateExpenseButtonText()
    {
        var textModule = expenseTabButton.GetComponentInChildren<TextMeshProUGUI>();
        var expenseCount = allocatedEvent.expenses.Count;

        textModule.text = $"Expenses ({expenseCount})";
    }
    #endregion

    #endregion
}