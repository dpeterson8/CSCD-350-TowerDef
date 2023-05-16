using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public GameObject[] objectList;
    public GameObject rangeTowerPre;
    public GameObject mageTowerPre;
    public Tilemap roadMap;
    [SerializeField] private Player player;
    public static List<GameObject> _towers; 
    public TMP_Text moneyText;
    public TMP_Text healthText;

     
    void Start()
    {
        _towers = new List<GameObject>();
        roadMap = GameObject.Find("Road").GetComponent<Tilemap>();
    }

    void Update()
    {
        moneyText.text = player.getPlayerMoney().ToString();
        healthText.text = player.getPlayerHealth().ToString();
    }

    public void CreateTower(string towerTypeEnum) {
        if(towerTypeEnum.ToLower() == "range" && player.getPlayerMoney() >= 25) {
            player.money -= 25;
            Vector2 spawnPos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
            GameObject newTower = GameObject.Instantiate(rangeTowerPre, spawnPos, Quaternion.identity);
        } else if(towerTypeEnum.ToLower() == "mage" && player.getPlayerMoney() >= 30) {
            player.money -= 30;
            Vector2 spawnPos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
            var temp = Instantiate(mageTowerPre, spawnPos, Quaternion.identity);
            objectList[objectList.Length + 1] = temp;
        }
    }

    public bool isTowerValid(Transform curPosition) {
        foreach(GameObject tower in objectList) {
            if(tower.transform.localPosition == curPosition.position) {
                return false;
            }
        }

        return true;
    }
}
