using UnityEngine;
using Weapons.Ammunition;

namespace WorldObjects.Shredders
{
    public class Shredder : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            var bulletComponent = other.gameObject.GetComponent<Bullet>();
            if (bulletComponent)
            {
                Destroy(bulletComponent.gameObject);
            }
        }
    }
}