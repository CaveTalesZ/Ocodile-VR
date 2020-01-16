using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScroll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    
    }

    public float MainScrollX = 0.1f;
    public float MainScrollY = 0.1f;

    // Update is called once per frame
    void Update()
    {
        float MainOffsetX = Time.time * MainScrollX;
        float MainOffsetY = Time.time * MainScrollY;
        GetComponent<Renderer> ().material.mainTextureOffset = new Vector2 (MainOffsetX, MainOffsetY);
    }
}
