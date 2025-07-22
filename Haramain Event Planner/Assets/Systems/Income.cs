using UnityEngine;

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