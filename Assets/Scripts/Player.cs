using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum playerState
{
    placing,
    attack,
    endRound
}

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set;}

    public static ArrayList _towers;
    public int health = 100;
    public int money;
    public int enemyCount;
    public playerState gameState = playerState.attack;

    private void Awake () {
        if (Instance == null) {
            Instance = this;
        } else {
        }
    }

    private void Update() {
        if(health <= 0) {
            gameState = playerState.endRound;
        }
    }

    void Start() {
        _towers = new ArrayList();
    }


    public int getPlayerHealth() {
        return health;
    }

    public int getPlayerMoney() {
        return money;
    }


    public void CreateTower(string towerTypeEnum) {
        if(towerTypeEnum.ToLower() == "range") {
            Vector2 spawnPos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
            var newTower = Instantiate(Resources.Load("../Prefabs/archerLevel1") as GameObject, spawnPos, Quaternion.identity);
            _towers.Add(newTower);
        } else if(towerTypeEnum.ToLower() == "mage") {
            Vector2 spawnPos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
            var newTower = Instantiate(Resources.Load("Prefabs/mageLevel1"), spawnPos, Quaternion.identity);
            _towers.Add(newTower);
            Debug.Log(_towers.ToString());
        }
    }

    public void PlayGame (int level) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
    }
}
