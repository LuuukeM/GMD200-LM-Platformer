using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceClimb : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ice Climb"))
        {

        }
    }
}
