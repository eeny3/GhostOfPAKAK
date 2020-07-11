using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozeSpell : MonoBehaviour
{
    float timer;
    public float lifeSpan = 5.0f;
    public LayerMask targetLayers;
    int dmg = 5;
    bool cast;
    // Start is called before the first frame update
    void Start()
    {
        cast = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (!cast)
        {
            Collider2D[] mobs = Physics2D.OverlapCircleAll(GetComponent<Transform>().position, 1.0f, targetLayers);
            foreach (Collider2D mob in mobs)
            {
                if (!mob.GetComponent<SamuraiController>().dead)
                {
                    mob.GetComponent<SamuraiController>().frozen = true;
                    mob.GetComponent<SamuraiController>().TakeDamage(dmg);
                }
            }
            cast = true;
        }

        if (timer > lifeSpan)
        {
            Destroy(gameObject);
        }
    }
}
