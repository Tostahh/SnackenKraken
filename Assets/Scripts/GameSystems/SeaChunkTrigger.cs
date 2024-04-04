using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaChunkTrigger : MonoBehaviour
{
    private MapController Mc;
    public GameObject Target;

    private void Awake()
    {
        Mc = FindObjectOfType<MapController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Mc.CurrentSeaChunk = Target;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(Mc.CurrentSeaChunk == Target)
            {
                Mc.CurrentSeaChunk = null;
            }
        }
    }
}
