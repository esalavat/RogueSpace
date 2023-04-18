using TMPro;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text creditsText;

    public GameVars gameState;

    public static UIManagerScript Instance { get; private set; }

    private void Start(){
        UpdateCredits();
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

    private void OnEnable()
    {
        EventManager.ScoreUpdated += EventManagerOnScoreUpdated;
    }

    private void OnDisable()
    {
        EventManager.ScoreUpdated -= EventManagerOnScoreUpdated;
    }

    public void UpdateCredits() {
        if(gameState.credits > 0) {
            creditsText.gameObject.SetActive(true);
            creditsText.text = gameState.credits.ToString();
        }
    }
}
