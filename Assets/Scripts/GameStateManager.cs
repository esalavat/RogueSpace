using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameVars gameState;
    public static GameStateManager Instance { get; private set; }
    
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
            Destroy(this); 
        else 
            Instance = this; 
    }

    public GameVars getGameState() {
        return gameState;
    }

    public bool hasUpgrade(Upgrade upgrade) {
        return gameState.purchasedUpgrades.Contains(upgrade);
    }

    public void addUpgrade(Upgrade upgrade) {
        gameState.purchasedUpgrades.Add(upgrade);
    }

    public void addCredits(int value) {
        gameState.credits += value;
        EventManager.CreditsUpdated(gameState.credits);
    }

    public void wipeState() {
        gameState.credits = 0;
        gameState.purchasedUpgrades = new System.Collections.Generic.List<Upgrade>();
    }
}
