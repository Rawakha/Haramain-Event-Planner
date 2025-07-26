using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class UI_EventPage : MonoBehaviour
{
    [Header("Event Page")]
    public Event allocatedEvent;
    public TMP_InputField eventNameInput;

    [Header("Event Duration")]
    public TMP_InputField eventDurationInput;
    public Button increaseButton;
    public Button decreaseButton;

    [Header("Totals")]
    public UI_TextAnimator totalIncomeText;
    public UI_TextAnimator totalExpensesText;
    public UI_TextAnimator balanceText;

    [Header("Add Income/Expense Buttons")]
    public Button addIncomeButton;
    public Button addExpenseButton;
    [Header("Creation Modules")]
    public GameObject incomeCreatorPrefab;
    public GameObject expenseCreatorPrefab;

    [Header("Display Incomes/Expenses")]
    [Header("Tabs")]
    public GameObject incomeTab;
    public GameObject expenseTab;
    public Button incomeTabButton;
    public Button expenseTabButton;
    [Header("Tab Colors")]
    public Color selectedTabColor = Color.blue;
    public Color unselectedTabColor = Color.white;
    [Header("Text Colors")]
    public Color selectedTextColor = Color.white;
    public Color unselectedTextColor = Color.blue;

    [Header("Income/Expense Modules")]
    public UI_IncomeModule incomeModuleTemplate;
    public UI_ExpenseModule expenseModuleTemplate;

    private List<UI_IncomeModule> incomeModules = new List<UI_IncomeModule>();
    private List<UI_ExpenseModule> expenseModules = new List<UI_ExpenseModule>();

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

        // Update Initial UI
        UpdateTotals(allocatedEvent);
        UpdateIncomeModules(allocatedEvent.incomes);
    }

    private void OnEnable()
    {
        allocatedEvent.OnEventUpdated += OnEventUpdated;
        allocatedEvent.OnIncomesUpdated += UpdateIncomeModules;
        allocatedEvent.OnExpensesUpdated += UpdateExpenseModules;
    }

    private void OnDisable()
    {
        allocatedEvent.OnEventUpdated -= OnEventUpdated;
        allocatedEvent.OnIncomesUpdated -= UpdateIncomeModules;
        allocatedEvent.OnExpensesUpdated -= UpdateExpenseModules;
    }

    private void OnEventUpdated()
    {
        UpdateIncomeButtonText();
        UpdateExpenseButtonText();
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
        allocatedEvent.EventDuration++;
        UpdateDurationInputField();
    }

    private void DecreaseEventDuration()
    {
        allocatedEvent.EventDuration = Mathf.Max(0, allocatedEvent.eventDuration - 1);
        UpdateDurationInputField();
    }

    private void OnDurationInputFieldChanged(string input)
    {
        if (int.TryParse(input, out int newValue))
        {
            allocatedEvent.EventDuration = Mathf.Max(0, newValue);
        }
        UpdateDurationInputField();
    }

    #endregion

    #region Totals
    public void UpdateTotals(Event currentEvent)
    {
        totalIncomeText.UpdateNumericalText(currentEvent.totalIncome);
        totalExpensesText.UpdateNumericalText(currentEvent.totalExpenses);
        balanceText.UpdateNumericalText(currentEvent.balance);
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
        GameObject expenseCreator = Instantiate(expenseCreatorPrefab, transform, false);
        UI_ExpenseCreator expenseCreatorScript = expenseCreator.GetComponent<UI_ExpenseCreator>();
        expenseCreatorScript.Initialize(allocatedEvent);
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
        incomeTab.gameObject.SetActive(true);
    }

    private void CloseIncomeTab()
    {
        HandleButtonVisual(incomeTabButton, false);

        // Deactivate Tab Content
        incomeTab.gameObject.SetActive(false);
    }

    private void OpenExpenseTab()
    {
        CloseIncomeTab();
        HandleButtonVisual(expenseTabButton, true);

        // Activate Tab Content
        expenseTab.gameObject.SetActive(true);
    }

    private void CloseExpenseTab()
    {
        HandleButtonVisual(expenseTabButton, false);

        // Deactivate Tab Content
        expenseTab.gameObject.SetActive(false);
    }
    #endregion

    #region Tab Button Visuals
    private void HandleButtonVisual(Button button, bool isOpen)
    {
        var image = button.GetComponent<Image>();
        var text = button.GetComponentInChildren<TextMeshProUGUI>();

        var targetImageColor = isOpen ? selectedTabColor : unselectedTabColor;
        var targetTextColor = isOpen ? selectedTextColor : unselectedTextColor;

        image.DOKill();
        text.DOKill();

        image.DOColor(targetImageColor, 0.25f).SetEase(Ease.InOutSine);
        text.DOColor(targetTextColor, 0.25f).SetEase(Ease.InOutSine);
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

    #region Creating Income/Expense Modules 

    private void UpdateIncomeModules(List<Income> incomes)
    {
        ClearIncomeModules();

        foreach (var income in incomes)
        {
            UI_IncomeModule incomeModule = Instantiate(incomeModuleTemplate, incomeTab.transform, false);
            incomeModule.Initialize(income);
            incomeModules.Add(incomeModule);
        }
    }

    private void ClearIncomeModules()
    {
        foreach (var incomeModule in incomeModules)
        {
            Destroy(incomeModule.gameObject);
        }
        incomeModules.Clear();
    }

    private void UpdateExpenseModules(List<Expense> expenses)
    {
        ClearExpenseModules();

        foreach (var expense in expenses)
        {
            UI_ExpenseModule expenseModule = Instantiate(expenseModuleTemplate, expenseTab.transform, false);
            expenseModule.Initialize(expense);
            expenseModules.Add(expenseModule);
        }
    }

    private void ClearExpenseModules()
    {
        foreach (var expenseModule in expenseModules)
        {
            Destroy(expenseModule.gameObject);
        }
        expenseModules.Clear();
    }

    #endregion

    #endregion
}