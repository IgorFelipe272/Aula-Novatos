using UnityEngine;
using UnityEngine.EventSystems;

public class BF_Detector : MonoBehaviour
{
    public BF_Patrol inimigo;

    [Header("Configuração do Detector")]
    public bool isFront;    // Ativa o OnTriggerEnter2D
    public bool isFoot;     // Ativa o OnTriggerExit2D

    [Header("Layer que o detector considera chão/parede")]
    public LayerMask groundLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Se frente colidiu com um chão, mudar de direcao
        if (isFront && IsInLayerMask(other.gameObject.layer, groundLayer))
        {
            inimigo.Flip();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Se pé saiu do chao, inverter direcao
        if (isFoot && IsInLayerMask(other.gameObject.layer, groundLayer))
        {
            inimigo.Flip();
        }
    }

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return ((1 << layer) & layerMask) != 0;
    }
}