using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjForPool : MonoBehaviour
{
    public void ActivateObject(Vector3 pos, Transform parent)
    {
        this.gameObject.SetActive(true);
        transform.position = pos;
        transform.SetParent(parent);
        if(this.gameObject.tag == "Bomb")
        {
            GameObject [] bombElement = GetComponent<BombExplosion>().bombElements;

            foreach(GameObject element in bombElement)
            {
                element.GetComponent<Collider>().isTrigger = false;
            }
        }
    }
    public void ActivateObject(Vector3 pos)
    {
        this.gameObject.SetActive(true);
        transform.position = pos;
    }
    public void DeactivateObject()
    {
        transform.position = Vector3.zero;
        transform.SetParent(null);
        if (this.gameObject.tag == "Human" || this.gameObject.tag == "Gem")
        {
            this.gameObject.GetComponent<FoodAttraction>().StopAttraction();
        }
        this.gameObject.SetActive(false);
    }
}
