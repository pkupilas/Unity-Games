using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Weapons.Guns
{
    public abstract class Firearm : Weapon
    {
        protected Ammunition ammunition;
        
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
