using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderHealth : MonoBehaviour
{
    [SerializeField] GameObject boulder;
    [SerializeField] ParticleSystem boulderExplosion;
    [SerializeField] AudioBehaviour boulderSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("AttackArea"))
        {
            boulderExplosion.Play();
            boulder.SetActive(false);
            boulderSound.GetComponent<AudioSource>().Play();
        }

    }
}
