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

    public bool hasUpgrade(Upgrades upgrade) {
        if(gameState.purchasedUpgrades.Contains(upgrade)) {
            return true;
        }

        return false;
    }

    public void addUpgrade(Upgrades upgrade) {
        gameState.purchasedUpgrades.Add(upgrade);
    }

    public void addCredits(int value) {
        gameState.credits += value;
        EventManager.CreditsUpdated(gameState.credits);
    }
}
