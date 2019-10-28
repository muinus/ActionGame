using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahoujinIcon : MonoBehaviour
{
    public GameObject taka;
    public GameObject fairly;
    public GameObject ookami;
    public GameObject saboten;
    public GameObject up;
    public GameObject down;
    public GameObject right;
    public GameObject left;

    UIBottun UB_up;
    UIBottun UB_down;
    UIBottun UB_left;
    UIBottun UB_right;
    UIBottun UB_summon;

    GameObject player;
    bool[] judge={ true, true, true, true };//taka yousei saboten ookami 
    float CT=10.0f;
    private void Start()
    {
        player= GameObject.Find("Player");
        UB_up = up.GetComponent<UIBottun>();
        UB_down = down.GetComponent<UIBottun>();
        UB_right = right.GetComponent<UIBottun>();
        UB_left = left.GetComponent<UIBottun>();
        UB_summon = GetComponent<UIBottun>();

    }
    private void Update()
    {
        ChangeState();
    }
    public void ChangeState()
    {
        //taka

        if ((UB_summon.GetIsPressedDown()||Input.GetKeyDown(KeyCode.V))&& (UB_up.GetIsPressed()||Input.GetKey(KeyCode.UpArrow)) && SkillLearned.GetSkillActive("SummonTaka") )
        {
            if (judge[0])
            {
                judge[0] = false;
                Instantiate(taka, new Vector3(player.transform.position.x - 10, player.transform.position.y + 10, player.transform.position.z), Quaternion.identity);
                StartCoroutine("TukaimaCT", 0);
            }
        }
        //yousei
        else if ((UB_summon.GetIsPressedDown() || Input.GetKeyDown(KeyCode.V)&&(UB_down.GetIsPressed()||Input.GetKey(KeyCode.DownArrow))&& SkillLearned.GetSkillActive("SummonYosei") ))
        {
            if (judge[1])
            {
                judge[1] = false;
                Instantiate(fairly, new Vector3(player.transform.position.x + 2, player.transform.position.y, player.transform.position.z), Quaternion.identity);
                StartCoroutine("TukaimaCT", 1);
            }
        }
        else if ((UB_summon.GetIsPressedDown() || Input.GetKeyDown(KeyCode.V)&&(UB_right.GetIsPressed()|| UB_left.GetIsPressed()||Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.LeftArrow)) && judge[2]  && SkillLearned.GetSkillActive("SummonSaboten") ))
        {
            if (judge[2])
            {
                judge[2] = false;
                Instantiate(saboten, new Vector3(player.transform.position.x + 2, player.transform.position.y, player.transform.position.z), Quaternion.identity);
                StartCoroutine("TukaimaCT", 2);
            }
        }
        else if ((UB_summon.GetIsPressedDown() || Input.GetKeyDown(KeyCode.V)&&judge[3] && SkillLearned.GetSkillActive("SummonOokami")))
        {
            if (judge[3])
            {
                judge[3] = false;
                Instantiate(ookami, new Vector3(player.transform.position.x - 0.1f, player.transform.position.y - 0.4f, player.transform.position.z), Quaternion.identity);
                StartCoroutine("TukaimaCT", 3);
            }
        }
    }
    IEnumerator TukaimaCT(int index)
    {
        yield return new WaitForSeconds(CT);
        judge[index] = true;
        Debug.Log(judge[index]);
    }
}
