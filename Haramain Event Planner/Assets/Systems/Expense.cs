using System;
using UnityEngine;

[System.Serializable]
public class Expense
{
    public string name;
    public ExpenseCategory category;
    public float cost;
    public Frequency frequency;
    public int customFrequency = 0;
    public float total;

    public event Action<Expense> OnExpenseDeleted;
    public event Action<Expense> OnExpenseEdited;

    public void Delete()
    {
        OnExpenseDeleted?.Invoke(this);
    }

    public void EditExpense(string name, float cost, Frequency frequency, int customFrequency = 0)
    {
        this.name = name;
        this.cost = cost;
        this.frequency = frequency;
        this.customFrequency = customFrequency;
        OnExpenseEdited?.Invoke(this);
    }
}