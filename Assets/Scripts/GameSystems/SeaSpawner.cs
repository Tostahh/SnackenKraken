using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeaSpawner : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private TextMeshProUGUI TimerDisplay;

    [Header("Critters")]
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private GameObject[] fishies;

    [Header("Attributes")]
    [SerializeField] private float eps = 0.2f;
    [SerializeField] private float cps = 1f;
    [SerializeField] private float fps = 1f;
    [SerializeField] private float Range = 10f;
    [SerializeField] private LayerMask CritterMask;

    public List<Critter> SeaObjects = new List<Critter>();

    private GameSystems Us;
    private float TimeSinceStart;
    private int done;

    private float timeSinceLastSpawnS;
    private float timeSinceLastSpawnf;
    private float timeSinceLastCheck;
    private GameObject Player;

    private void Awake()
    {
        Time.timeScale = 0;
        Us = FindObjectOfType<GameSystems>();
        Player = FindObjectOfType<SeaCritterController>().gameObject;
        timeSinceLastSpawnf = (1f / fps);
    }
    private void OnEnable()
    {
        GameSystems.EnterBoss += EnterBoss;
        GameSystems.ExitBoss += ExitBoss;
    }
    private void OnDisable()
    {
        GameSystems.EnterBoss -= EnterBoss;
        GameSystems.ExitBoss -= ExitBoss;
    }

    private void OnGUI()
    {
        TimerDisplay.text = TimeSinceStart.ToString("F2");
    }

    private void Update()
    {
        if(Us.InPlay)
        {
            TimeSinceStart += Time.deltaTime;
            if (TimeSinceStart >= 60 && done < 1)
            {
                done++;
                eps += 0.04f;
                fps += 0.2f;
            }
            if(TimeSinceStart >= 120 && done < 2)
            {
                done++;
                eps += 0.06f;
                fps += 0.5f;
            }
            if(TimeSinceStart >= 180 && done < 3)
            {
                done++;
                eps += 0.1f;
                fps += 0.7f;
            }
            if (TimeSinceStart >= 240 && done < 4)
            {
                done++;
                eps += 0.12f;
                fps += 1f;
            }
            if (TimeSinceStart >= 300 && done < 5)
            {
                done++;
                eps += 0.3f;
                fps += 1.5f;
            }
            if(TimeSinceStart >= 1200 && done < 6)
            {
                done++;
                eps = 30f;
                fps = 0;
            }
        }

        if (!Us.BossState)
        {
            timeSinceLastCheck += Time.deltaTime;
            timeSinceLastSpawnf += Time.deltaTime;
            timeSinceLastSpawnS += Time.deltaTime;
            if (timeSinceLastCheck >= (1f / cps))
            {
                Check();
                timeSinceLastCheck = 0;
            }
            if (timeSinceLastSpawnS >= (1f / eps))
            {
                SpawnShark();
                timeSinceLastSpawnS = 0;
            }
            if (timeSinceLastSpawnf >= (1f / fps))
            {
                SpawnFishy();
                timeSinceLastSpawnf = 0;
            }
        }
    }
    private void SpawnShark()
    {
        int x = Random.Range(1, 3);
        int y = Random.Range(1, 3);

        if (x == 1)
        {
            if (y == 1)
            {
                int index = Random.Range(0, Enemies.Length);
                GameObject prefabToSpawn = Enemies[index];
                GameObject E = Instantiate(prefabToSpawn, transform.position + new Vector3(10 + Random.Range(0f, 3f), Random.Range(-6, 6), +5), Quaternion.identity);

            }
            else
            {
                int index = Random.Range(0, Enemies.Length);
                GameObject prefabToSpawn = Enemies[index];
                GameObject E = Instantiate(prefabToSpawn, transform.position + new Vector3(-10 + Random.Range(-3f,0f), Random.Range(-6, 6), +5), Quaternion.identity);

            }
        }
        else
        {
            if (y == 1)
            {
                int index = Random.Range(0, Enemies.Length);
                GameObject prefabToSpawn = Enemies[index];
                GameObject E = Instantiate(prefabToSpawn, transform.position + new Vector3(Random.Range(-10, 10), 6 + Random.Range(0f, 3f), +5), Quaternion.identity);

            }
            else
            {
                int index = Random.Range(0, Enemies.Length);
                GameObject prefabToSpawn = Enemies[index];
                GameObject E = Instantiate(prefabToSpawn, transform.position + new Vector3(Random.Range(-10, 10), -6 + Random.Range(-3f,0f), +5), Quaternion.identity);

            }
        }
    }
    private void SpawnFishy()
    {
        int x = Random.Range(1, 3);
        int y = Random.Range(1, 3);

        if (x == 1)
        {
            if (y == 1)
            {
                int index = Random.Range(0, fishies.Length);
                GameObject prefabToSpawn = fishies[index];
                GameObject E = Instantiate(prefabToSpawn, transform.position + new Vector3(10 + Random.Range(0f, 3f), Random.Range(-6, 6), +5), Quaternion.identity);

            }
            else
            {
                int index = Random.Range(0, fishies.Length);
                GameObject prefabToSpawn = fishies[index];
                GameObject E = Instantiate(prefabToSpawn, transform.position + new Vector3(-10 + Random.Range(-3f,0f), Random.Range(-6, 6), +5), Quaternion.identity);

            }
        }
        else
        {
            if (y == 1)
            {
                int index = Random.Range(0, fishies.Length);
                GameObject prefabToSpawn = fishies[index];
                GameObject E = Instantiate(prefabToSpawn, transform.position + new Vector3(Random.Range(-10, 10), 6 + Random.Range(0f, 3f), +5), Quaternion.identity);

            }
            else
            {
                int index = Random.Range(0, fishies.Length);
                GameObject prefabToSpawn = fishies[index];
                GameObject E = Instantiate(prefabToSpawn, transform.position + new Vector3(Random.Range(-10, 10), -6 + Random.Range(-3f, 0f), +5), Quaternion.identity);

            }
        }
    }

    private void Check()
    {
        List<Critter> RemoveCritters = new List<Critter>();

        if(SeaObjects.Count > 0)
        {
            foreach(Critter c in SeaObjects)
            {
                if(Vector3.Distance(Player.transform.position, c.position) > Range)
                {
                    if(c.gameObject == null)
                    {
                        RemoveCritters.Add(c);
                    }
                    else
                    {
                        if (c.Ignore)
                        {
                            Debug.Log("skip");
                        }
                        else
                        {
                            c.gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    if(c.gameObject == null)
                    {
                        RemoveCritters.Add(c);
                    }
                    else
                    {
                        if (c.Ignore)
                        {
                            Debug.Log("skip");
                        }
                        else
                        {
                            c.gameObject.SetActive(true);
                        }
                    }
                }
            }


        }

        if(RemoveCritters.Count > 0)
        {
            foreach(Critter c in RemoveCritters)
            {
                SeaObjects.Remove(c);
            }
        }
    }

    private void EnterBoss()
    {
        foreach(Critter c in SeaObjects)
        {
            c.gameObject.SetActive(false);
        }
    }
    private void ExitBoss()
    {
        foreach (Critter c in SeaObjects)
        {
            if (c != null)
            {
                c.gameObject.SetActive(true);
            }
        }
    }

    public void ResetGame()
    {
        foreach(Critter c in SeaObjects)
        {
            Destroy(c.gameObject);
        }

        SeaObjects.Clear();

        TimeSinceStart = 0;
        eps = 0.09f;
        fps = 0.8f;
        Player.gameObject.transform.position = new Vector3(0, 0, 0);
        Player.GetComponent<SeaCritterController>().KnockedBack = false;
        Player.GetComponent<SeaCritterController>().SpecailIndicator = 0;
        Player.GetComponent<SeaCritterController>().ResetPower();
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Player.transform.localScale = new Vector3(1, 1, 1);
    }
}

[System.Serializable]
public class Critter
{
    public GameObject gameObject;
    public Vector3 position;
    public bool Ignore;
}