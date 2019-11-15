using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCollider : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().drag = 2f;
            Debug.Log("In");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().drag = 0f;
            Debug.Log("Out");
        }
    }
}
