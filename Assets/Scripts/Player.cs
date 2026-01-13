using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Animator animator;
    public Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Le damos velocidad pero también en el eje y porque sino se quedaría parado.
        rig.velocity = (transform.right * speed * Input.GetAxis("Horizontal")) + (transform.up * rig.velocity.y);

        if (rig.velocity.x > 0.1f) GetComponent<SpriteRenderer>().flipX = false; // Cambiamos la imagen de movimiento
        else if (rig.velocity.x < -0.1f) GetComponent<SpriteRenderer>().flipX = true;

        if (Input.GetButtonDown("Jump")  )
        {
            rig.AddForce(transform.up * jumpForce);
        }

        animator.SetFloat("VelocityX" ,Mathf.Abs(rig.velocity.x));
        animator.SetFloat("VelocityY", rig.velocity.y);

    }
}
