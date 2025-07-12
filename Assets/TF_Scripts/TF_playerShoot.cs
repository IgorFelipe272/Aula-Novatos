using UnityEngine;

public class TF_playerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    private float shootCD = 0f;
    public float bulletSpeed = 10f;
    public float cooldown = 0.5f; 
    

    void Start()
    {

    }


    void Update()
    {
        if (shootCD > 0f)
        {
            shootCD -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && shootCD <= 0f)
        {
            shootCD = cooldown;
            Shoot();
        }
    }


    void Shoot()
    {
        GameObject tiro = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
       
        Rigidbody2D rb = tiro.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float direction = Mathf.Sign(transform.localScale.x);
            rb.linearVelocity = new Vector2(direction * bulletSpeed, 0f);

            Vector3 escala = tiro.transform.localScale;
            escala.x = direction;
            tiro.transform.localScale = escala;
        }
        else
        {
            Debug.LogWarning("Rigidbody2D not found on the bullet prefab.");
        }
    }
}
