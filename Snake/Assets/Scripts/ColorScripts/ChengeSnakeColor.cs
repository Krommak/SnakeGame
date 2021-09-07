using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChengeSnakeColor : MonoBehaviour
{
    [SerializeField]
    MeshRenderer CheckPointMR;
    Material actualColor;

    int colorIndex;
    void OnTriggerEnter(Collider collider)
    {
        actualColor = CheckPointMR.material;
        Material [] mat = collider.GetComponent<MeshRenderer>().materials;

        for(int i = 0; i < mat.Length; i++)
        {
            mat[i] = actualColor;
        }
        if(collider.gameObject.tag == "Player")
        {
            collider.transform.root.GetComponent<SnakeController>().SetSnakeColor(colorIndex);
        }

        collider.GetComponent<Renderer>().materials = mat;
    }

    public void SetColorIndex(int index)
    {
        colorIndex = index; 
    }
}
