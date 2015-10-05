using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{

    public int damage;
    public float speed;
   
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    //void OnCollisionEnter(Collision other)
    //{
       
    //    if (other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerProjectile" || other.gameObject.tag == "PlayerProjectileHoming")
    //    {   if (explosion != null)
    //        Instantiate(explosion, transform.position, transform.rotation);

    //        Destroy(transform.gameObject);
    //    }

    //}
}
