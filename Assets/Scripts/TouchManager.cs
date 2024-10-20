using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public GameObject node_pref;
    public GameObject edge_pref;

    public int currentNodeIndex;

    public GameObject nodesParent;

    private RaycastHit2D hit;
    
    private GameObject prevNode;
    private bool shouldAddEdge = false;

    private void Start()
    {
        currentNodeIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 spawnPos = Camera.main.ScreenToWorldPoint(touch.position);
            spawnPos.z = 0;

            
            if(touch.phase == TouchPhase.Began)
            {
                hit = Physics2D.Raycast(Camera.main.transform.position, spawnPos);

                if (hit.collider)
                {
                    // Hit a already present node
                    if (hit.collider.CompareTag("Node"))
                        prevNode = hit.collider.gameObject;
                }
                else
                    SpawnANode(spawnPos);
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                shouldAddEdge = true;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                if(shouldAddEdge)
                {
                    hit = Physics2D.Raycast(Camera.main.transform.position, spawnPos);
                    
                    if(hit.collider)
                    {
                        GameObject currNode = hit.collider.gameObject;
                        
                        if (hit.collider.CompareTag("Node") && currNode != prevNode)
                            AddNewEdge(prevNode, currNode);
                    }
                }

                shouldAddEdge = false;
                prevNode = null;
            }
        }
    }

    void AddNewEdge(GameObject node1, GameObject node2)
    {
        GameObject edge = Instantiate(edge_pref, node1.transform.position, Quaternion.identity);
        
        EdgeManager edM = edge.GetComponent<EdgeManager>();
        edM.InitEdge(node1, node2);

        GraphManager.Instance.AddEdge(edM);
    }

    void SpawnANode(Vector3 pos)
    {
        GameObject inst = Instantiate(node_pref, pos, Quaternion.identity);
        inst.transform.parent = nodesParent.transform;

        NodeManager nodeManager = inst.GetComponent<NodeManager>();
        nodeManager.SetNum(currentNodeIndex++);

        prevNode = inst; 

        GraphManager.Instance.AddNode(inst);
    }
}
