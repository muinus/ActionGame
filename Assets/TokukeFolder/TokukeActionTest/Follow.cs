using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
    public Transform m_target = null;
    public float m_speed = 5;
    public float m_attenuation = 0.5f;
    private Vector3 m_velocity;

    private void Start()
    {
        
    }
    private void Update()
    {
        Vector3 lscale = gameObject.transform.localScale;
        m_velocity += (m_target.position - transform.position) * m_speed;
        m_velocity.y += 2.0f;
        if (m_target.localScale.x < 0) { m_velocity.x += 3.0f;  }
        else if (m_target.localScale.x > 0) { m_velocity.x -= 3.0f; }
       
        if (m_target.position.x >= transform.position.x) {
            lscale.x = -2;
            gameObject.transform.localScale=lscale;
        }
        else if (m_target.position.x < transform.position.x) {
            lscale.x = 2;
            gameObject.transform.localScale = lscale;
        }
        m_velocity *= m_attenuation;
        transform.position += m_velocity *= Time.deltaTime;
    }
}