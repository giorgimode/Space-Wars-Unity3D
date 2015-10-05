using UnityEngine;
using System.Collections;

public class Lines : MonoBehaviour 
{
    private LineRenderer lineRenderer;
    private SpiderAttackPathfinder playerScript;

	void Start () 
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        playerScript = GameObject.Find("EnemySpider4/AIAttack").GetComponent<SpiderAttackPathfinder>();
	}
	
	void Update () 
    {
        DrawPath();
	}

    private void DrawPath()
    {
        if (playerScript.Path.Count > 0)
        {
            Debug.Log("draw");
            lineRenderer.SetVertexCount(playerScript.Path.Count);

            for (int i = 0; i < playerScript.Path.Count; i++)
            {
                lineRenderer.SetPosition(i, playerScript.Path[i] + Vector3.up);
             //   Debug.DrawLine(playerScript.Path[i+1], playerScript.Path[i] + Vector3.up, Color.red);
            }
        }
        else
        {
            lineRenderer.SetVertexCount(0);
        }
    }
}
