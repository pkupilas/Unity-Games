using UnityEngine;

namespace _Characters.Enemies
{
    public class FaceToCamera : MonoBehaviour
    {
        void LateUpdate()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}