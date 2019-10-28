using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    public void ButtonYes()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ButtonNo()
    {
        Time.timeScale = 1;
        SkillLearned.SetPrevStage(int.Parse(SceneManager.GetActiveScene().name.Substring(5)));
        SceneManager.LoadScene("TitleScene");
    }
}
