//https://www.youtube.com/watch?v=ID5JzxQOjnE&feature=emb_logo&ab_channel=OnlineCodeCoaching 
// Feb 2 2021
// MMackenzie

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nado : MonoBehaviour
{
    public Transform tornadoCenter;
    public float pullforce;
    public float refreshRate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "OBJ")
        {
            StartCoroutine(pullObject(other, true));
        }
    }

    private void OnTriggerExit(Collider other)
    {
         if(other.tag == "OBJ")
        {
            StartCoroutine(pullObject(other, false));
        }
    }

    IEnumerator pullObject(Collider x, bool shouldPull)
    {
        if(shouldPull)
        {
            Vector3 ForceDir = tornadoCenter.position - x.transform.position;
            x.GetComponent<Rigidbody>().AddForce(ForceDir.normalized * pullforce * Time.deltaTime);
            yield return refreshRate;
            StartCoroutine(pullObject(x, shouldPull));
        }
    }
}
