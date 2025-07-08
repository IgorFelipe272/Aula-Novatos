using UnityEngine;

public class ME_Parallax : MonoBehaviour
{
    public Transform cameraTransform;          // Câmera a seguir
    public float speedParallax = 0.5f;         // Velocidade relativa ao movimento da câmera (0 = fixo, 1 = segue junto)

    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        lastCameraPosition = cameraTransform.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit * transform.localScale.x;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * speedParallax, deltaMovement.y * speedParallax, 0f);

        lastCameraPosition = cameraTransform.position;

        float distanceFromCamera = cameraTransform.position.x - transform.position.x;
        if (Mathf.Abs(distanceFromCamera) >= textureUnitSizeX)
        {
            float offset = (distanceFromCamera % textureUnitSizeX);
            transform.position = new Vector3(cameraTransform.position.x + offset, transform.position.y, transform.position.z);
        }
    }
}