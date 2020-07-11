using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpSpell : MonoBehaviour
{
    float timer;
    public float lifeSpan = 1.2f;
    public LayerMask targetLayers;
    public GameObject ashPrefab;
    //int dmg = 20;
    bool cast;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (!cast)
        {
            Collider2D[] mobs = Physics2D.OverlapCircleAll(GetComponent<Transform>().position, 1.2f, targetLayers);
            foreach (Collider2D mob in mobs)
            {
                Instantiate(ashPrefab, mob.GetComponent<Rigidbody2D>().position + Vector2.up * 0.9f, Quaternion.identity);
                mob.GetComponent<SamuraiController>().Die();
            }
            cast = true;
        }
        if (timer > lifeSpan)
        {
            Destroy(gameObject);
        }
    }
}
