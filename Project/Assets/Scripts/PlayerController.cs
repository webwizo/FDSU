using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator m_Animator;

    private bool m_IsAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
        Attach();
    }

    private void Attach()
    {
        m_IsAttacking |= Input.GetKeyDown(KeyCode.Space);

        if (Input.GetKeyUp(KeyCode.Space) && m_IsAttacking)
            m_IsAttacking = false;

        m_Animator.SetBool("Attack", m_IsAttacking);


    }

    private void Move()
    {
        float xAxis = Input.GetAxis("Horizontal");

        if (xAxis > 0)
            transform.rotation = Quaternion.Euler(0, 90, 0);

        if (xAxis < 0)
            transform.rotation = Quaternion.Euler(0, -90, 0);

        m_Animator.SetFloat("Walking", Mathf.Abs(xAxis));
    }
}
