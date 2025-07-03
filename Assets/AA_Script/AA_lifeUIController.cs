using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AA_lifeUIController : MonoBehaviour
{
    [Header("Refer�ncias")]
    public GameObject heartFullPrefab;
    public GameObject heartEmptyPrefab;
    public Transform heartParent;
    public AA_healthSystem healthSystem;

    private List<GameObject> heartObjects = new List<GameObject>();

    void Start()
    {
        AtualizarCoroes();
    }

    void Update()
    {
        
    }

    public void AtualizarCoroes()
    {
        int maxVidas = healthSystem.vida;
        int vidasAtuais = healthSystem.vida;

        // Se j� tiver cora��es instanciados, destr�i todos para recriar corretamente
        foreach (GameObject heart in heartObjects)
        {
            Destroy(heart);
        }
        heartObjects.Clear();

        // Instancia cora��es conforme vidas
        for (int i = 0; i < maxVidas; i++)
        {
            GameObject heartPrefab = (i < vidasAtuais) ? heartFullPrefab : heartEmptyPrefab;
            GameObject heart = Instantiate(heartPrefab, heartParent);
            heartObjects.Add(heart);
        }
    }
}