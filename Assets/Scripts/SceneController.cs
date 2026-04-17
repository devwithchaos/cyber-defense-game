using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private float waitTime = 1.5f;
    private float nextTimeCheck;
    private bool waitingOnFinalRound = false;

    [Header("Other Managers")]
    [SerializeField] RoundManager roundManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] EconomyController economyController;
    void Start()
    {
        roundManager.OnFinalRoundComplete += SwitchToWinScreen;
        gameManager.OnGameOver += SwitchToLoseScreen;
    }

    void SwitchToWinScreen()
    {
        if (economyController.GetEnemyCount == 0) SceneManager.LoadScene(1);
        nextTimeCheck = Time.time + waitTime;
        waitingOnFinalRound = true;
    }
    void SwitchToLoseScreen()
    {
        SceneManager.LoadScene(2);
    }

    private void Update()
    {
        if(waitingOnFinalRound && Time.time > nextTimeCheck)
        {
            SwitchToWinScreen();
        }
    }


    public static void SwitchToMainGame()
    {
        SceneManager.LoadScene(0);
    }
}
