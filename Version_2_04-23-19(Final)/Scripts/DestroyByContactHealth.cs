using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContactHealth : MonoBehaviour
{
    public GameObject explosion;
    public GameObject explosionS;
    public GameObject playerExplosion;
    public int scoreValue;
    public int startingHealth;
    public int currentHealth;

    private GameController gameController;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (explosionS != null)
        {
            Instantiate(explosionS, transform.position, transform.rotation);
        }

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        currentHealth = currentHealth - 1;
        Destroy(other.gameObject);

        if (currentHealth <= 0)
        {
            gameController.AddScore(scoreValue);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }
}
