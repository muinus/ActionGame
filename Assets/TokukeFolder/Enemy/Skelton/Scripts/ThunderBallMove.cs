using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBallMove : MonoBehaviour
{
    public GameObject player;
    Vector2 v;
    float speed = 3.0f;
    public Rigidbody2D rb;
    
    void Start()

    {
        player = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(thunderballattack());
    }

    IEnumerator thunderballattack()
    {
        yield return new WaitForSeconds(2.0f);

        /*float dx = player.transform.position.x - this.gameObject.transform.position.x;
         float dy = player.transform.position.y - this.gameObject.transform.position.y;
         float rad = Mathf.Atan2(dy, dx);
         float kakudo=rad * Mathf.Rad2Deg;

         v.x = Mathf.Cos(rad) * speed;
         v.y = Mathf.Sin(rad) * speed;
         */

        rb.velocity = transform.up.normalized * speed;
        Destroy(this.gameObject, 8.0f);
      /*  while (true)
        {
            yield return new WaitForSeconds(0.02f);
            rb.position = Vector3.forward * 0.1f;
        }*/

    }
}