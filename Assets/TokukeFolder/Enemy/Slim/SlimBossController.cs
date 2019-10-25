using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimBossController : MonoBehaviour
{

    Slider HPbar;//HPバーのオブジェクト
    GameObject player;
    Animator animator;

    private Rigidbody2D rb;

    bool isdamage;
    string state;
    public int movedir;
    int i;
    float distance;
    //ジャンプ用変数
    float time = 1.5f;
    float gravity = 9.8f;
    float x;
    float y=3.0f;
    float z;
    float jumpx;
    float jumpy;
    bool jumpJudge = false;
    public BoxCollider2D col;



    float fixposition;

    public GameObject BattleEvent;
    public GameObject slim;

    // Start is called before the first frame update
    void Start()
    {
        col.gameObject.SetActive(false);
        rb = this.GetComponent<Rigidbody2D>();
        HPbar = GetComponentInChildren<Slider>();
        player = GameObject.Find("Player");
        animator = transform.root.GetComponent<Animator>();
        isdamage = false;

        BattleEvent = GameObject.Find("BattleEventMaster");
    }

    // Update is called once per frame

    void Update()
    {
        ChangeState();          // ① 状態を変更する
        ChangeAnimation();      // ② 状態に応じてアニメーションを変更する
        //向き取得
        if (player.transform.position.x >= this.transform.position.x)
        {
            movedir = 1;
        }
        else if (player.transform.position.x < this.transform.position.x)
        {
            movedir = -1;
        }
    }

    void ChangeState()
    {
        //敵から自分への向き
        int drec = System.Math.Sign(this.transform.position.x - player.transform.position.x);
        this.transform.rotation = new Quaternion(0, 90.0f * drec + 90.0f, 0, 0);
        if (HPbar.value <= 0)
        {
            //state = "Die";
            animator.SetBool("isDead", true);
            Destroy(this.gameObject, 2.0f);
        }
        /*else if (HPbar.value <= 200)
        {
            state = "Wind";
        }*/
        else if (rb.velocity.y < 0)
        {
            state = "Fall";
        }
       
        else
        {
            state = "Default";
        }


    }

    void ChangeAnimation()
    {
        switch (state)
        {
            case "Die":
                animator.SetBool("isDead", true);
                animator.SetBool("isDamaged", false);
                
                break;
            case "Fall":
                animator.SetBool("isFall", true);
                break;
            /*case "Landing":
                Debug.Log("test");
                animator.SetBool("isFall", false);
                animator.SetBool("isLanding", true);
                break;*/
            case "wind":
                animator.SetBool("isWind", true);
                break;
            case "Default":
                animator.SetBool("isFall", false);
                break;


        }
    }
    //攻撃アニメーションを分裂→ジャンプ→ジャンプでループさせる
    void SlimJumpAttack()//ジャンプアタック
    {
        //飛翔時の方向固定
        x = Mathf.Abs(player.transform.position.x - this.transform.position.x)/2;
        //z = Mathf.Sqrt(x*x+y*y);
        jumpx = x / time;
        jumpy = (y + 0.5f * gravity * time * time)/time;
        
        Vector2 force = new Vector2(jumpx * movedir*80, jumpy*80);
        Debug.Log(force);
        rb.AddForce(force);
        col.gameObject.SetActive(true);
    }
    void SlimDivisionAttack()//分裂
    {
        for (i = 0; i <= 2; i++)
        {
            float horizontal = Random.Range(200, 400);
            float vartical = Random.Range(200, 400);
            Vector2 force = new Vector2(horizontal * movedir, vartical);
            if (player.transform.position.x>this.transform.position.x)
            {
                fixposition = this.transform.position.x+1.0f;
            }
            else { fixposition = this.transform.position.x - 1.0f; }
           GameObject Slim = Instantiate(slim,new Vector3(fixposition, this.transform.position.y, this.transform.position.z), Quaternion.identity) as GameObject;
            Slim.GetComponent<Rigidbody2D>().AddForce(force);
        }
    }
    void Wind()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        isdamage = true;
    }
    void OnCollisionEnter2D(Collision2D collision)//着地処理
    {
        if (collision.gameObject.tag == "Ground")
        {
            col.gameObject.SetActive(false);
            animator.SetBool("isFall", false);
            animator.SetBool("isLanding", true);
            // jumpJudge = true;

            //方向転換固定解除
            // rb.constraints = RigidbodyConstraints.None;
            //rb.velocity = Vector3.zero;
        }

    }
}