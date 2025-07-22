using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event
{
    public int eventDuration;

    [Header("Expenses")]
    public List<Expense> expenses = new List<Expense>();

    [Header("Income")]
    public List<Income> incomes = new List<Income>();

    [Header("Totals")]
    public float totalExpenses;
    public float totalIncome;
    public float balance;
}