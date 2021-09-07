using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    public GameObject [] bombElements;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && !collision.transform.root.GetComponent<SnakeController>().GetIsFever())
        {
            Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
            collision.transform.root.GetComponent<SnakeController>().EndGame(true);
        }
    }
}
