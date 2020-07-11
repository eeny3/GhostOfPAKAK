using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class RangeSpell : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    int dmg = 5;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.magnitude > 50.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SamuraiController controller = other.GetComponent<SamuraiController>();
        if (controller != null)
        {
            controller.TakeDamage(dmg);
        }
        Destroy(gameObject);
    }
}
