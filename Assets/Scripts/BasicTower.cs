using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

enum towerStatus
{
    idle,
    attack,
    placing
}

public class BasicTower : MonoBehaviour
{
    private PlayerUI playerUI;
    private towerStatus status;
    private float zCord;
    private Vector3 mouseOffset;
    private Grid grid;
    private Color startcolor;
    public new Renderer renderer;
    public GameObject projectilePrefab;
    private Material orignalMaterial;
    private List<BasicEnemy> curEnemiesInRange = new List<BasicEnemy>();
    public float attackRate;
    private float lastAttackTime = 0;
    public int towerDamage;
    public float projectileSpeed;
    public BasicEnemy curEnemy;
    public Tilemap roadMap;
    public GameObject moveButton, sellButton;
    

     
    void Start()
    {
        grid = Grid.FindObjectOfType<Grid>();
        renderer = GetComponent<Renderer>();
        roadMap = GameObject.Find("Road").GetComponent<Tilemap>();
        startcolor = GetComponent<Renderer>().material.color;
        orignalMaterial = renderer.material;
        status = towerStatus.placing;
        playerUI = GameObject.Find("PlayerUI").GetComponent<PlayerUI>();
    }

    void Update()
    {

        if (status == towerStatus.placing) {
            Vector3Int cellPosition = grid.WorldToCell(transform.position);
            transform.localPosition = grid.GetCellCenterWorld(cellPosition);
            Debug.Log(roadMap.GetTile(grid.WorldToCell(transform.position)));
            isTowerValid();
        } else if (status == towerStatus.attack) {
            Attack();
        }
    }

    private BasicEnemy nextEnemy() {
        if(curEnemiesInRange.Count > 0) {
            return curEnemiesInRange[0];
        }
        
        return null;
    }

    private void Attack() {
        if (Time.time - lastAttackTime > attackRate && status == towerStatus.attack) {
            lastAttackTime = Time.time;
            curEnemy = nextEnemy();

            if(curEnemy != null) {
                GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectileObject.GetComponent<BasicProjectile>().Initialize(curEnemy, towerDamage, projectileSpeed);            }
            }
    }

    private Vector3 mouseWorldPosition() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zCord;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return worldPosition;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy"))
        {
            curEnemiesInRange.Add(other.GetComponent<BasicEnemy>());
            Debug.Log("in");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            curEnemiesInRange.Remove(other.GetComponent<BasicEnemy>());
            Debug.Log("Out");
        }   
    }

    private void OnMouseDown() {
        if (status == towerStatus.placing) {
            zCord = Camera.main.WorldToScreenPoint(transform.position).z;
            mouseOffset = transform.position - mouseWorldPosition();
        }
    }

    private void OnMouseDrag() {
        if (status == towerStatus.placing) {
            transform.position = mouseWorldPosition() + mouseOffset;
            startcolor = renderer.material.color;
        }
    }

    private void OnMouseUp() {
        // if (status == towerStatus.attack) {
        //     sellButton.SetActive(true);
        //     moveButton.SetActive(true);
        //     status = towerStatus.idle;
        // } else 
        if (isTowerValid()) {
            renderer.material.color = Color.white;
            status = towerStatus.attack;
        }
    }
    
    private bool isTowerValid() {
        if(roadMap.GetTile(grid.WorldToCell(transform.position)) == null && playerUI.isTowerValid(transform))
        {
            renderer.material.color = Color.green;
            return true;
        }
        else
        {
            renderer.material.color = Color.red;
            return false;
        }
    }
}
