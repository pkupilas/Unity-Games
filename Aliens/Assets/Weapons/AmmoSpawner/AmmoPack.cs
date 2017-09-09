using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    [SerializeField] private AudioClip _pickupClip;

    private AudioSource _audioSource;

    public GameObject weaponType;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _pickupClip;
    }

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player)
        {
            player.GetComponent<Player>().AddMagazine(weaponType.transform.GetChild(0).gameObject);
            _audioSource.Play();
            Destroy(gameObject, _pickupClip.length);
        }
    }

}
