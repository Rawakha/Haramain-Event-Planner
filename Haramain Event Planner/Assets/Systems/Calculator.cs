using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Frequency
{
    OneTime,
    Daily,
    Custom
}

public enum ExpenseCategory
{
    Hotel,
    ArtistPayment,
    FoodAndDrink,
    Supplies,
    Other
}

[System.Serializable]
public class Expense
{
    public string name;
    public ExpenseCategory category;
    public float cost;
    public Frequency frequency;
    public int customFrequency = 0;

    [Header("Total")]
    public float total;
}

[System.Serializable]
public class Income
{
    public string name;
    public float cost;
    public Frequency frequency;
    public int customFrequency = 0;

    [Header("Total")]
    public float total;
}

public class Calculator : MonoBehaviour
{
    public int eventDuration;

    [Header("Expenses")]
    public List<Expense> expenses = new List<Expense>();

    [Header("Income")]
    public List<Income> incomes = new List<Income>();

    [Header("Totals")]
    public float totalExpenses;
    public float totalIncome;
    public float totalProfit;

    private void Update()
    {
        // Get all the totals for each expense and income
        HandleExpenses();
        HandleIncomes();

        totalExpenses = GetTotalExpense();
        totalIncome = GetTotalIncome();

        // Get balance
        totalProfit = totalIncome - totalExpenses;
    }

    private void HandleExpenses()
    {
        foreach (var expense in expenses)
        {
            int frequency = GetFrequency(expense.frequency);
            frequency = frequency == -1 ? expense.customFrequency : frequency;

            float total = expense.cost * frequency;
            expense.total = total;
        }
    }

    private void HandleIncomes()
    {
        foreach (var income in incomes)
        {
            int frequency = GetFrequency(income.frequency);
            frequency = frequency == -1 ? income.customFrequency : frequency;

            float total = income.cost * frequency;
            income.total = total;
        }
    }

    private float GetTotalExpense()
    {
        float e = 0f;

        foreach (var expense in expenses)
        {
            e += expense.total;
        }

        return e;
    }

    private float GetTotalIncome()
    {
        float i = 0f;

        foreach (var income in incomes)
        {
            i += income.total;
        }

        return i;
    }

    public int GetFrequency(Frequency frequency)
    {
        int i = 0;

        switch (frequency)
        {
            case Frequency.Daily:

                i = eventDuration;

            break;

            case Frequency.OneTime:

                i = 1;

            break;

            case Frequency.Custom:

                i = -1;

            break;
        }

        return i;
    }
}