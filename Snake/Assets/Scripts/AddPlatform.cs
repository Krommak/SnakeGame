using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlatform : MonoBehaviour
{
    [SerializeField]
    WorldBuilder worldBuilder;
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "PlatformAddPoint")
        {
            worldBuilder.CreatePlatform();
        }
    }
}
