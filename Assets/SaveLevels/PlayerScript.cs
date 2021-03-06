﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
  //  ManagerScript manager;

    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float speed = 5f;
    private float move;

    private bool facingRight;
    private bool isGrounded;

    void Start()
    {
     //   manager = FindObjectOfType<ManagerScript>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;

      //  transform.position = manager.playerPos;
    }


    void Update()
    {
        move = Input.GetAxis("Horizontal");
        transform.position += new Vector3(move, 0f, 0f) * speed;

        if (Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("IsMoving", true);
        }
        if (!Input.anyKey)
        {
            anim.SetBool("IsMoving", false);
        }

        // FLIP ANIMATION:
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        if (move < 0 && facingRight)
        {
            Flip();
        }

    }

    // Jump Movement
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetBool("IsJumping", true);
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("IsJumping", false);
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }

    //Facing rigth or left:
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 objScale = transform.localScale;
        objScale.x *= -1;
        transform.localScale = objScale;
    }
}