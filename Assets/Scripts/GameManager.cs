using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player playerReference;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameplayCanvas gameCanvas;

    public void StartGameplay(CharacterGameStats stats)
    {
        inventory.SetActive(false);
        playerReference.InitPlayer(stats);
        spawner.StartSpawning();
        gameCanvas.InitCanvas(stats);
        gameCanvas.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public GameplayCanvas GetGameplayCanvas()
    {
        return gameCanvas;
    }
}
