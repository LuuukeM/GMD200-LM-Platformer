using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public GameObject Checkpoint;
    [SerializeField] private Transform respawn;
    // private Renderer rend;
    [SerializeField] private Color checkpointColor = Color.red;
    [SerializeField] private Color baseCheckpointColor = Color.green;

    private void Start()
    {
        respawn.position = Checkpoint.transform.position;
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