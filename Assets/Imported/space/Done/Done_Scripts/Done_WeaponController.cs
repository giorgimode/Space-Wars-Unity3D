using UnityEngine;
using System.Collections;

public class Done_WeaponController : MonoBehaviour
{  // public float Range;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;
    private Transform target;
    private float distance;

    void Start()
    {
        InvokeRepeating("Fire", delay, fireRate); //Invokes the method Fire in 'delay' time seconds, then repeatedly every fireRate seconds.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    void Fire()
    {
        if (target != null)
        {
            NPCController npc = gameObject.GetComponent<NPCController>();

            distance = Vector3.Distance(transform.position, target.position);

            if (distance <= npc.attackStartDist)
            {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                GetComponent<AudioSource>().Play();
            }
        }
    }
}