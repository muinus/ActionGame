using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            string scene = SceneManager.GetActiveScene().name;
            SkillLearned.SetPrevStage(int.Parse(scene.Substring(5)));
            SceneManager.LoadScene("SkillsLearned");
        }

    }
}
