using UnityEngine;
using UnityEngine.EventSystems;

public class JB_Detector : MonoBehaviour
{

    public JB_Patrol inimigo;

    [Header("Configuração do Detector")]
    public bool isFront; //Ativa o OnTriggerEnter2D
    public bool isFoot; //Ativa o OnTriggerExit2D

    [Header("Layer que o detector considera como chão/parede")]
    public LayerMask groundLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isFront && isInLayerMask(other.gameObject.layer, groundLayer)){
            inimigo.Flip();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(isFoot && isInLayerMask(other.gameObject.layer, groundLayer)){
            inimigo.Flip();
        }
    }

    public bool isInLayerMask(int layer, LayerMask layerMask){
        return ((1 << layer) & layerMask) != 0;
    }
}
