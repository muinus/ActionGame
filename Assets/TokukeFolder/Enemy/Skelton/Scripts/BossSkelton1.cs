using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSkelton1 : MonoBehaviour
{

    Slider HPbar;//HPバーのオブジェクト
    public GameObject player;
    Animator animator;

    private Rigidbody rb;

    bool isdamage;
    string state;
    public int movedir;
    int i;
    int value;

    public GameObject BattleEvent;
    public GameObject thunderWave;
    public GameObject magicCircle;
    public BoxCollider2D col;
    //出現ポイント
    public GameObject upLeft, upRight, downLeft, downRight, middle;
    Vector2 upLeftV, upRightV, downLeftV, downRightV, middleV;
    Vector2[] appPoint=new Vector2[5];
    Vector3 thunderPosition;

    // Start is called before the first frame update
    void Start()
    {
        
        col.gameObject.SetActive(false);
        rb = this.GetComponent<Rigidbody>();
        HPbar = GetComponentInChildren<Slider>();
        animator = transform.root.GetComponent<Animator>();
        isdamage = false;
        player= GameObject.Find("Player");
        upLeft = GameObject.Find("upLeft");
        upRight = GameObject.Find("upRight");
        downLeft = GameObject.Find("downLeft");
        downRight = GameObject.Find("downRight");
        middle = GameObject.Find("middle");
        upLeftV = new Vector2(upLeft.transform.position.x, upLeft.transform.position.y);
        upRightV = new Vector2(upRight.transform.position.x, upRight.transform.position.y);
        downLeftV = new Vector2(downLeft.transform.position.x, downLeft.transform.position.y);
        downRightV = new Vector2(downRight.transform.position.x, downRight.transform.position.y);
        middleV = new Vector2(middle.transform.position.x, middle.transform.position.y);
        appPoint[0] = upLeftV; appPoint[1] = upRightV; appPoint[2] = downLeftV;
        appPoint[3] = downRightV; appPoint[4] = middleV;
        Debug.Log(appPoint[0]);

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
        /*if (animator.GetCurrentAnimatorStateInfo(0).IsName("skelton_Dead"))
        {
            Destroy(this.gameObject,3.0f);
            if (BattleEvent.GetComponent<BattleEventMaster>().GetIsBattleEvent())
            {
                BattleEvent.GetComponent<BattleEventMaster>().DecreaseEnemyCounter();
            }
        }*/
        if (HPbar.value <= 0)
        {
            state = "Die";
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
                break;
            case "Warp":
                animator.SetBool("isWarp", true);
                break;
            default:
                animator.SetBool("isDead", false);
                break;
        }
    }
    //攻撃アニメーションを分裂→ジャンプ→ジャンプでループさせる
    void SkeltonWarpAttack()//急接近して切りつける
    {
        //スケールプラスで右出現
        this.transform.position = new Vector2(player.transform.position.x + ((player.transform.localScale.x >= 0) ? -0.5f : 0.5f), 11.23f);
        //animator.SetBool("isAttack", true);
        
        animator.SetInteger("isRand", Random.Range(0,3));
    }
    
    void SkeltonWarpRandom()//墓場のどこかにワープする
    {
        value = Random.Range(0, 5);
        Debug.Log(value);
        Debug.Log(appPoint[value]);
        this.transform.position = appPoint[value];
    }
    void SkeltonCommingThunder()//手を空に掲げ召雷を行う
    {
        Instantiate(magicCircle, new Vector2(player.transform.position.x, player.transform.position.y+3.0f), Quaternion.identity);
        animator.SetInteger("isRand", Random.Range(0, 3));
    }
    void SkeltonThunderWave()//手から雷を発射
    {
        /*float dx = player.transform.position.x - this.gameObject.transform.position.x;
        float dy = player.transform.position.y - this.gameObject.transform.position.y;
        float rad = Mathf.Atan2(dy, dx);
        float kakudo = rad * Mathf.Rad2Deg+270;*/
        thunderPosition = new Vector3(this.transform.position.x + 0.2f, this.transform.position.y + 1.0f, this.transform.position.z);
        var vec = (player.transform.position - thunderPosition).normalized;
        Instantiate(thunderWave, new Vector2(this.transform.position.x+0.2f,this.transform.position.y+1.0f), Quaternion.FromToRotation(Vector3.up, vec));
        animator.SetInteger("isRand", Random.Range(0, 3));
    }
    void SkeltonFourOfaKind()//上2体は召雷，下２体は雷を発射する
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isdamage = true;
    }
    void Dead()
    {
        
        Destroy(this.gameObject);
    }
    void IsDead()
    {
        animator.SetBool("isCancel", true);
    }
    void AttackJudge()
    {
        if (col.gameObject.activeSelf == true)
        {
            col.gameObject.SetActive(false);
        }else if (col.gameObject.activeSelf == false)
        {
            col.gameObject.SetActive(true);
        }
    }
    void HPbarDis()
    {
        if (HPbar.gameObject.activeSelf == true)
        {
            HPbar.gameObject.SetActive(false);
        }
        else if (HPbar.gameObject.activeSelf == false)
        {
            HPbar.gameObject.SetActive(true);
        }
    }
}
