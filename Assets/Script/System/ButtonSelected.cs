﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelected : MonoBehaviour
{
    public void OnClick()
    {
        GameObject.Find("MainSystem").GetComponent<SkillLearned>().BottunSelected(gameObject.name);
        GameObject.Find("Canvas").GetComponentInChildren<SkillExplain>().SetText(gameObject.name);
    }
}
