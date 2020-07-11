using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GhostController : MonoBehaviour
{
    public static GhostController instance { get; private set; }

    Vector2 mousepos;
    Animator animator;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    public float speed = 3.0f;
    public GameObject projectilePrefab;
    public GameObject eSpellPrefab;
    public GameObject cSpellPrefab;
    public ParticleSystem rageEffect;
    string curSpell;
    int maxHP = 100;
    int currentHP;
    AudioSource audioSource;

    bool canQ = true;
    float qTimer = 2.0f;
    float eTimer = 20.0f;
    float cTimer = 4.0f;
    bool canE = true;
    bool canC = true;

    public Text score;
    public int souls = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        audioSource = GetComponent<AudioSource>();

        cdScript.instance.SetValue(0, 1);
        cdScript.instance.SetValue(0, 2);
        cdScript.instance.SetValue(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        animator.SetFloat("MouseX", mousepos.normalized.x);
        animator.SetFloat("MouseY", mousepos.normalized.y);

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetKeyDown(KeyCode.Q) && canQ)
        {
            animator.SetTrigger("Attack");
            //ChidoriSound();
            curSpell = "RangedSpell";
            //Launch("RangedSpell");
            canQ = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && canE)
        {
            animator.SetTrigger("Attack");
            curSpell = "ExpSpell";
            //Launch("ExpSpell");
            canE = false;
        }

        if (Input.GetKeyDown(KeyCode.C) && canC)
        {
            animator.SetTrigger("Attack");
            curSpell = "FrozSpell";
            //Launch("ExpSpell");
            canC = false;
        }

        if (!canQ)
        {
            cdScript.instance.SetValue(qTimer / 2.0f, 1);
            qTimer -= Time.deltaTime;
            if(qTimer <= 0)
            {
                canQ = true;
                qTimer = 2.0f;
            }
        }

        if (!canE)
        {
            cdScript.instance.SetValue(eTimer / 20.0f, 2);
            eTimer -= Time.deltaTime;
            if (eTimer <= 0)
            {
                canE = true;
                eTimer = 20.0f;
            }
        }
        if (!canC)
        {
            cdScript.instance.SetValue(cTimer / 4.0f, 3);
            cTimer -= Time.deltaTime;
            if (cTimer <= 0)
            {
                canC = true;
                cTimer = 4.0f;
            }
        }

        score.text = " Souls: " + souls;
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        Vector2 move = new Vector2(horizontal, vertical);

        position += speed * move * Time.fixedDeltaTime;

        rigidbody2d.MovePosition(position);
    }

    void Launch()
    {
        if (curSpell == "RangedSpell")
        {
            ChidoriSound();
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 1.5f, Quaternion.identity);
            RangeSpell projectile = projectileObject.GetComponent<RangeSpell>();
            projectile.Launch((mousepos - Vector2.up * 1.5f).normalized, 300);
        }
        else if(curSpell == "ExpSpell")
        {
            Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject eSpellObject = Instantiate(eSpellPrefab, rayCast.GetPoint(10), Quaternion.identity);
        }
        else if (curSpell == "FrozSpell")
        {
            Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject cSpellObject = Instantiate(cSpellPrefab, rayCast.GetPoint(10), Quaternion.identity);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        UIHealthBar.instance.SetValue(currentHP / (float)maxHP);
        if (currentHP <= 0)
        {
            animator.SetTrigger("Dead");
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

    void Die()
    {
        Destroy(gameObject);
        PlayerPrefs.SetInt("souls", souls);
        SceneManager.LoadScene("GameOver");
    }

    public void ChidoriSound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }

}
