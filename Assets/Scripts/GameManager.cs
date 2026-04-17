using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int playerHealth = 25;

    public event Action<int> OnHealthDecrease;
    public event Action OnGameOver;


    [Header("Other Managers")]
    [SerializeField] PlayerCubeController cubeController;
    [SerializeField] EconomyController economyController;
    [SerializeField] UIController uIController;
    void Start()
    {

        cubeController.OnHit += DecreaseHealth;
        PauseGame();
        uIController.OnStartGame += ResumeGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DecreaseHealth()
    {
        playerHealth--;
        OnHealthDecrease?.Invoke(playerHealth);
        if (playerHealth <= 0) OnGameOver?.Invoke();
    }

    void ResumeGame() { Time.timeScale = 1f; Debug.Log("Nutcracker"); }

    void PauseGame() => Time.timeScale = 0f;
}
