using Photon.Pun;
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
        if (GetComponent<PhotonView>().IsMine)
        {

            rig = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            Camera.main.transform.SetParent(transform);
            Camera.main.transform.position = transform.position + (Vector3.up) + transform.forward * -10; 
        }
    }

   
    void Update()
    {
        // Le damos velocidad pero también en el eje y porque sino se quedaría parado.

        if (GetComponent<PhotonView>().IsMine)
        {
            rig.velocity = (transform.right * speed * Input.GetAxis("Horizontal"))
                           + (transform.up * rig.velocity.y);

            if (rig.velocity.x > 0.1f && GetComponent<SpriteRenderer>().flipX) // Cambiamos la imagen de movimiento
                GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, false);
            else if (rig.velocity.x < 0.1f && GetComponent<SpriteRenderer>().flipX)
                GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, true);

            // Añadimos el salto
            if (Input.GetButtonDown("Jump"))
            {
                rig.AddForce(transform.up * jumpForce);
            }

            // Añadimos la animación.
            animator.SetFloat("VelocityX", Mathf.Abs(rig.velocity.x));
            animator.SetFloat("VelocityY", rig.velocity.y);
        }
    }

    [PunRPC]
    public void RotateSprite(bool rotate)
    {
        if (rig.velocity.x > 0.1f && GetComponent<SpriteRenderer>().flipX) // Cambiamos la imagen de movimiento
            GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, false);
        else if (rig.velocity.x < 0.1f && GetComponent<SpriteRenderer>().flipX)
            GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, true);
    }

}
