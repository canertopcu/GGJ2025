using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState GameState = GameState.MainMenu;
    public bool isGameFinalized=false;
 
    internal void PlayerKilled(int playerID)
    {
        GameState = GameState.Pause;
        if (playerID == 1) {
            UIManager.Instance.OpenLosePage();
        } else { 
            UIManager.Instance.OpenWinPage();
        }
    }
}
