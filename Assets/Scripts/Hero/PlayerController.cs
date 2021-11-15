using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public float m_walkSpeed;
    public float m_runSpeed;
    public float m_gravity;
    public float m_jumpHeight;
    public TEAM m_team;

    public int m_health;
    public int m_maxHealth;

    public WeaponAbstract[] m_weapons = new WeaponAbstract[2];


    public float m_currentSpeed;

    public CharacterController m_chController;

    private Vector3 m_sideVelocity;
    private Vector3 m_forwardVelocity;
    private Vector3 m_movementVelocity;
    private Vector3 m_verticalVelocity;
    private bool m_isGrounded = false;

    private float m_directionY;

    public Rechargables m_rechargables;

    public void Start()
    {
        m_verticalVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        m_chController = GetComponent<CharacterController>();
        m_currentSpeed = m_walkSpeed;
    }

    private void Update()
    {
        Movement();
        m_chController.Move(m_movementVelocity * m_currentSpeed * Time.deltaTime);
        Jump();
    }

    public void Jump()
    {
        if (m_chController.isGrounded && Input.GetKey(Keybindings.m_jumpKey))
        {
            m_directionY = m_jumpHeight;
        }
        if (!m_chController.isGrounded)
            m_directionY -= m_gravity * Time.deltaTime;

        m_verticalVelocity.y = m_directionY;
        m_chController.SimpleMove(m_verticalVelocity);
    }

    public void Movement()
    {
        if (Input.GetKey(Keybindings.m_forwardKey))
            m_forwardVelocity = Vector3.forward;
        else if (Input.GetKey(Keybindings.m_backwardKey))
            m_forwardVelocity = Vector3.back;
        else
            m_forwardVelocity = new Vector3(0.0f, 0.0f, 0.0f);

        if (Input.GetKey(Keybindings.m_rightKey))
            m_sideVelocity = Vector3.right;
        else if (Input.GetKey(Keybindings.m_leftKey))
            m_sideVelocity = Vector3.left;
        else
            m_sideVelocity = new Vector3(0.0f, 0.0f, 0.0f);

        m_movementVelocity = m_forwardVelocity + m_sideVelocity;

        ToggleRun(Input.GetKey(Keybindings.m_sprint));
        
        m_movementVelocity.Normalize();

    }
    private void ToggleRun(bool qrun)
    {
        m_movementVelocity.y = 0.0f;
        if (qrun)
            m_currentSpeed = m_runSpeed;
        else
            m_currentSpeed = m_walkSpeed;
    }





}
