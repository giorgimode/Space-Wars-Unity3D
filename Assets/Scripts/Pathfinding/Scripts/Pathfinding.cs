using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Pathfinding : MonoBehaviour 
{
    public List<Vector3> Path = new List<Vector3>();
  
    [HideInInspector]


    public void FindPath(Vector3 startPosition, Vector3 endPosition)
    {
      
            Pathfinder.Instance.InsertInQueue(startPosition, endPosition, SetList);
              
    }

   

    
    public void Move()
    {
        if (Path.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, Path[0], Time.deltaTime * 30F);
            if (Vector3.Distance(transform.position, Path[0]) < 0.4F)
            {
                Path.RemoveAt(0);
            }
        }
    }

    protected virtual void SetList(List<Vector3> path)
    {
        if (path == null)
        {
            return;
        }
		
		  Path.Clear();
	        Path = path;
            if (Path.Count > 0)
            {
                //Path[0] = new Vector3(Path[0].x, Path[0].y - 1, Path[0].z);
                //Path[Path.Count - 1] = new Vector3(Path[Path.Count - 1].x, Path[Path.Count - 1].y - 1, Path[Path.Count - 1].z);
            }
		
     
    }
}
	
