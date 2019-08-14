using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerSt1 : MonoBehaviour
{
    public GameObject player;

    public GameObject BattleEvent;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!BattleEvent.GetComponent<BattleEvent>().GetIsBattleEvent())
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.75f, -1);
        }
    }
}