using UnityEngine;

namespace _Characters.CommonScripts
{
    public class WeapointContainer : MonoBehaviour
    {
        void OnDrawGizmos()
        {
            foreach (Transform childTransform in transform)
            {
                Gizmos.color = new Color(255f, 0f, 0f, 0.5f);
                Gizmos.DrawSphere(childTransform.position, 0.5f);
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = new Color(0f, 0f, 255f, 0.5f);
                Gizmos.DrawLine(transform.GetChild(i).position,
                    i == transform.childCount - 1 ? transform.GetChild(0).position : transform.GetChild(i + 1).position);
            }
        }
    }
}
