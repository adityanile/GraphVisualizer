using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    public static GraphManager Instance;

    [SerializeField]
    private List<NodeManager> nodes = new ();

    [SerializeField]
    private List<EdgeManager> edges = new ();

    public int[,] graph;

    public Dijkstra dijk;

    // Start is called before the first frame update
    void Start()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    public async Task CreateGraph()
    {
        graph = new int[nodes.Count, nodes.Count];

        foreach(var edge in edges)
        {
            int i = edge.node1.num;
            int j = edge.node2.num;

            int weight = edge.weight;

            graph[i, j] = weight;
            graph[j, i] = weight;
        }

        // Now the adjacency matrix is created successfully
        dijk.Init();
        await dijk.ShortestPath(0);
    }

    // Utilities to Visualise
    public EdgeManager FindEdge(int src, int dst)
    {
        string str1 = src.ToString() + "," + dst.ToString();
        string str2 = dst.ToString() + "," + src.ToString();

        var edge = from e in edges where e.gameObject.name.Equals(str1) || e.gameObject.name.Equals(str2) select e;

        if (edge.Count() > 0)
            return edge.First();
        else
            return null;
    }

    public void AddNode(GameObject node)
    {
        nodes.Add(node.GetComponent<NodeManager>());
    }
    public void AddEdge(EdgeManager edge)
    {
        edges.Add(edge);
    }
}
