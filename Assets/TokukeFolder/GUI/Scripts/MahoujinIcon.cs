using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahoujinIcon : MonoBehaviour
{
    public GameObject taka;
    public GameObject fairly;
    public GameObject ookami;
    public GameObject saboten;
    GameObject player;
    bool[] judge={ true, true, true, true };//taka yousei saboten ookami 
    float CT=10.0f;
    private void Start()
    {
        player= GameObject.Find("Player");
        
    }
    private void Update()
    {
        //taka
        if (Input.GetKey(KeyCode.RightArrow)&&Input.GetKey(KeyCode.Space)&&judge[0]==true)
        {
            judge[0] = false;
            Instantiate(taka, new Vector3(player.transform.position.x-10,player.transform.position.y-10,player.transform.position.z), Quaternion.identity);
            StartCoroutine("TukaimaCT", 0);
        }
        //yousei
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.Space) && judge[1] == true)
        {
            judge[1] = false;
            Instantiate(fairly, new Vector3(player.transform.position.x+2, player.transform.position.y , player.transform.position.z), Quaternion.identity);
            StartCoroutine("TukaimaCT", 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.Space) && judge[2] == true)
        {
            judge[2] = false;
            Instantiate(saboten, new Vector3(player.transform.position.x + 2, player.transform.position.y, player.transform.position.z), Quaternion.identity);
            StartCoroutine("TukaimaCT", 2);
        }
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.Space) && judge[3] == true)
        {
            judge[2] = false;
            Instantiate(ookami, new Vector3(player.transform.position.x-0.1f, player.transform.position.y-0.1f, player.transform.position.z), Quaternion.identity);
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
