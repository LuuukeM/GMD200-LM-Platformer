using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform respawn;
    public GameObject Checkpoint;
    // private Renderer rend;
    [SerializeField] private Color checkpointColor = Color.red;
    [SerializeField] private Color baseCheckpointColor = Color.green;

    private void Start()
    {
        //rend = GetComponent<Renderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            //Respawn();
            transform.position = respawn.transform.position;
        }
        else if (other.gameObject.CompareTag("Checkpoint"))
        {
            Checkpoint.GetComponent<SpriteRenderer>().color = baseCheckpointColor;
            respawn = other.transform;
            Checkpoint = other.gameObject;
            other.GetComponent<SpriteRenderer>().color = checkpointColor;
        }
    }
}