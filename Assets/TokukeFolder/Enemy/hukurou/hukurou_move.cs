using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class hukurou_move : MonoBehaviour
{

    Slider HPbar;//HPバーのオブジェクト
    public GameObject player;
    public GameObject flyposR;
    public GameObject flyposL;
    public GameObject flytop;
    public GameObject feather;
    public GameObject featherMachine;
    public GameObject kazePoint;
    public GameObject kazeseiseiki;
    public GameObject kaze;
    
    public BoxCollider2D col;
    public BoxCollider2D Dcol;
    int count=0;
    int drec;

    Vector3 b;
    private float time = 10.0f;
    bool posJudge=true;
    bool startJudge=false;
    private float startTime;
    bool windJudge = true;

    Animator animator;

    private Rigidbody2D rb;

    bool isdamage;
    string state;
    public int movedir;
    int i;
    int value;

    public GameObject BattleEvent;


    // Start is called before the first frame update
    void Start()
    {
        Dcol.enabled=false;
        kaze.SetActive(false);
        col.enabled = false;
        rb = this.GetComponent<Rigidbody2D>();
        HPbar = GetComponentInChildren<Slider>();
        animator = transform.root.GetComponent<Animator>();
        isdamage = false;
        BattleEvent = GameObject.Find("BattleEventMaster");
        StartMove();
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
        drec = System.Math.Sign(this.transform.position.x - player.transform.position.x);
        this.transform.rotation = new Quaternion(0, 90.0f * drec + 90.0f, 0, 0);

        if (HPbar.value <= 0)
        {
            state = "Die";
        }else if (HPbar.value<=250&&windJudge==true)
        {
            animator.SetBool("isWind", true);
            windJudge = false;
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
            case "Idle":
                animator.SetBool("isIdle", true);
                break;
            default:
                animator.SetBool("isDead", false);
                break;
        }
    }
    //攻撃アニメーションを分裂→ジャンプ→ジャンプでループさせる
    
    
    IEnumerator Fly_attack()
    {
        animator.SetInteger("isRand", Random.Range(0, 3));
        animator.SetBool("isIdle", false);
        var subpos = new Vector3();
        if (posJudge)
        {
            subpos = flyposL.transform.position;posJudge = false;
        }
        else
        {
           subpos = flyposR.transform.position; posJudge = true;
        }
        Vector3[] path =
        {
            this.transform.position,player.transform.position,subpos
        };
        transform.DOLocalPath(path, 3.0f,PathType.CatmullRom).SetOptions(false);
        col.enabled = true;


        yield return null;
    }
    void FeatherAttack()
    {
        animator.SetInteger("isRand", Random.Range(0, 3));
        var vec = (player.transform.position - this.transform.position).normalized;
        //this.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);
        Instantiate(feather, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.FromToRotation(Vector3.up, vec));
    }
    IEnumerator ForestAttack()
    {
        animator.SetInteger("isRand", Random.Range(0, 3));
        animator.SetBool("isIdle", false);
        var subpos = new Vector3();
        if (posJudge)
        {
            subpos = flyposL.transform.position; posJudge = false;
        }
        else
        {
            subpos = flyposR.transform.position; posJudge = true;
        }
        Vector3[] path =
        {
            this.transform.position,flytop.transform.position, subpos
        };
        transform.DOLocalPath(path, 10.0f, PathType.CatmullRom).SetOptions(false);
        Instantiate(featherMachine, flytop.transform.position, Quaternion.identity);
        yield return null;
    }
    void IdleCount()
    {
        if (startJudge == true)
        {
            count += 1;
            animator.SetInteger("idleCount", count);
            if (count >= 3)
            {
                count = 0;
            }
        }
    }
    IEnumerator Kazeokosi()
    {
        Instantiate(kazeseiseiki, kazePoint.transform.position, Quaternion.identity);
        kaze.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        animator.SetBool("isWind", false);
    }
    void StartMove()
    {
        Vector3[] path =
        {
            this.transform.position,flytop.transform.position, flyposR.transform.position
        };
        transform.DOLocalPath(path, 10.0f, PathType.CatmullRom).SetOptions(false);
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
       
           isdamage = true;
        
        if (other.tag == "flyPoint")
        {
            startJudge = true;
            col.enabled = false;
            animator.SetBool("isIdle", true);
            animator.SetBool("isForest", true);
        }
    }
    void DeadJudge()
    {
        animator.SetBool("isCancel", true);
        Dcol.enabled = true;
        Vector2 force = new Vector3(movedir*2.0f, 50.0f);
        rb.gravityScale = 0.5f;
        rb.AddForce(force);
        
    }
    void OnCollisionEnter2D(Collision2D collision)//着地処理
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("isGround", true);
            Destroy(this.gameObject, 2.0f);
        }

    }

}
