using UnityEngine;

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