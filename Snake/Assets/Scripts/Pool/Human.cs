using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : ObjForPool
{
    private Material humanColor;
    int colorIndex;

    public void ActivateObject(Vector3 pos, Material color, int index, Transform parent)
    {
        transform.position = pos;
        this.GetComponent<MeshRenderer>().material = color;
        colorIndex = index;
        humanColor = color;
        transform.SetParent(parent);
        this.gameObject.SetActive(true);
    }

    public int GetColor()
    {
        return colorIndex;
    }
}
