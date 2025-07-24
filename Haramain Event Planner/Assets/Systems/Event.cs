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

    public void AddIncome(Income income)
    {
        incomes.Add(income);
        Calculator.UpdateEventValues(this);

        OnEventUpdated?.Invoke();
        OnIncomesUpdated?.Invoke(incomes);
    }

    public void AddExpense(Expense expense)
    {
        expenses.Add(expense);
        Calculator.UpdateEventValues(this);

        OnEventUpdated?.Invoke();
        OnExpensesUpdated?.Invoke(expenses);
    }
}