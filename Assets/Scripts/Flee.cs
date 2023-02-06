using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    public Transform target;
    public float acceleration = 1.0f;
    public float maxVelocity = 10.0f;
    public float turnAcceleration = 0.2f;
    public float turnSpeed = 1.0f;
    public float maxTurnSpeed = 10.0f;
    public float stopDistance = 2.0f;
    public float movSpeed;


    public Vector3 direction = Vector3.zero;
    public Vector3 movement = Vector3.zero;
    public float angle = 0f;
    public Quaternion rotation = Quaternion.identity;
    public float freq = 0f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateFlee()
    {
        freq += Time.deltaTime;
        if (freq < 0.5) return;

        if (Vector3.Distance(target.transform.position, transform.position) < stopDistance)
            return;
        DoFlee();   // calls to this function should be reduced

        turnSpeed += turnAcceleration * Time.deltaTime;
        turnSpeed = Mathf.Min(turnSpeed, maxTurnSpeed);
        movSpeed += acceleration * Time.deltaTime;
        movSpeed = Mathf.Min(movSpeed, maxVelocity);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
        transform.position += transform.forward.normalized * movSpeed * Time.deltaTime;
    }

    void DoFlee()
    {
        direction = transform.position - target.transform.position;
        direction.y = 0.0f;

        movement = direction.normalized * maxVelocity;
        angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        rotation = Quaternion.AngleAxis(angle, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
        transform.position += transform.forward.normalized * maxVelocity * Time.deltaTime;
    }
}
