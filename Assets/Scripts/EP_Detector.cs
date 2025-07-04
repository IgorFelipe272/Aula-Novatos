using UnityEngine;
using UnityEngine.EventSystems;

public class EP_Detector : MonoBehaviour
{
    public EP_Patrol inimigo;

    [Header("Configuração do Detector")]
    public bool isFront;
    public bool isFoot;

    [Header("Layer que o detector considera como chão/parede")]
    public LayerMask groundLayer;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (isFoot && IsInLayerMask(other.gameObject.layer, groundLayer))
        {
            inimigo.Flip();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
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
