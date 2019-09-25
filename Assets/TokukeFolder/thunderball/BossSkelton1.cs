using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSkelton : MonoBehaviour
{

    Slider HPbar;//HPバーのオブジェクト
    GameObject player;
    Animator animator;

    private Rigidbody rb;

    bool isdamage;
    string state;
    public int movedir;
    int i;

    public GameObject BattleEvent;
    public GameObject thunderWave;
    public GameObject thunder;

    //出現ポイント
    Vector2 leftUp = new Vector2(1, 1);
    Vector2 rightUp = new Vector2(1, 1);
    Vector2 leftDown = new Vector2(1, 1);
    Vector2 rightDown = new Vector2(1, 1);
    Vector2 middle = new Vector2(1, 1);
    Vector2[] appPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        HPbar = GetComponentInChildren<Slider>();
        player = GameObject.Find("Player");
        animator = transform.root.GetComponent<Animator>();
        isdamage = false;
        Vector2[] appPoint = { leftUp, rightUp, leftDown, rightDown, middle };

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
            case "DAttack":
                break;
            case "Warp":
                break;
            case "CTAttack":
                break;
            case "TWAttack":
                break;
            case "FAttack":
                break;
            case "Idle":
                break;
            
            default:
                animator.SetBool("isDead", false);
                animator.SetBool("isDamaged", false);
                break;
        }
    }
    //攻撃アニメーションを分裂→ジャンプ→ジャンプでループさせる
    void SkeltonDashAttack()//急接近して切りつける
    {
        //スケールプラスで右出現
        this.transform.position = new Vector2(player.transform.position.x + ((player.transform.localScale.x >= 0) ? 1 : -1), player.transform.position.y);
    }
    void SkeltonWarpRandom()//墓場のどこかにワープする
    {
        int value = Random.Range(1, 5 + 1);
        this.transform.position = appPoint[value];
    }
    void SkeltonCommingThunder()//手を空に掲げ召雷を行う
    {
        Instantiate(thunder, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }
    void SkeltonThunderWave()//手から雷を発射
    {
       Instantiate(thunderWave, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }
    void SkeltonFourOfaKind()//上2体は召雷，下２体は雷を発射する
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isdamage = true;
    }
    
}
