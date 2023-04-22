using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWayPoint : MonoBehaviour
{
    public Transform[] allWayPoints;
    public float rotationSpeed = .5f, movementSpeed = 0.5f;
    public int currentTarget;

    void Start()
    {
        
    }

   
    void Update()
    {
        Movement();
        Rotate();
        ChangeTarget();
    }

    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, allWayPoints[currentTarget].position,movementSpeed*Time.deltaTime);
    }

    void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(allWayPoints[currentTarget].position-transform.position),rotationSpeed*Time.deltaTime);
    }

    void ChangeTarget()
    {
        if (transform.position==allWayPoints[currentTarget].position)
        {
            currentTarget++;
            currentTarget = currentTarget % allWayPoints.Length;
        }
    }
}
