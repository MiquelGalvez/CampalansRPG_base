using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAi : MonoBehaviour
{
    public float speed = 3f;
    public float checkRadius;

    public LayerMask whatIsPlayer;

    public bool shouldRotate;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    public Vector3 dir;

    //Aquesta booleana determina si hem de continuar seguint al juagdor
    public bool isInChaserange;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("walking", isInChaserange);

        isInChaserange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg;

        dir.Normalize();
        movement = dir;
        if (shouldRotate)
        {
            anim.SetFloat("x", dir.x);
            anim.SetFloat("y", dir.y);
        }

    }

    private void FixedUpdate()
    {
        if (isInChaserange)
        {
            MoveCharcater(movement);
        }
    }

    private void MoveCharcater(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }
}
