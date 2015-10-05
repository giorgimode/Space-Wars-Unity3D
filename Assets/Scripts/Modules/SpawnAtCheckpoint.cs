
using UnityEngine;
using System.Collections;
//using System.Math;
public class SpawnAtCheckpoint : MonoBehaviour
{

    private static float respawnTime = 5.0f;
    private bool paused = false;
    private bool dead = false;
    private Health playerHealth;
  private  Transform checkpoint;
  public Texture2D lifeImage;
    public GUIStyle myStyle;

  private int score;
  private DoneSceneFadeInOut sceneFadeInOut;			// Reference to the SceneFadeInOut script.
  private PlayerLives playerLives;

  void Awake()
  {
      score = 0;
      checkpoint = transform;
      playerHealth = gameObject.GetComponent<Health>();
      sceneFadeInOut = GameObject.FindGameObjectWithTag(DoneTags.fader).GetComponent<DoneSceneFadeInOut>();

      playerLives = GameObject.FindGameObjectWithTag("PlayerLives").GetComponent<PlayerLives>();

  }
public    void OnSignal()
    {
        transform.position = checkpoint.position;
        transform.rotation = checkpoint.rotation;

        ResetHealthOnAll();
    }

public void AddScore(int newScoreValue)
{
    score += newScoreValue;

}

static IEnumerator ResetHealthOnAll()
    {
        Health[] healthObjects = FindObjectsOfType(typeof(Health)) as Health[];
        foreach (Health health in healthObjects)
        {
            health.dead = false;
            health.health = health.maxHealth;
        }
        yield return new WaitForSeconds(respawnTime);
    }

void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1;
        }
        else
        {
            paused = true;
            Time.timeScale = 0;
        }
    }


}
void OnGUI()
{
    
    //Counts displaed on HUD
    GUI.Label(new Rect(Screen.width / 1.4f, 0, 160, 110), "Health ", myStyle); 
   // GUI.Label(new Rect((Screen.width - 120) / 2, 0, 120, 20), "Health: " + Mathf.Round(playerHealth.health), myStyle);
    GUI.Label(new Rect(Screen.width / 6f, 0, 160, 110), "SCORE: " + score, myStyle);
    for (int i = 1; i <= playerLives.lives; i++)
        GUI.DrawTexture(new Rect(Screen.width / 100 + i / 1.1f * lifeImage.width, Screen.height - lifeImage.height * 1.4f, lifeImage.width/1.4f, lifeImage.height), lifeImage);
//GUI.Label(new Rect((Screen.width - 120) / 2 - 400, 0, 120, 20), "Lives: " + Mathf.Round(playerLives.lives), myStyle);

    
    if (paused && !dead)
    {
        GUI.skin.button.fontSize = Screen.width / 20;
        if (GUI.Button(new Rect(Screen.width / 3, Screen.height / 2 - 15, 500, 130), "Exit to menu"))
        {
            Application.LoadLevel("StartMenu");
            
            Time.timeScale = 1;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 4, 500, 130), "Restart Level"))
        {   
            paused = false;
            Time.timeScale = 1;
            Application.LoadLevel(Application.loadedLevel);
            
            
        }
    }

    //if (dead)
    //{
    //    GUI.skin.button.fontSize = Screen.width / 80;
    //    GUI.skin.button.fontStyle = FontStyle.Bold;
    //    if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2, 180, 90), "Exit to Menu"))
    //    {
    //        Application.LoadLevel("StartMenu");
    //        dead = false;
    //        Time.timeScale = 1;
    //    }

    //    if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 4, 180, 90), "Restart Level"))
    //    {
    //        playerHealth.health = 0;

    //        paused = false;
    //        Time.timeScale = 1;
    //    }

    //}

   

}


}