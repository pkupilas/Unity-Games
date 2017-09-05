using UnityEngine;

namespace WorldObjects.Shredders
{
    public class Shredder : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var bulletComponent = other.gameObject.GetComponent<Bullet>();
            if (bulletComponent)
            {
                Destroy(bulletComponent.gameObject);
            }
        }
    }
}