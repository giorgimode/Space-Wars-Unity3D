using UnityEngine;
using System.Collections;
using System;

public class PatrolPoint : MonoBehaviour
{

    public Vector3 position;

    void Awake()
    {
        position = transform.position;
    }
}
