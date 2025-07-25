using UnityEngine;
using System.Collections.Generic;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance;

    [SerializeField] public Event currentEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}