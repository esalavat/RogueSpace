using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text[] creditsTexts;
    public TMP_Text messageText;
    public GameObject messageContainer;
    public GameObject shieldUI;
    public GameVars gameState;

    public GameObject laserUpgradeUI;
    public GameObject shieldUpgradeUI;

    public static UIManagerScript Instance { get; private set; }

    private void Start(){
        UpdateCredits();
        UpdateUpgrades();
    }
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
            Destroy(this); 
        else 
            Instance = this; 
    }

    private void EventManagerOnScoreUpdated(int score)
    {
        scoreText.text = score.ToString();
    }

    private void CoinCollected(int value) {
        gameState.credits += value;
        UpdateCredits();
    }

    private void OnEnable()
    {
        EventManager.OnScoreUpdated += EventManagerOnScoreUpdated;
        EventManager.OnCoinCollected += CoinCollected;
        EventManager.OnCreditsUpdated += UpdateCredits;
        EventManager.OnMessage += DisplayMessage;
        EventManager.OnLifeUpdated += UpdateShield;
    }

    private void OnDisable()
    {
        EventManager.OnScoreUpdated -= EventManagerOnScoreUpdated;
        EventManager.OnCoinCollected -= CoinCollected;
        EventManager.OnCreditsUpdated -= UpdateCredits;
        EventManager.OnMessage -= DisplayMessage;
        EventManager.OnLifeUpdated -= UpdateShield;
    }

    public void UpdateCredits(int credits) {
        UpdateCredits();
    }
    public void UpdateCredits() {
        if(gameState.credits > 0) {
            foreach(var creditsText in creditsTexts) {
                creditsText.gameObject.SetActive(true);
                creditsText.text = gameState.credits.ToString();
            }
        }
    }

    public void PurchaseLaserUpgrade() {
        if(GameStateManager.Instance.getGameState().credits >= 100) {
            PurchaseUpgrade(Upgrades.LaserSpeed, 100);
        } else {
            EventManager.Message("Not enough credits!");
        }
    }

    public void PurchaseShieldUpgrade() {
        if(GameStateManager.Instance.getGameState().credits >= 150) {
            PurchaseUpgrade(Upgrades.Shield, 150);
        } else {
            EventManager.Message("Not enough credits!");
        }
    }
    private void PurchaseUpgrade(Upgrades upgrade, int cost) {
        GameStateManager.Instance.addUpgrade(upgrade);
        GameStateManager.Instance.addCredits(cost * -1);
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
    }

    private void DisplayMessage(string message) { 
        messageText.text = message;
        messageContainer.SetActive(true);
        Invoke("HideMessage", 2);
    }

    private void HideMessage() {
        messageContainer.SetActive(false);
    }

    private void UpdateUpgrades() {
        if(GameStateManager.Instance.hasUpgrade(Upgrades.LaserSpeed)) {
            laserUpgradeUI.GetComponent<Button>().interactable = false;
        }

        if(GameStateManager.Instance.hasUpgrade(Upgrades.Shield)) {
            shieldUpgradeUI.GetComponent<Button>().interactable = false;
        }
    }

    private void UpdateShield(int life) {
        if(life > 1) {
            shieldUI.SetActive(true);
            shieldUI.transform.Find("ShieldAmount").gameObject.GetComponent<TMP_Text>().text = life-1 + "X";
        } else {
            shieldUI.SetActive(false);
        }
        
    }
}
