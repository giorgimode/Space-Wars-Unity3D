using UnityEngine;
using System.Collections;

public class Level02 : MonoBehaviour {

    public GameObject[] powerPlantsMain = new GameObject[7];
    public GameObject[] powerPlantsBase = new GameObject[3];

	private int numPowerPlants;
    private GameObject shield;
    private GameObject player;
    private GameObject boss;
    private GameObject Arrow;
    private ArrowPoint AP;
    private enum state {phase01, phase02, phase03, phase04};
    private state myState;
	private float loadWait;
	private float loadWaitDuration = 5.0f;
	private Boss02 bossScript;
	private bool setWait = true;
	// Use this for initialization
	void Start () 
    {
        myState = state.phase01;
        player = GameObject.FindGameObjectWithTag("Player");
        Arrow = GameObject.FindGameObjectWithTag("Arrow");
        shield = GameObject.FindGameObjectWithTag("Shield");
        boss = GameObject.FindGameObjectWithTag("Boss2");
		bossScript = boss.GetComponent<Boss02> ();

        AP = Arrow.GetComponent<ArrowPoint>();

		numPowerPlants = 0;
		foreach (GameObject go in powerPlantsMain) 
		{
			if(go != null)
			{
				numPowerPlants++;
			}
		}

		foreach (GameObject go in powerPlantsBase) 
		{
			if(go != null)
			{
				numPowerPlants++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
    {
		changePhase ();
        switch (myState)
        {
            case state.phase01:
                AP.target = closestPhase1();
                break;
            case state.phase02:
				shrinkShield();
                AP.target = closestPhase2();
                break;
            case state.phase03:
                AP.target = boss;
				if(bossScript.myBossState == Boss02.bossState.idel)
				{bossScript.Activate();	}
                break;
            case state.phase04:
                Destroy(Arrow);
				NextLevel();
                break;
            default:

                break;
        }
	}

	private void NextLevel()
	{
		if(setWait)
		{
			loadWait = Time.time + loadWaitDuration;
			setWait = false;
		}

		if(loadWait < Time.time)
		{
			Application.LoadLevel("TransitionLevel3");
		}

	}

	private void changePhase()
	{
		switch (myState) 
		{
			case state.phase01:
				bool mainpp = false;
				foreach(GameObject go in powerPlantsMain)
				{
					if(go != null)
					{
						mainpp = true;
						break;
					}
				}
				if(!mainpp)
				{myState = state.phase02;}	
				break;
			case state.phase02:
				bool basepp = false;
				
				shrinkShield();
				foreach(GameObject go in powerPlantsBase)
				{
					if(go != null)
					{
						basepp = true;
						break;
					}
				}
				if(!basepp)
				{
					DestroyShield();
					myState = state.phase03;
				}	
				break;
			case state.phase03:
				if(boss == null)
				{myState = state.phase04;}
				break;
			case state.phase04:
				break;
			default:
				break;
		}
	}

    private GameObject closestPhase1()
    {
        GameObject closest = new GameObject();
        float dist = 9999999.9f;
        foreach (GameObject GO in powerPlantsMain)
        {
            if (GO != null)
            {
                float d = Vector3.Distance(player.transform.position, GO.transform.position); 
                if (d < dist)
                {
                    dist = d;
                    closest = GO;
                }
            }
        }
        return closest;
    }

    private GameObject closestPhase2()
    {
        GameObject closest = new GameObject();
        float dist = 9999999.9f;
        foreach (GameObject GO in powerPlantsBase)
        {
            if (GO != null)
            {
                float d = Vector3.Distance(player.transform.position, GO.transform.position);
                if (d < dist)
                {
                    dist = d;
                    closest = GO;
                }
            }
        }
        return closest;
    }

    private void shrinkShield()
    {
		shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, new Vector3(600f, 600f, 600f), 1.0f * Time.deltaTime);

    }

    private void DestroyShield()
    {
		Destroy (shield);
    }

	public void LaunchWave()
	{
		numPowerPlants--;

	}

   
}
