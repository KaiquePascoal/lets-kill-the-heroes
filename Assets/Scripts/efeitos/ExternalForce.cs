using UnityEngine;

public class ExternalForce : MonoBehaviour
{
    [Header("Forças Externas")]
    public Vector3 southWind;
    public Vector3 northWind;
    public Vector3 jetpack;
    public Vector3 totalForce;
    public Vector3 acceleration;
    public float mass;

    private void Update()
    {
        totalForce = southWind + northWind + jetpack;
        acceleration = totalForce / mass; 
    }    
}
