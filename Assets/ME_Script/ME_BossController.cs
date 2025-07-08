using UnityEngine;
using System.Collections;

public class ME_BossController : MonoBehaviour
{
    public GameObject projetilPrefab;

    [Header("Pontos de disparo")]
    public Transform pontoBaixo;
    public Transform pontoMedio;

    [Header("Configurações")]
    public float intervaloAtaque = 2f;
    public float intervaloAtaqueRapido = 1f;
    public int vidaCritica = 10;

    private bool jogadorDentro = false;
    private float cronometroAtaque;
    private ME_HealthSystem health;

    void Start()
    {
        cronometroAtaque = intervaloAtaque;
        health = GetComponent<ME_HealthSystem>();
    }

    void Update()
    {
        if (!jogadorDentro) return;

        cronometroAtaque -= Time.deltaTime;

        if (cronometroAtaque <= 0)
        {
            StartCoroutine(ExecutarAtaque());
            cronometroAtaque = (health.vida > vidaCritica) ? intervaloAtaque : intervaloAtaqueRapido;
        }
    }

    IEnumerator ExecutarAtaque()
    {
        // Tiros baixos (3 tiros)
        for (int i = 0; i < 3; i++)
        {
            Instantiate(projetilPrefab, pontoBaixo.position, Quaternion.identity);
            yield return new WaitForSeconds(0.7f);
        }

        yield return new WaitForSeconds(1f);

        // Tiros médios (2 tiros)
        for (int i = 0; i < 2; i++)
        {
            Instantiate(projetilPrefab, pontoMedio.position, Quaternion.identity);
            yield return new WaitForSeconds(0.6f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorDentro = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorDentro = false;
        }
    }
}
