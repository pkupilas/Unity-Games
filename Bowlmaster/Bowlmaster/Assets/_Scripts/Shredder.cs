using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

    public void OnTriggerExit(Collider coll)
    {
        GameObject leftObject = coll.gameObject;

        if (leftObject.GetComponent<Pin>() != null)
        {
            Destroy(leftObject);
        }
    }
}
