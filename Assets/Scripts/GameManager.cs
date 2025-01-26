using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isGameFinalized=false;

    public void ResetGame()
    {
        isGameFinalized = false;
    }

}
