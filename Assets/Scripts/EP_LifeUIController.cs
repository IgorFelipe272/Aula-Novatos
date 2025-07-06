using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EP_LifeUIController : MonoBehaviour
{
    [Header("Referências")]
    public GameObject heartFullPrefab;
    public GameObject heartEmptyPrefab;
    public Transform heartParent;
    public EP_HealthSystem healthSystem;

    private List<GameObject> heartObjects = new List<GameObject>();

    void Start()
    {
        AtualizarCoroes();
    }

    public void AtualizarCoroes()
    {
        int maxvidas = healthSystem.vidaMax;
        int vidasAtuais = healthSystem.vidaAtual;

        foreach(GameObject heart in heartObjects)
        {
            Destroy(heart);
        }
        heartObjects.Clear();

        for(int i = 0; i < maxvidas ; i++)
        {
            GameObject heartPrefab = (i < vidasAtuais) ? heartFullPrefab : heartEmptyPrefab;
            GameObject heart = Instantiate(heartPrefab, heartParent);
            heartObjects.Add(heart);
        }
    }
}
