using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PhotonView PV;
    private Rigidbody2D rb;
    private float MoveHoriz = 0, MoveVert = 0;
    public float speed = 5, jump = 10, JumpRadius = 1;
    private bool isGrounded = true;
    public Transform GroundCheck;
    public LayerMask WhatIsGround;
    // Start is called before the first frame update

    void Start()
    {

        PV = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame


    void FixedUpdate()
    {
        if (PV.IsMine)
        {
            //Horizontal movement
            HorizontalMovement();

            //Vertical movement
            Jump();
        }


    }



    void HorizontalMovement()
    {

        MoveHoriz = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(MoveHoriz * speed, rb.velocity.y);
    }

    void Jump()
    {
        MoveVert = Input.GetAxis("Vertical");
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, JumpRadius, WhatIsGround);
        if (MoveVert > 0 && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jump);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, JumpRadius);
    }
}
