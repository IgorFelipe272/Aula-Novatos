using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BF_LifeUIController : MonoBehaviour
{
    [Header("Refer?ncias")]
    public GameObject heartFullPrefab;
    public GameObject heartEmptyPrefab;
    public Transform heartParent;
    public BF_HealthSystem healthSystem;

    private List<GameObject> heartObjects = new List<GameObject>();

    void Start()
    {
        AtualizarCoracoes();
    }

    void Update()
    {
        
    }

    public void AtualizarCoracoes()
    {
        int maxVidas = healthSystem.vida;
        int vidasAtuais = healthSystem.vida;

        // Se j? tiver cora??es instanciados, destr?i todos para recriar corretamente
        foreach (GameObject heart in heartObjects)
        {
            Destroy(heart);
        }
        heartObjects.Clear();

        // Instancia cora??es conforme vidas
        for (int i = 0; i < maxVidas; i++)
        {
            GameObject heartPrefab = (i < vidasAtuais) ? heartFullPrefab : heartEmptyPrefab;
            GameObject heart = Instantiate(heartPrefab, heartParent);
            heartObjects.Add(heart);
        }
    }
}