using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameObject obj;
    Transform trns;
    Rigidbody rigid;
    float x;
    float y;
    float z;

    void Awake()
    {
        trns = GetComponent<Transform>();
        x = trns.position.x; y = trns.position.y; z = trns.position.z;
        rigid = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reset")
        {
            if (obj.tag == "Sugar")
            {
                obj.transform.position = new Vector3(x, y, z);
                obj.transform.rotation = Quaternion.identity;
            }
            else if (obj.tag == "BakingSoda")
            {
                obj.transform.position = new Vector3(x, y, z);
                obj.transform.rotation = Quaternion.Euler(0, -38.612f, 0);
            }
            else if (obj.tag == "MixStick")
            {
                obj.transform.position = new Vector3(x, y, z);
                obj.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if(obj.tag == "Press")
            {
                obj.transform.position = new Vector3(x, y, z);
                obj.transform.rotation = Quaternion.identity;
            }
            else if (obj.tag == "Frame")
            {
                obj.transform.position = new Vector3(x, y, z);
                obj.transform.rotation = Quaternion.Euler(0, -30, 0);
            }

            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
}
