using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private List<GameObject> SeaChunks;
    [SerializeField] private LayerMask SeaMask;

    [SerializeField] private float checkRadius;

    private Vector3 noSeaPos;
    private SeaCritterController Sc;
    
    public GameObject CurrentSeaChunk;

    private void Awake()
    {
        Sc = FindObjectOfType<SeaCritterController>();
    }

    private void Update()
    {
        SeaCheck();
    }
    private void SeaCheck()
    {
        if(!CurrentSeaChunk)
        {
            return;
        }

        if(Sc.Direction.y > 0)
        {
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Up").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Up").position;
                SpawnSeaChunk();
            }

            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Left Up").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Left Up").position;
                SpawnSeaChunk();
            }
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Right Up").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Right Up").position;
                SpawnSeaChunk();
            }
        }

        if(Sc.Direction.y < 0)
        {
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Down").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Down").position;
                SpawnSeaChunk();
            }
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Left Down").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Left Down").position;
                SpawnSeaChunk();
            }
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Right Down").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Right Down").position;
                SpawnSeaChunk();
            }
        }

        if(Sc.Direction.x > 0)
        {
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Right").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Right").position;
                SpawnSeaChunk();
            }
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Right Down").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Right Down").position;
                SpawnSeaChunk();
            }
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Right Up").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Right Up").position;
                SpawnSeaChunk();
            }
        }

        if(Sc.Direction.x < 0)
        {
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Left").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Left").position;
                SpawnSeaChunk();
            }
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Left Down").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Left Down").position;
                SpawnSeaChunk();
            }
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Left Up").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Left Up").position;
                SpawnSeaChunk();
            }
        }



        if (Sc.rb.velocity.y > 0)
        {
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Up").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Up").position;
                SpawnSeaChunk();
            }

            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Left Up").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Left Up").position;
                SpawnSeaChunk();
            }
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Right Up").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Right Up").position;
                SpawnSeaChunk();
            }
        }
        if (Sc.rb.velocity.y < 0)
        {
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Down").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Down").position;
                SpawnSeaChunk();
            }
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Left Down").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Left Down").position;
                SpawnSeaChunk();
            }
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Right Down").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Right Down").position;
                SpawnSeaChunk();
            }
        }
        if (Sc.rb.velocity.x > 0)
        {
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Right").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Right").position;
                SpawnSeaChunk();
            }
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Right Down").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Right Down").position;
                SpawnSeaChunk();
            }
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Right Up").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Right Up").position;
                SpawnSeaChunk();
            }
        }
        if (Sc.rb.velocity.x < 0)
        {
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Left").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Left").position;
                SpawnSeaChunk();
            }
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Left Down").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Left Down").position;
                SpawnSeaChunk();
            }
            if (!Physics2D.OverlapCircle(CurrentSeaChunk.transform.Find("Left Up").position, checkRadius, SeaMask))
            {
                noSeaPos = CurrentSeaChunk.transform.Find("Left Up").position;
                SpawnSeaChunk();
            }
        }
    }
    private void SpawnSeaChunk()
    {
        int x = Random.Range(0, SeaChunks.Count);

        Instantiate(SeaChunks[x], noSeaPos, Quaternion.identity);
    }
    public void ResetGame()
    {
        CurrentSeaChunk = null;
    }
}
