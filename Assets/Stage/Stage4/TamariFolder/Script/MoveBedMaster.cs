using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBedMaster : MonoBehaviour
{

    private Dictionary<int, (int,float,float,int)> registerIndex = new Dictionary<int, (int,float, float,int)>();

    private int time=0;

    public int acceptableFrame=5;//何フレームまで動く床とプレイヤーが接触していない状態を許容するか
    // Start is called before the first frame update
    void Start()
    {
        time = Time.frameCount;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void setRideGameObject(GameObject ride_g, GameObject bed_g)
    {
        //ride_g:プレイヤーなど動く床の上に乗っているオブジェクト
        //bed_g : 動く床のゲームオブジェクト

        time = Time.frameCount;
        int ride_id = ride_g.GetInstanceID();
        int bed_id = bed_g.GetInstanceID();

        registerIndex[ride_id] = (bed_id,ride_g.transform.position.x, ride_g.transform.position.y,time);
    }
    public void RemoveRideInfo(GameObject ride_g)
    {
        int ride_id = ride_g.GetInstanceID();
        registerIndex.Remove(ride_id);
    }

    public Vector2 getMoveVector(GameObject ride_g,GameObject bed_g,Vector2 bed_speed)
    {
        //ride_g:プレイヤーなど動く床の上に乗っているオブジェクト
        //bed_g :ride_gが今乗っている動く床のゲームオブジェクト
        //bed_speed:足場の動く床の速度
        time = Time.frameCount;
        int ride_id = ride_g.GetInstanceID();
        int bed_id = bed_g.GetInstanceID();



        float tmp = bed_speed.y;
        if (tmp != 0)
        {
            tmp *= 2;
        }
        Vector2 killedVectorYPlusBedSpeed = new Vector2(bed_speed.x, tmp);

        if (registerIndex.ContainsKey(ride_id))
        {
            //ここを呼ばれる以前に何かの足場に乗っている

            int registed_bed_id = registerIndex[ride_id].Item1;
            float registed_ride_g_x= registerIndex[ride_id].Item2;
            float registed_ride_g_y = registerIndex[ride_id].Item3;
            int registed_time = registerIndex[ride_id].Item4;

            if (bed_id != registed_bed_id)
            {
                //前乗っていた足場から乗り換えている
                setRideGameObject(ride_g, bed_g);//新しく登録

                return killedVectorYPlusBedSpeed;
            }

            //ここまでくれば前に乗っている足場は今と同じ足場
            //acceptableFrame以内か。
            if (time - registed_time <= acceptableFrame)
            {
                setRideGameObject(ride_g,bed_g);//更新して
                return killedVectorYPlusBedSpeed * (time - registed_time);//値を返しておしまい
            }
            else
            {
                //ジャンプかなんかで足場から離れていたと判定
                setRideGameObject(ride_g, bed_g);//更新して
                return killedVectorYPlusBedSpeed;
            }
        }
        else
        {
            //初期登録処理
            setRideGameObject(ride_g,bed_g);
            return killedVectorYPlusBedSpeed;
        }

    }
}
