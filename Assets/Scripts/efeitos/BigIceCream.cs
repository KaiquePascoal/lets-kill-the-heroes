using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigIceCream : MonoBehaviour
{
    public NewBehaviouScript2 outro; //ABACAXI
    public Vector3 aceleration;
    public Vector3 acelerationReacao;

    public Vector3 velocidadeDesse;
    public Vector3 velocidadeOutro;
    public Vector3 velocity;
    public Vector3 velocityOutro;
    public Vector3 velocidadeGeral;
    public Vector3 mola, molaReacao;
    public Vector3 distanciaOutro, distanciaDesse;
    public float distance;
    public float pontoEquilibrio;
    public float raioDesse, raioOutro;

    public float k, massDesse, massOutro;

    private bool isColliding = false;

    private void Start()
    {
        raioDesse = transform.localScale.x / 2f;
        raioOutro = outro.transform.localScale.x / 2f;
    }

    private void FixedUpdate()
    {
        distanciaOutro = outro.transform.position;
        distanciaDesse = transform.position;
        distance = Vector3.Distance(distanciaOutro, distanciaDesse);

        if (distance <= (raioDesse + raioOutro))
        {
            if (!isColliding)
            {
                isColliding = true;
                velocidadeOutro = ((massOutro - massDesse) / (massDesse + massOutro)) * velocityOutro;
                velocidadeDesse = (massOutro * 2 / (massDesse + massOutro)) * velocityOutro;
                velocityOutro = velocidadeOutro;
                velocity = velocidadeDesse;

                Debug.Log("Objetos colidindo!"); 
            }
        }

        //mola = new Vector3(-k * (distance - pontoEquilibrio), 0, 0);
        //molaReacao = -mola;

        //aceleration = mola / massOutro;
        //acelerationReacao = molaReacao / outro.mass;

        UpdateVelocity();

        transform.position += velocity * Time.deltaTime + (aceleration * (Time.deltaTime * Time.deltaTime)) / 2f;
        outro.transform.position += velocityOutro * Time.deltaTime + (acelerationReacao * (Time.deltaTime * Time.deltaTime)) / 2f;
    }

    private void UpdateVelocity()
    {
        velocity += aceleration * Time.deltaTime;
        velocityOutro += acelerationReacao * Time.deltaTime;
    }
}
