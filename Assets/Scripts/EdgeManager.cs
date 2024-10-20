using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EdgeManager : MonoBehaviour
{
    public int weight;
    public TMP_InputField weightUI;

    [SerializeField]
    public float edge_width = 0.14f;
    public Material line_material;

    public LineRenderer lineR;
    public EdgeCollider2D lCollider;

    public NodeManager node1;
    public NodeManager node2;

    private Color intialColor;

    public bool selected = false;

    // Start is called before the first frame update
    void Start()
    {
        weight = 1;
        weightUI.text = "1";
        
        intialColor = line_material.color;
    }

    public void InitEdge(GameObject node1, GameObject node2)
    {
        this.node1 = node1.GetComponent<NodeManager>();
        this.node2 = node2.GetComponent<NodeManager>();

        gameObject.name = this.node1.num.ToString() + "," + this.node2.num.ToString();
        transform.parent = node1.transform.parent.parent.GetChild(1);

        lineR.startWidth = edge_width;
        lineR.endWidth = edge_width;
        lineR.material = line_material;

        lCollider.edgeRadius = edge_width;
        lCollider.SetPoints(new List<Vector2> { node1.transform.localPosition, node2.transform.localPosition });

        // Visualising the line 
        lineR.positionCount = 2;
        lineR.SetPosition(0, node1.transform.position);
        lineR.SetPosition(1, node2.transform.position);

        float midX = (node1.transform.localPosition.x + node2.transform.localPosition.x)/2;
        float midY = (node1.transform.localPosition.y + node2.transform.localPosition.y)/2;

        transform.GetChild(0).position = new Vector2(midX, midY);
    }

    public void HighlightEdge()
    {
        lineR.material.color = Color.green;
    }
    public void SelectEdge()
    {
        lineR.material.color = Color.red;
    }
    public void RemoveHiglight()
    {
        line_material.color = intialColor;
    }

    public void SetWeight()
    {
        weight = int.Parse(weightUI.text);
    }
}
