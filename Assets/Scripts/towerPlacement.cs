
using UnityEngine;

public class towerPlacement : MonoBehaviour
{
    private float zCord;
    private Vector3 mouseOffset;
    private Grid grid;
     private Color startcolor;
    public new Renderer renderer;

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
        // Used to move tower
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
        
        return new SimpleAiMove();
    }

    void Attack() {
        Debug.Log("Attack()");
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

    private Vector3 mouseWordPosition() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zCord;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return worldPosition;
    }
}
