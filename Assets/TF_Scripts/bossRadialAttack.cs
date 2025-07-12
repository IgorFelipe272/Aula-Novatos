using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class bossRadialAttack : MonoBehaviour {

    public int damage = 1;
    public float chargeTime = 0.5f;
    private float chargeTimer = 0f;
    public float attackDuration = 1f;
    private float attackTimer = 0f;
    private bool isCharging = true;
    private bool isAttacking = false;
    private SpriteRenderer spriteRenderer;
    private bool blinkState = false;
    public float blinkInterval = 0.1f;
    private float blinkTimer = 0f;
    private HashSet<Collider2D> damagedColliders = new HashSet<Collider2D>();
    private HashSet<Collider2D> collidersInside = new HashSet<Collider2D>();

    void Start()
    {
        chargeTimer = 0f;
        attackTimer = 0f;
        isCharging = true;
        isAttacking = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color c = spriteRenderer.color;
            c.a = 0f;
            spriteRenderer.color = c;
        }
    }

    void Update()
    {
        if (isCharging)
        {
            chargeTimer += Time.deltaTime;
            if (spriteRenderer != null)
            {
                float alpha = Mathf.Clamp01(chargeTimer / chargeTime);
                Color c = spriteRenderer.color;
                c.a = alpha;
                spriteRenderer.color = c;
            }
            if (chargeTimer >= chargeTime)
            {
                isCharging = false;
                isAttacking = true;
                attackTimer = 0f;
                blinkTimer = 0f;
                blinkState = false;
                // Damage all colliders currently inside
                foreach (var other in collidersInside)
                {
                    if (!damagedColliders.Contains(other))
                    {
                        var player = other.GetComponent<TF_healthSys>();
                        if (player != null)
                        {
                            player.takeDamage(damage, transform.position, other);
                            damagedColliders.Add(other);
                        }
                    }
                }
            }
        }
        else if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            blinkTimer += Time.deltaTime;
            if (spriteRenderer != null)
            {
                if (blinkTimer >= blinkInterval)
                {
                    blinkState = !blinkState;
                    blinkTimer = 0f;
                }
                Color c = spriteRenderer.color;
                c.a = blinkState ? 1f : 0f;
                spriteRenderer.color = c;
            }
            if (attackTimer >= attackDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collidersInside.Add(other);
        if (isAttacking && !damagedColliders.Contains(other))
        {
            var player = other.GetComponent<TF_healthSys>();
            if (player != null)
            {
                player.takeDamage(damage, transform.position, other);
                damagedColliders.Add(other);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        collidersInside.Remove(other);
    }

}