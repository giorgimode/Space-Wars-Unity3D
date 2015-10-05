using UnityEngine;
using System.Collections;
[RequireComponent (typeof (LineRenderer))]
public class Laserhit : MonoBehaviour {

	public float height = 3.2f;
	public float  speed = 2.0f;
	public float  timingOffset = 0.0f;
	public float  laserWidth = 12.0f;
	public float  damage = 1f;
	public GameObject hitEffect ;
	
	private Vector3 originalPosition;
	private RaycastHit hit ;
	private float lastHitTime = 0.0f;
	
	void Start ()
	{
		originalPosition = transform.position;
		GetComponent<LineRenderer>().SetPosition(1, Vector3.forward * laserWidth);
	}
	
	void Update ()
	{
		float offset = (1 + Mathf.Sin(Time.time * speed + timingOffset)) * height / 2f;
		transform.position = originalPosition +  (new Vector3(-offset, 0f, 0f));
		
		if (Time.time > lastHitTime + 0.25f && Physics.Raycast(transform.position, transform.forward, out hit, laserWidth))
		{
			if (hit.collider.tag == "Player" || hit.collider.tag == "Enemy")
			{
				Instantiate(hitEffect, hit.point, Quaternion.identity);
				hit.collider.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
				lastHitTime = Time.time;
			}
		}
	}


}
