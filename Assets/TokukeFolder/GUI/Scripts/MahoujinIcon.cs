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

    }
    private void Update()
    {
      
    }
    public void OnClick()
    {
        //taka

        if (UB_up.GetIsPressed() && judge[0] == true && SkillLearned.GetSkillActive("SummonTaka") == true)
            {
                judge[0] = false;
            Instantiate(taka, new Vector3(player.transform.position.x - 10, player.transform.position.y + 10, player.transform.position.z), Quaternion.identity);
            StartCoroutine("TukaimaCT", 0);
        }
        //yousei
        if (UB_down.GetIsPressed() &&  judge[1] == true && SkillLearned.GetSkillActive("SummonYosei") == true)
        {
            judge[1] = false;
            Instantiate(fairly, new Vector3(player.transform.position.x + 2, player.transform.position.y, player.transform.position.z), Quaternion.identity);
            StartCoroutine("TukaimaCT", 1);
        }
        if ((UB_right.GetIsPressed()|| UB_left.GetIsPressed()) && judge[2] == true && SkillLearned.GetSkillActive("SummonSaboten") == true)
        {
            judge[2] = false;
            Instantiate(saboten, new Vector3(player.transform.position.x + 2, player.transform.position.y, player.transform.position.z), Quaternion.identity);
            StartCoroutine("TukaimaCT", 2);
        }
        if (judge[3] == true && SkillLearned.GetSkillActive("SummonOokami") == true)
        {
            judge[3] = false;
            Instantiate(ookami, new Vector3(player.transform.position.x - 0.1f, player.transform.position.y - 0.1f, player.transform.position.z), Quaternion.identity);
            StartCoroutine("TukaimaCT", 2);
        }
    }
    IEnumerator TukaimaCT(int index)
    {
        yield return new WaitForSeconds(CT);
        judge[index] = true;
        Debug.Log(judge[index]);
    }
}
