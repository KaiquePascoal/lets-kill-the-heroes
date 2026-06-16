using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviouScript2: MonoBehaviour
{
    public Vector3 acceleration;
    public Vector3 velocity;

    [Header("Forças Externas")]
    public Vector3 southWind;
    public Vector3 northWind;
    public Vector3 jetpack;
    public Vector3 totalForce;
    public float mass;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        totalForce = southWind + northWind + jetpack;
        acceleration = totalForce / mass;

        UpdateVelocity();

        transform.position += velocity * Time.deltaTime;
    }

    private void UpdateVelocity()
    {
        velocity += acceleration * Time.deltaTime;
    }
}
