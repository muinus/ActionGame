using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnAllSkill : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SkillLearned.AllSkillLreaned(true);
    }
}
