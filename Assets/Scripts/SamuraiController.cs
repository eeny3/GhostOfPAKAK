using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2d;
    Vector2 ghostpos;
    public GameObject ghost;
    float speed;
    float asTimer = -1.0f;
    float frzTimer = 4.0f;
    public float attackRange = 1.6f;
    public LayerMask targetLayers;
    public Transform attackPoint;
    int maxHP = 20;
    int currentHP;
    public bool dead = false;
    public GameObject bloodPrefab;
    AudioSource audioSource;
    public ParticleSystem frozenEffect;
    public bool frozen;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        speed = Random.Range(1.5f, 5.0f);
        currentHP = maxHP;
        frozen = false;
        frozenEffect.Pause();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (ghost)
            {
                ghostpos = ghost.GetComponent<Rigidbody2D>().position - rigidbody2d.position;
            }
            else
                ghostpos = new Vector2(0, 0);

            animator.SetFloat("GhostX", ghostpos.normalized.x);
            animator.SetFloat("GhostY", ghostpos.normalized.y);


            if (asTimer < 0)
            {
                if (ghostpos.magnitude < 2)
                {
                    animator.SetTrigger("Attack");

                    Attack();
                    asTimer = 2.0f;
                }
            }
            asTimer -= Time.deltaTime;
        }

        if (frozen)
        {
            frozenEffect.Play();
            frzTimer -= Time.deltaTime;
            if (frzTimer < 0)
            {
                frozen = false;
                frozenEffect.Pause();
                frozenEffect.Clear();
                frzTimer = 4.0f;
            }
        }
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            Vector2 position = rigidbody2d.position;
            Vector2 move = new Vector2(ghostpos.x, ghostpos.y);

            if (frozen)
                position += 0.3f * move.normalized * Time.fixedDeltaTime;
            else
                position += speed * move.normalized * Time.fixedDeltaTime;

            rigidbody2d.MovePosition(position);
        }
    }

    void Attack()
    {
        Collider2D[] ghosts = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, targetLayers);
        if (ghosts.Length > 0)
        {
            SwordSound();
            GameObject bloodObject = Instantiate(bloodPrefab, ghost.GetComponent<Rigidbody2D>().position + Vector2.up * 0.7f, Quaternion.identity);
            ghosts[0].GetComponent<GhostController>().TakeDamage(2);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            dead = true;
            //GhostController.instance.souls = GhostController.instance.souls + 1;
            animator.SetTrigger("Dead");
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

    public void Die()
    {
        ghost.GetComponent<GhostController>().souls = ghost.GetComponent<GhostController>().souls + 1;
        Destroy(gameObject);
    }

    public void SwordSound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
