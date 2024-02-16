using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointInteract : MonoBehaviour
{
    private Renderer rend;

    [SerializeField] private Color checkpointColor = Color.red;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rend.material.color = checkpointColor;
        }
    }

}
