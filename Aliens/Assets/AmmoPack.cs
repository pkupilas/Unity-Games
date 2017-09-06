using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    public GameObject weaponType;

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player)
        {
            player.GetComponent<Player>().AddMagazine(weaponType.transform.GetChild(0).gameObject);
            Destroy(gameObject);
        }
    }

}
