using UnityEngine;
using System;

[System.Serializable]
public class Income
{
    public string name;
    public float cost;
    public Frequency frequency;
    public int customFrequency = 0;
    public float total;

    public event Action<Income> OnIncomeDeleted;
    public event Action<Income> OnIncomeEdited;

    public void Delete()
    {
        OnIncomeDeleted?.Invoke(this);
    }

    public void EditIncome(string name, float cost, Frequency frequency, int customFrequency = 0)
    {
        this.name = name;
        this.cost = cost;
        this.frequency = frequency;
        this.customFrequency = customFrequency;
        OnIncomeEdited?.Invoke(this);
    }
}