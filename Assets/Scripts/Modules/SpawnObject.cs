using UnityEngine;
using System.Collections;

public class SpawnObject : MonoBehaviour {

public GameObject objectToSpawn;
public SignalSender onDestroyedSignals;

private GameObject spawned;

// Keep disabled from the beginning


    void Start()
    {
        enabled = false;

    }

void  OnSignal (){
	spawned = Spawner.Spawn (objectToSpawn, transform.position, transform.rotation);
	if (onDestroyedSignals.receivers.Length > 0)
		enabled = true;
}


void  Update (){
	if (spawned == null || spawned.activeInHierarchy == false)
	{
		onDestroyedSignals.SendSignals (this);
		enabled = false;
	}
}

}
