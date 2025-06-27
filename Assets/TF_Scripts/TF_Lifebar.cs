using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TF_Lifebar : MonoBehaviour
{
    [Header("Referências")]
    public GameObject heartFullPrefab;
    public GameObject heartEmptyPrefab;
    public Transform heartParent;
    public TF_healthSys healthSystem;
    private List<GameObject> heartObjects = new List<GameObject>();
    void Start()
    {
        AtualizarCores();
    }
    void Update()
    {
        
    }
    
    public void AtualizarCores()
    {

        int maxVida = healthSystem.vida;
        int vidaAtual = healthSystem.vida;

        foreach (GameObject heart in heartObjects)
        {
            Destroy(heart);
        }
        heartObjects.Clear();

        for(int i = 0; i < maxVida; i++)
        {
            GameObject heart = Instantiate(i < vidaAtual ? heartFullPrefab : heartEmptyPrefab, heartParent);
            heartObjects.Add(heart);
        }
    }
}
