using Enemy;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemiesManager enemiesManager;
    [SerializeField]
    private TimeManager timeManager;



    public void FinishGame()
    {
        timeManager.FinishGame();
        enemiesManager.FinishGame();
    }

    public void ActiveResult()
    {

    }
}
