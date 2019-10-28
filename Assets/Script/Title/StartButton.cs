using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void StartMove()
    {
        SkillLearned.AllSkillLreaned(false);
        SceneManager.LoadScene("Stage1");
    }
}
