using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBossAiManager : AiBoss
{
    [SerializeField] private SnakeBossAi snake;

    [SerializeField] private GameObject SnakeBoss2;

    private bool Done;
    private void Start()
    {
        snake = FindObjectOfType<SnakeBossAi>();
    }
    private void Update()
    {
        if(!snake)
        {
            Destroy(gameObject);
        }
        if(snake.Phase == 2)
        {
            if(!Done)
            {
                Done = true;
                SnakeBoss2.SetActive(true);
            }
        }
    }
}
