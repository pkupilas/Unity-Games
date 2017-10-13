using UnityEngine;

public class Position : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
