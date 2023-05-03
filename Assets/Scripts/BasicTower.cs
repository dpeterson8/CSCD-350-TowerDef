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
    private towerStatus status;
    private float zCord;
    private Vector3 mouseOffset;
    private Grid grid;
     private Color startcolor;
    public new Renderer renderer;
    public GameObject projectilePrefab;

    private List<BasicEnemy> curEnemiesInRange = new List<BasicEnemy>();

    public float attackRate;
    private float lastAttackTime = 0;
    public int towerDamage;
    public float projectileSpeed;

    public BasicEnemy curEnemy;
    public Tilemap roadMap;

     
    void Start()
    {
        grid = Grid.FindObjectOfType<Grid>();
        renderer = GetComponent<Renderer>();
        status = towerStatus.attack;
        roadMap = GameObject.Find("Road").GetComponent<Tilemap>();
    }

    void Update()
    {

        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        transform.localPosition = grid.GetCellCenterWorld(cellPosition);
        Debug.Log(roadMap.GetTile(grid.WorldToCell(transform.position)));
        startcolor = renderer.material.color;

        if (Time.time - lastAttackTime > attackRate && status == towerStatus.attack) {
            lastAttackTime = Time.time;
            curEnemy = nextEnemy();

            if(curEnemy != null) {
                Attack();
            }
        }
    }

    BasicEnemy nextEnemy() {
        if(curEnemiesInRange.Count > 0) {
            return curEnemiesInRange[0];
        }
        
        return null;
    }

    void Attack() {
        GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectileObject.GetComponent<BasicProjectile>().Initialize(curEnemy, towerDamage, projectileSpeed);
    }

    private Vector3 mouseWordPosition() {
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

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            curEnemiesInRange.Remove(other.GetComponent<BasicEnemy>());
            Debug.Log("Out");
        }   
    }

    void OnMouseDown() {
        this.status = towerStatus.placing;
        zCord = Camera.main.WorldToScreenPoint(transform.position).z;
        mouseOffset = transform.position - mouseWordPosition();
    }

    private void OnMouseDrag() {
        transform.position = mouseWordPosition() + mouseOffset;
        startcolor = renderer.material.color;
        if(roadMap.GetTile(grid.WorldToCell(transform.position)) == null)
        {
            renderer.material.color = Color.green;
        }
        else
        {
            renderer.material.color = Color.red;
        }
    }
    
    private void OnMouseUp() {
        renderer.material.color = startcolor;
        status = towerStatus.attack;    
    }
}
