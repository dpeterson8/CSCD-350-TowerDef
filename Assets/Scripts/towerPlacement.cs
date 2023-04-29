
using UnityEngine;

public class towerPlacement : MonoBehaviour
{
    private float zCord;
    private Vector3 mouseOffset;
    private Grid grid;
     private Color startcolor;
    public new Renderer renderer;

    // Start is called before the first frame update

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
    void Start()
    {
        grid = Grid.FindObjectOfType<Grid>();
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        transform.localPosition = grid.GetCellCenterWorld(cellPosition);
    }

    private Vector3 mouseWordPosition() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zCord;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return worldPosition;
    }
}
