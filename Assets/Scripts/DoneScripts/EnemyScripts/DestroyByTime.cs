using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
    public float destructionTime = 5;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, destructionTime);
    }
}
