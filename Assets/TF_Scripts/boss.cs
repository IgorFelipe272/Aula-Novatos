using UnityEngine;

enum BossState
{
    follow,
    approach,
    afterAttackCD,
    attack,
};
public class boss : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public GameObject radialAttackPrefab;
    public GameObject bulletPrefab; 

    [Header("Movimento")]
    public float velocity = 7f;
    public float followYOffset = 3f; 
    public float followSpeed = 1f;
    public float approachSpeed = 2f;

    [Header("Ataque")]
    public float bulletCooldown = 0.5f;
    private float bulletSpeed = 10f;

    [Header("Efeitos Visuais")]
    public float floatingSpeed = 0.1f;
    public float floatingAmplitude = 0.1f;

    private BossState currentState = BossState.follow;
    private float afterAttackCDTime = 2f;
    private float nextAttackTime = 0f;
    private float floatingTimer = 0f;
    private Vector2 smoothDampVelocity = Vector2.zero;
    private Vector2 prefloatPos = Vector2.zero;
    private float bulletTimer = 0f;
    private float approachTimer = 0f;
    private float approachSpeedBase;
    private float approachSpeedMax = 16f; // doubled max speed
    private float approachSpeedIncreaseRate = 2f; // doubled increase rate

    void Start()
    {

        nextAttackTime = Time.time + Random.Range(5f, 10f);
        approachSpeedBase = approachSpeed;
    }

    void FixedUpdate()
    {
        Movement();
        SpriteFloating();
    }

    void Update(){
        ProcessBossAI();
        ProcessShooting();
    }

    private void Movement()
    {
        switch (currentState)
        {
            case BossState.follow:
                if (player == null) return;
                Vector2 targetPos = new Vector2(player.position.x, player.position.y + followYOffset);
                Vector2 currentPos = prefloatPos;
                float smoothTime = 1f / followSpeed;
                prefloatPos = Vector2.SmoothDamp(currentPos, targetPos, ref smoothDampVelocity, smoothTime);
                break;

            case BossState.approach:
                if (player == null) return;
                approachTimer += Time.deltaTime;
                float dynamicApproachSpeed = Mathf.Min(approachSpeedBase + approachTimer * approachSpeedIncreaseRate, approachSpeedMax);
                targetPos = player.position;
                currentPos = prefloatPos;
                smoothTime = 1f / dynamicApproachSpeed;
                prefloatPos = Vector2.SmoothDamp(currentPos, targetPos, ref smoothDampVelocity, smoothTime);
                break;
        }
    }

    private void ProcessBossAI()
    {

        switch (currentState)
        {
            case BossState.follow:
                if(nextAttackTime < Time.time)
                {
                    currentState = BossState.approach;
                }
                break;
            case BossState.approach:
                if (player == null) return;
                Vector2 targetPos = player.position;
                Vector2 currentPos = prefloatPos;
                float distanceToPlayer = Vector2.Distance(currentPos, targetPos);
                if (distanceToPlayer < 0.5f)
                {
                    currentState = BossState.attack;
                    approachTimer = 0f; // Reset timer when leaving approach state
                }
                break;

            case BossState.afterAttackCD:

                afterAttackCDTime -= Time.deltaTime;
                if (afterAttackCDTime <= 0f)
                {
                    currentState = BossState.follow;
                    nextAttackTime = Time.time + Random.Range(5f, 10f);
                }

                break;
            case BossState.attack:

                GameObject radialAttack = Instantiate(radialAttackPrefab, transform.position, Quaternion.identity);
                afterAttackCDTime = 2f;
                currentState = BossState.afterAttackCD;
                break;

        }

    }

    private void ProcessShooting()
    {
        if (currentState != BossState.follow) return;
        if(bulletTimer > 0f)
        {
            bulletTimer -= Time.deltaTime;
        } else {
            Shoot();
            bulletTimer = bulletCooldown;
        }
    }

    private void Shoot()
    {
        GameObject tiro = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = tiro.GetComponent<Rigidbody2D>();
        if (rb != null && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.linearVelocity = direction * bulletSpeed;
            tiro.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            if (direction.x < 0)
            {
                Vector3 scale = tiro.transform.localScale;
                scale.y = -Mathf.Abs(scale.y);
                tiro.transform.localScale = scale;
            }
            else
            {
                Vector3 scale = tiro.transform.localScale;
                scale.y = Mathf.Abs(scale.y);
                tiro.transform.localScale = scale;
            }
        }
        else
        {
            Debug.LogWarning("Rigidbody2D not found on the bullet prefab or player is null.");
        }
    }

    private void SpriteFloating()
    {

        floatingTimer += Time.deltaTime;
        float floatingHeight = Mathf.Sin(floatingTimer * floatingSpeed) * floatingAmplitude;
        transform.position = new Vector3(prefloatPos.x, prefloatPos.y + floatingHeight, transform.position.z);

    }
    
    
}
