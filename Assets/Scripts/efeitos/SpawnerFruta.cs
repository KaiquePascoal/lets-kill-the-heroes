using UnityEngine;

public class SpawnerFruta : MonoBehaviour
{
    public GameObject frutaPrefab;
    public float tempoFixo = 2.0f; // Tempo exato em segundos entre cada fruta

    void Start()
    {
        // Começa após 1 segundo e repete exatamente a cada 'tempoFixo'
        InvokeRepeating("CairFruta", 1.0f, tempoFixo);
    }

    void CairFruta()
    {
        if (frutaPrefab != null)
        {
            Instantiate(frutaPrefab, transform.position, Quaternion.identity);
        }
    }
}

