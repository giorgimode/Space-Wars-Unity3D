using UnityEngine;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
   
    public int scoreValue;
    private Done_GameController gameController;
    private PlayerController scoreController;
    private GameObject gameControllerObject;
    private GameObject scoreControllerObject;
    void Start()
    {
        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        scoreControllerObject = GameObject.FindGameObjectWithTag("Player");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<Done_GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        if (scoreControllerObject != null)
        {
            scoreController = scoreControllerObject.GetComponent<PlayerController>();
        }
        if (scoreController == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        }
    }

    void OnCollisionEnter(Collision other)
    {   // fix refine change here *****needs trigger, not collision*******************************
         // Change here
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerProjectile" || other.gameObject.tag == "PlayerProjectileHoming")
        {
                if (explosion != null)
                    Instantiate(explosion, transform.position, transform.rotation);
                else Debug.Log("Explosion Object Not found");

                if (scoreController != null)
                    scoreController.AddScore(scoreValue);
                else Debug.Log("Player Not Found by scoreControllerObject");
                //	Destroy (other.gameObject);
                Destroy(gameObject);
             //	gameController.GameOver();}
            //	Debug.Log ("Yes I hit you");
        }

         
        
    }
}