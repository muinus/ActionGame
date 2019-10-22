using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealPotion_L : MonoBehaviour
{
    float life_time = 20.0f;//生存時間
    float time = 0f;

    int drec;

    void Start()
    {
        drec = Random.Range(-2, 2);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * drec, 1);
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
            other.transform.tag != "Player")
            return;


        if (other.transform.tag == "Player")
        {

            GameObject player = GameObject.Find("PlayerUI"); 
            Slider HPbar = player.GetComponentInChildren<Slider>();

            HPbar.value += HPbar.maxValue * 0.6f;//最大HPの6割を回復
            Destroy(gameObject);
        }

    }
}
