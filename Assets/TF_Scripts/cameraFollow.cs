using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player;
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    void LateUpdate()
    {
        if (player == null) return;

        float x = Mathf.Clamp(player.position.x, minX, maxX);
        float y = Mathf.Clamp(player.position.y, minY, maxY);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}

