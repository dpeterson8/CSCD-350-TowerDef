using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : MonoBehaviour
{
    private float zCord;
    private Vector3 mouseOffset;
    private Grid grid;
     private Color startcolor;
    public new Renderer renderer;
    private List<SimpleAiMove> curEnemiesInRange = new List<SimpleAiMove>();

        public float attackRate;
    private float lastAttackTime;

    public SimpleAiMove curEnemy;

     
    void Start()
    {
        grid = Grid.FindObjectOfType<Grid>();
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        transform.localPosition = grid.GetCellCenterWorld(cellPosition);
        // Used to check attack status
        if (Time.time - lastAttackTime > attackRate) {
            lastAttackTime = Time.time;

            if(curEnemy != null) {
                Attack();
            }
        }
    }

    SimpleAiMove nextEnemy() {
        if(curEnemiesInRange[0] !=  null) {
            return curEnemiesInRange[0];
        }
        
        return new SimpleAiMove();
    }

    void Attack() {
        Debug.Log("Attack()");
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
            curEnemiesInRange.Add(other.GetComponent<SimpleAiMove>());
            Debug.Log("in");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            curEnemiesInRange.Remove(other.GetComponent<SimpleAiMove>());
            Debug.Log("Out");
        }   
    }

    void OnMouseDown() {
        Debug.Log("Mouse Down");
        zCord = Camera.main.WorldToScreenPoint(transform.position).z;
        mouseOffset = transform.position - mouseWordPosition();
    }

    private void OnMouseDrag() {
        transform.position = mouseWordPosition() + mouseOffset;
        startcolor = renderer.material.color;
        renderer.material.color = Color.yellow;
    }
}
