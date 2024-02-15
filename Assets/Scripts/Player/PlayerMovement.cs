using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Vector2 m_lastVelocity;
    public Vector2 m_direction;
    public float m_acceleration = 0.1f;
    public float m_movementVelocityCap = 20.0f;
 
    void Start()
    {
       m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        m_lastVelocity = m_rb.velocity;
        LookAtMouse();

    }

    void OnCollisionEnter2D(Collision2D Collision)
    {
        m_direction = Vector2.Reflect(m_lastVelocity.normalized, Collision.contacts[0].normal);
        //halfs the velocity and reflects at an angle of somekind
        if (Collision.gameObject.CompareTag("Bouncy"))
        {
            m_rb.velocity = (m_direction * Mathf.Max(m_lastVelocity.magnitude, 0f)) * 1.5f;
        }
        else
        {

            m_rb.velocity = m_direction * Mathf.Max(m_lastVelocity.magnitude, 0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (Collision.gameObject.CompareTag("Slower"))
        {
            m_rb.velocity /= 2;
        }    
        if (Collision.gameObject.CompareTag("Slower"))
        {
            Debug.Log("win");
        }
    }
    public void ProcessMoveX(float input)
    {
        if( m_rb.velocity.x < m_movementVelocityCap &&  m_rb.velocity.x > -m_movementVelocityCap)
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x+(input * m_acceleration), m_rb.velocity.y);
        }
    }
    public void ProcessMoveY(float input)
    {
        if( m_rb.velocity.y < m_movementVelocityCap  &&  m_rb.velocity.y > -m_movementVelocityCap)
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, m_rb.velocity.y+(input * m_acceleration));
        }
    }
    public void LookAtMouse()
    {
        Vector2 i_mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = i_mousePos - new Vector2(transform.position.x, transform.position.y);
    }
}
