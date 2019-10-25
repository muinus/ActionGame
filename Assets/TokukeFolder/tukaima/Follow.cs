using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
    public Transform m_target;
     float m_speed = 5;
     float m_attenuation = 0.5f;
    float p_speed;
    private Vector3 m_velocity;
    float CT = 20;
    PlayerController script;

    private void Start()
    {
        m_target = GameObject.Find("Player").transform;
        script = m_target.GetComponent<PlayerController>();
        script.SetMasicEffect(2f);
        //p_speed+=2;
        StartCoroutine("DestroyTime");
    }
    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(20);
        script.SetMasicEffect(1f);
        Destroy(this.gameObject);
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