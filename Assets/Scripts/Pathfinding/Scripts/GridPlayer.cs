using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridPlayer : Pathfinding
{
 //   public Camera playerCam;
 //   public Camera minimapCam;

	//public GUIStyle bgStyle;
   private GameObject testPlayer;
   private Transform testPlayerTransform;
   private KamikazeMovementMotor movementMotor;
	private BuzzerKamikazeControllerAndAi AIcontroller;
	float distance;
	private DoneLastPlayerSighting lastPlayerSighting;
	private EnemySight enemySight;	
	[HideInInspector]
	public bool pathfindEnable = true;
	void Awake()
	{
		lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<DoneLastPlayerSighting>();
		enemySight = GetComponent<EnemySight>();
	}
	void Start()
	{
		testPlayer = GameObject.FindWithTag("Player");
		testPlayerTransform = testPlayer.transform;

	   
		movementMotor = GetComponent<KamikazeMovementMotor>();
		AIcontroller = GetComponent<BuzzerKamikazeControllerAndAi>();
		
	}
	void Update ()
	{   
		FindPath();
		if (Path.Count > 0 && pathfindEnable)
		{
			MoveMethod();
		}

		if (Path.Count == 3)
		{
			movementMotor.enabled = true;
		   // AIcontroller.enabled = true;
			pathfindEnable = false;
		//enabled = false;
		}

		//if (movementMotor.enabled && Vector3.Distance(transform.position, testPlayer.transform.position) > 15.0f)
		//{
		//    pathfindEnable = true;
		//     movementMotor.enabled = false;
		//}
	}

	private void FindPath()
	{

        if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition && testPlayerTransform!=null)
			{
				
				FindPath(transform.position, testPlayerTransform.position);
   
		}

	}

	private void MoveMethod()
	{
		if (Path.Count > 0)
		{
			Vector3 direction = (Path[0] - transform.position).normalized;

			transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * 14F);
			
            //if close to the waypoint on path, remove waypoint
            if (transform.position.x < Path[0].x + 0.4F && transform.position.x > Path[0].x - 0.4F && transform.position.z > Path[0].z - 0.4F && transform.position.z < Path[0].z + 0.4F)
			{
				Path.RemoveAt(0);
			   
			}

			RaycastHit[] hit = Physics.RaycastAll(transform.position + (Vector3.up * 20F), Vector3.down, 100);
			float maxY = -Mathf.Infinity;
			foreach (RaycastHit h in hit)
			{
				if (h.transform.tag == "Untagged")
				{
					if (maxY < h.point.y)
					{
						maxY = h.point.y;
					}
				}
			}
			transform.position = new Vector3(transform.position.x, maxY + 1F, transform.position.z);

		}
	}

    //void OnGUI()
    //{
    //    GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "", bgStyle);
    //}
}
