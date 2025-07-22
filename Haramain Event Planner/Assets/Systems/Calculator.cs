using System.Collections.Generic;
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

public static class Calculator
{
    public static void UpdateEventValues(Event p_event)
    {
        // Get all the totals for each expense and income
        HandleExpenses(p_event.expenses, p_event);
        HandleIncomes(p_event.incomes, p_event);

        p_event.totalExpenses = GetTotalExpense(p_event.expenses);
        p_event.totalIncome = GetTotalIncome(p_event.incomes);

        // Get balance
        p_event.balance = p_event.totalIncome - p_event.totalExpenses;
    }

    private static void HandleExpenses(List<Expense> expenses, Event p_event)
    {
        foreach (var expense in expenses)
        {
            int frequency = GetFrequency(expense.frequency, p_event);
            frequency = frequency == -1 ? expense.customFrequency : frequency;

            float total = expense.cost * frequency;
            expense.total = total;
        }
    }

    private static void HandleIncomes(List<Income> incomes, Event p_event)
    {
        foreach (var income in incomes)
        {
            int frequency = GetFrequency(income.frequency, p_event);
            frequency = frequency == -1 ? income.customFrequency : frequency;

            float total = income.cost * frequency;
            income.total = total;
        }
    }

    private static float GetTotalExpense(List<Expense> expenses)
    {
        float e = 0f;

        foreach (var expense in expenses)
        {
            e += expense.total;
        }

        return e;
    }

    private static float GetTotalIncome(List<Income> incomes)
    {
        float i = 0f;

        foreach (var income in incomes)
        {
            i += income.total;
        }

        return i;
    }

    private static int GetFrequency(Frequency frequency, Event p_event)
    {
        int i = 0;

        switch (frequency)
        {
            case Frequency.Daily:

                i = p_event.eventDuration;

            break;

            case Frequency.OneTime:

                i = 1;

            break;

            case Frequency.Custom:

                i = -2;

            break;
        }

        return i;
    }
}