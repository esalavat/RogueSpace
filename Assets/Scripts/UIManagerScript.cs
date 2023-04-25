using System.Collections.Generic;
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
    public GameObject upgradeUIPrefab;
    public GameObject upgradeGroup;
    private Dictionary<Upgrade, GameObject> upgradeUIs = new Dictionary<Upgrade, GameObject>();

    public static UIManagerScript Instance { get; private set; }

    private void Start(){
        UpdateCredits();
        LoadUpgrades();
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
        GameStateManager.Instance.gameState.credits += value;
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
        if(GameStateManager.Instance.gameState.credits > 0) {
            foreach(var creditsText in creditsTexts) {
                creditsText.gameObject.SetActive(true);
                creditsText.text = GameStateManager.Instance.gameState.credits.ToString();
            }
        }
    }

    public void PurchaseLaserUpgrade() {
        if(GameStateManager.Instance.getGameState().credits >= 100) {
            PurchaseUpgrade(Upgrade.LaserSpeed1);
        } else {
            EventManager.Message("Not enough credits!");
        }
    }

    public void PurchaseShieldUpgrade() {
        if(GameStateManager.Instance.getGameState().credits >= 150) {
            PurchaseUpgrade(Upgrade.Shield1);
        } else {
            EventManager.Message("Not enough credits!");
        }
    }
    private void PurchaseUpgrade(Upgrade upgrade) {
        GameStateManager.Instance.addUpgrade(upgrade);
        GameStateManager.Instance.addCredits(upgrade.cost * -1);
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

    private void LoadUpgrades() {
        foreach(var upgrade in Upgrade.Values)
        {
            GameObject uiElement = Instantiate(upgradeUIPrefab, upgradeGroup.transform, false);
            uiElement.transform.Find("DisplayName").gameObject.GetComponent<TMP_Text>().text = upgrade.displayName;
            uiElement.transform.Find("Cost").gameObject.GetComponent<TMP_Text>().text = upgrade.cost.ToString();
            Sprite sprite = Resources.Load<Sprite>(upgrade.imagePath);
            var imageElement = uiElement.transform.Find("UpgradeImg");
            imageElement.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
            imageElement.GetComponent<RectTransform>().sizeDelta = new Vector2(upgrade.imageWidth, upgrade.imageHeight);
            uiElement.GetComponent<Button>().onClick.AddListener(() => PurchaseUpgrade(upgrade));
            upgradeUIs.Add(upgrade, uiElement);
        }
        UpdateUpgrades();
    }

    private void UpdateUpgrades() {
        foreach(Upgrade upgrade in GameStateManager.Instance.gameState.purchasedUpgrades) {
            getUpgradeUI(upgrade).GetComponent<Button>().interactable = false;
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

    private GameObject getUpgradeUI(Upgrade upgrade) {
        return upgradeUIs[upgrade];
    }
}
