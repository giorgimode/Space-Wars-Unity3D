using UnityEngine;
using System.Collections;

public class PlayerLives : MonoBehaviour
{
   // [HideInInspector]
    public int lives=4;
    public static PlayerLives instance;
    // Use this for initialization

    void Awake()
    {

        if (instance)
                 DestroyImmediate(transform.gameObject);
             else
             {
                 DontDestroyOnLoad(gameObject); DontDestroyOnLoad(transform.gameObject);
                 instance = this;
             }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
