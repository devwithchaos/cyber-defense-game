using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Text References
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    [SerializeField] TMPro.TextMeshProUGUI remainingText;
    [SerializeField] TMPro.TextMeshProUGUI roundText;
    [SerializeField] TMPro.TextMeshProUGUI healthText;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject howToPlayPanel;
    // Upgrade Area
    [SerializeField] TMPro.TextMeshProUGUI[] upgradeLabels;
    [SerializeField] GameObject[] upgradeButtons;

    [Header("Other Managers")]
    [SerializeField] EconomyController economyController;
    [SerializeField] RoundManager roundManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] UpgradeManager upgradeManager;


    // Events
    public event Action OnStartGame;


    // UI Stats
    private bool upgradePanelShown = false;
    void Start()
    {
        upgradePanel.SetActive(upgradePanelShown);
        economyController.OnBalanceUpdate += UpdateScoreText;
        roundManager.OnCountUpdate += UpdateCountText;
        roundManager.OnNewRound += UpdateRoundText;
        gameManager.OnHealthDecrease += UpdateHealthText;
        upgradeManager.OnMaxUpgradeReached += DisableUpgradeButton;
    }

    void UpdateScoreText(int coins)
    {
        scoreText.text = "Coins: " + coins;
    }
    
    void UpdateCountText(int amt, int max)
    {
        remainingText.text = $"Remaining: {amt}/{max}";
    }

    // Update is called once per frame
    void UpdateRoundText(int round)
    {
        roundText.text = $"Round: {round}";
    }

    void UpdateHealthText(int health)
    {
        healthText.text = $"Health: {health}";
    }

    public void ToggleUpgradePanel()
    {
        upgradePanelShown = !upgradePanelShown;
        upgradePanel.SetActive(upgradePanelShown);
    }

    public void StartButtonClicked()
    {
        OnStartGame?.Invoke();
        howToPlayPanel.SetActive(false);
    }

    void DisableUpgradeButton(int option)
    {
        upgradeButtons[option].GetComponent<Button>().interactable = false;

        upgradeLabels[option].text = "MAX LEVEL";
    }
}
