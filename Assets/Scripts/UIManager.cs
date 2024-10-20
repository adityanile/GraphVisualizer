using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public bool graphCreated = false;
    
    public void OnClickDone()
    {
        if (!graphCreated)
        {
            GraphManager.Instance.CreateGraph();
            graphCreated = true;    
        }
    }
}
