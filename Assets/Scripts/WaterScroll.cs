using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScroll : MonoBehaviour
{
    public float MainScrollX = 0.1f;
    public float MainScrollY = 0.1f;

    void Update()
    {
        float MainOffsetX = Time.time * MainScrollX;
        float MainOffsetY = Time.time * MainScrollY;
        GetComponent<Renderer> ().material.mainTextureOffset = new Vector2 (MainOffsetX, MainOffsetY);
    }
}
