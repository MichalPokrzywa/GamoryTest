using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player playerReference;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private GameObject inventory;

    public void StartGameplay(CharacterGameStats stats)
    {
        inventory.SetActive(false);
        playerReference.InitPlayer(stats);
        spawner.StartSpawning();
    }

    public void RestartGame()
    {

    }

}
