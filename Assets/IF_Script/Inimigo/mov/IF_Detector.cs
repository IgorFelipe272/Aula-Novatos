using UnityEngine;
using UnityEngine.EventSystems;

public class IF_Detector : MonoBehaviour
{
    public IF_Patrol inimigo;

    [Header("Configuração do Detector")]
    public bool isFront;  // Ativa o OnTriggerEnter2D
    public bool isFoot;   // Ativa o OnTriggerExit2D

    [Header("Layer que o detector considera como chão/parede")]
    public LayerMask groundLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isFront && IsInLayerMask(other.gameObject.layer, groundLayer))
        {
            inimigo.Flip();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isFoot && IsInLayerMask(other.gameObject.layer, groundLayer))
        {
            inimigo.Flip();
        }
    }

    // Função utilitária para verificar se o objeto está na LayerMask
    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return ((1 << layer) & layerMask) != 0;
    }


}
