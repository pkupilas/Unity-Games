using UnityEngine;

namespace Weapons.Guns
{
    public abstract class Firearm : Weapon
    {
        protected Ammunition ammunition;

        public Ammunition Ammunition => ammunition;
        
        protected override void Start()
        {
            base.Start();
            ammunition = GetComponent<Ammunition>();
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.R))
            {
                ammunition.Reload();
            }
        }
    }
}
