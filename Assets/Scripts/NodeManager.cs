using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public int num;
    public TextMeshProUGUI num_text;

    public void SetNum(int n)
    {
        num = n;
        gameObject.name = "Node"+num.ToString();
        num_text.text = num.ToString();
    }
}
