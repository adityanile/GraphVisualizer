using System;
using System.Threading.Tasks;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    int vertices = 0;
    int[,] graph;

    public void Init()
    {
        vertices = GraphManager.Instance.graph.GetLength(0);
        graph = GraphManager.Instance.graph;
    }

    public async Task ShortestPath(int source)
    {
        // To keep track of the distance for start vertice
        int[] dist = new int[vertices];

        // To keep the track of shortest distance vertices
        bool[] sdVertice = new bool[vertices];

        for (int i = 0; i < vertices; i++)
        {
            dist[i] = int.MaxValue;
            sdVertice[i] = false;
        }

        // Now set the start vertice dist to be 0
        dist[source] = 0;

        // Finding shortest path from source to all the vertices    
        for (int i = source; i < vertices - 1; i++)
        {
            
            // here we will get the shortest vertice from the unvisited list
            int u = MinDist(dist, sdVertice);

            // Select this vertice and check its corresponding vertices
            sdVertice[u] = true;

            // Checking neighbouring vertices of the current selection
            for (int j = 0; j < vertices; j++)
            {
                EdgeManager edgeM = GraphManager.Instance.FindEdge(i, j);

                if (!sdVertice[j] && graph[i, j] != 0 &&
                     dist[i] != int.MaxValue &&
                     dist[i] + graph[i, j] < dist[j])
                {
                    if(edgeM)
                    edgeM.SelectEdge();

                    await Task.Delay(1000);

                    dist[j] = dist[i] + graph[i, j];
                }

            }
        }
    }

    int MinDist(int[] dist, bool[] spVisited)
    {
        int min = int.MaxValue, min_index = -1;

        for (int i = 0; i < vertices; i++)
        {
            if (!spVisited[i] && dist[i] <= min)
            {
                min = dist[i];
                min_index = i;
            }
        }
        return min_index;
    }
}
