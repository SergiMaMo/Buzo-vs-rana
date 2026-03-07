 using Photon.Pun;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 200f;

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer sr;
    public int maxJumps = 2;
    private int jumpCount;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        if (GetComponent<PhotonView>().IsMine)
        {
           
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.position= transform.position+(Vector3.up)+transform.forward*-10;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GetComponent<PhotonView>().IsMine) return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            anim.SetBool("isGrounded", true);
            anim.SetInteger("jumpCount", 0);
        }
    }
    void Update()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("GameOver") &&
        (bool)PhotonNetwork.CurrentRoom.CustomProperties["GameOver"])
        {
            return;
        }
        if (GetComponent<PhotonView>().IsMine)
        {
            // Movimiento horizontal
            float moveX = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(moveX * speed, rig.velocity.y);

            if (moveX > 0.1f && sr.flipX)
                GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, false);
            else if (moveX < -0.1f && !sr.flipX)
                GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, true);
            // Salto
            if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
                rig.velocity = new Vector2(rig.velocity.x, 0); // limpia Y
                rig.AddForce(Vector2.up * jumpForce);

                jumpCount++;

                anim.SetBool("isGrounded", false);
                anim.SetInteger("jumpCount", jumpCount);
            }

        // PASAR VELOCIDAD AL ANIMATOR
        anim.SetFloat("velocityX", Mathf.Abs(rig.velocity.x));
        anim.SetFloat("velocityY", rig.velocity.y);
        }
    }
    [PunRPC]
    public void RotateSprite(bool rotate)
    {
        GetComponent<SpriteRenderer>().flipX = rotate;
    }

}
