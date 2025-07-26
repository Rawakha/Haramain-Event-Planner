using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event
{
    public string eventName = new string("New Event");
    public int eventDuration;

    [Header("Expenses")]
    public List<Expense> expenses = new List<Expense>();

    [Header("Income")]
    public List<Income> incomes = new List<Income>();

    [Header("Totals")]
    public float totalExpenses;
    public float totalIncome;
    public float balance;

    public event Action OnEventUpdated;
    public event Action<List<Income>> OnIncomesUpdated;
    public event Action<List<Expense>> OnExpensesUpdated;

    // Setters
    public int EventDuration
    {
        get => eventDuration;
        set
        {
            eventDuration = value;
            EventUpdated();
        }
    }

    private void EventUpdated()
    {
        Calculator.UpdateEventValues(this);
        OnEventUpdated?.Invoke();
    }

    public void AddIncome(Income income)
    {
        incomes.Add(income);
        income.OnIncomeDeleted += RemoveIncome;
        income.OnIncomeEdited += IncomeEdited;
        EventUpdated();
        OnIncomesUpdated?.Invoke(incomes);
    }

    private void RemoveIncome(Income income)
    {
        var index = incomes.IndexOf(income);
        if (index >= 0)
        {
            incomes.RemoveAt(index);
            EventUpdated();
            OnIncomesUpdated?.Invoke(incomes);
        }
    }

    private void IncomeEdited(Income income)
    {
        EventUpdated();
        OnIncomesUpdated?.Invoke(incomes);
    }

    public void AddExpense(Expense expense)
    {
        expenses.Add(expense);
        expense.OnExpenseDeleted += RemoveExpense;
        expense.OnExpenseEdited += ExpenseEdited;
        EventUpdated();
        OnExpensesUpdated?.Invoke(expenses);
    }

    public void RemoveExpense(Expense expense)
    {
        var index = expenses.IndexOf(expense);
        if (index >= 0)
        {
            expenses.RemoveAt(index);
            Calculator.UpdateEventValues(this);
            OnEventUpdated?.Invoke();
            OnExpensesUpdated?.Invoke(expenses);
        }
    }

    public void ExpenseEdited(Expense expense)
    {
        EventUpdated();
        OnExpensesUpdated?.Invoke(expenses);
    }
}