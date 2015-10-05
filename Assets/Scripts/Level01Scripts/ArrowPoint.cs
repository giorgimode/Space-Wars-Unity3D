using UnityEngine;
using System.Collections;

public class ArrowPoint : MonoBehaviour {

    public GameObject target;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.LookAt(target.transform);
	}

    void OnGUI()
    {
        float d = Vector3.Distance(gameObject.transform.position, target.transform.position);
        
        GUI.Box(new Rect(Screen.width/2 - 50, 100, 100, 25), d.ToString("N") + " M");
    }
}
