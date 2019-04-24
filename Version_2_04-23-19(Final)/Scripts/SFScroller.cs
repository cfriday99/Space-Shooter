using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFScroller : MonoBehaviour
{
    private ParticleSystem ps;
    private float hSliderValue = 1.0F;
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (gameController != null)
        {
            gameController = gameController.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        var main = ps.main;
        main.simulationSpeed = hSliderValue;

        if (gameController.winCondition == true)
        {
            if (hSliderValue <= 7)
            {
                hSliderValue += Time.deltaTime;
            }
        }
    }
}
