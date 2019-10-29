using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<Text>().enabled = true;

        if (SkillLearned.GetPrevStage() == 0)
            gameObject.GetComponentInChildren<Text>().enabled = false;


    }

    // Update is called once per frame
    public void Continue()
    {
        if (SkillLearned.GetPrevStage() == 0)
            return;

        SceneManager.LoadScene("Stage" + SkillLearned.GetPrevStage().ToString());
    }
}
