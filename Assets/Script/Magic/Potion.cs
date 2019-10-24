using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    float life_time = 5.0f;//生存時間
    float time = 0f;
    
    int drec;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        drec = System.Math.Sign(this.transform.position.x - player.transform.position.x);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(3 * drec, 3);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > life_time)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag != "Untagged" &&
            other.transform.tag != "Ground" &&
            other.transform.tag != "Wall" &&
            other.transform.tag != "Enemy")
            return;

        GetComponent<Animator>().SetBool("isBreak", true);

        if (other.transform.tag == "Enemy")
        {

            GameObject enemy = other.gameObject;
            Slider HPbar = enemy.GetComponentInChildren<Slider>();
            EnemyController2 EC = enemy.GetComponent<EnemyController2>();

            if (!EC.GetIsPotioned())
            {
                HPbar.maxValue *= 3.0f;
                HPbar.value *= HPbar.maxValue;
                EC.SetIsPotioned();
            }
        }


    }
    
    void Destroy()
    {
        Destroy(gameObject);
    }
}
