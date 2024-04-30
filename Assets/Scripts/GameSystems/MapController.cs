using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private List<GameObject> SeaChunks;
    [SerializeField] private LayerMask SeaMask;

    [SerializeField] private float checkRadius;

    private int BossChance;

    private Vector3 noSeaPos;
    private SeaCritterController Sc;
    private GameSystems Gs;

    private int BDone;
    
    public GameObject CurrentSeaChunk;

    private void Awake()
    {
        Sc = FindObjectOfType<SeaCritterController>();
        Gs = FindObjectOfType<GameSystems>();
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
        int x = Random.Range(0, SeaChunks.Count-3);

        int BC = Random.Range(BossChance, 51);

        if (BC == BossChance && Gs.BossNumb == 0 && BDone == 0)
        {
            BossChance = 5;
            BDone++;
            Mathf.Clamp(BossChance, 0, 50);
            Instantiate(SeaChunks[3], noSeaPos, Quaternion.identity);
        }
        else if (BC == BossChance && Gs.BossNumb == 1 && BDone == 1)
        {
            BossChance = 20;
            BDone++;
            Mathf.Clamp(BossChance, 0, 50);
            Instantiate(SeaChunks[4], noSeaPos, Quaternion.identity);
        }
        else if(BC == BossChance && Gs.BossNumb == 2 && BDone == 2)
        {
            BossChance = 35;
            BDone++;
            Mathf.Clamp(BossChance, 0, 50);
            Instantiate(SeaChunks[5], noSeaPos, Quaternion.identity);
        }
        else
        {
            Instantiate(SeaChunks[x], noSeaPos, Quaternion.identity);
            BossChance += 1;
        }
    }
    public void ResetGame()
    {
        BossChance = 0;
        BDone = 0;
        CurrentSeaChunk = null;
    }
}
