using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAttraction : ObjForPool
{
    [SerializeField]
    private float moveSpeed = 0.4f;
    private bool isAttraction = false;
    private Vector3 targetDir;

    private void Update()
    {
        if(isAttraction)
        {
            Attraction(targetDir);
        }
    }
    
    private void Attraction(Vector3 snakePosition)
    {
        float distance = Vector3.Distance(transform.position, snakePosition);
        if(this.gameObject.tag == "Bomb")
        {
            GameObject [] bombElement = GetComponent<BombExplosion>().bombElements;

            foreach(GameObject element in bombElement)
            {
                element.GetComponent<Collider>().isTrigger = true;
            }
        }
        if(distance < 0.1f)
        {
            isAttraction = false;
            GetComponent<Collider>().enabled = true;
            DeactivateObject();
        }
        else
        {
            transform.position = Vector3.Lerp(snakePosition, transform.position, moveSpeed);
        }
    }

    public void StartAttraction(Vector3 pos, float dist)
    {
        isAttraction = true;
        targetDir = pos;
        if(this.gameObject.tag == "Bomb")
        {
            Debug.Log(1);
        }
    }

    public void StopAttraction()
    {
        isAttraction = false;
        targetDir = this.transform.position;
    }

}
