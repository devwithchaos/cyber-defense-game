using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public event Action<int> OnUpgrade;
    public event Action<int> OnMaxUpgradeReached;


    [Header("Other Managers")]
    [SerializeField] EconomyController economyController;

    private int[] currentLevels = new int[3];
    private int[] maxLevels = new int[] { 5, 5, 5 };


    private void Awake()
    {
        economyController = FindFirstObjectByType<EconomyController>();
    }

    public void EmitUpdate(int option)
    {
        if (currentLevels[option] >= maxLevels[option]) OnMaxUpgradeReached?.Invoke(option);
        if (economyController.TryPurchase(50))
        {
            OnUpgrade?.Invoke(option);
            currentLevels[option]++;
        }
    }
}
