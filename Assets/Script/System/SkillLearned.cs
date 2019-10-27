using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkillLearned : MonoBehaviour
{
    public static Dictionary<string, bool> skillTable
        = new Dictionary<string, bool>()
        {
            //剣スキル
            {"Hamma", false},
            {"Sickle", false},
            {"HighSlash", false},
            {"ThrowSword", false},
            {"Iai", false},
            {"AirRaid", false},
            {"Slashing", false},
            //銃スキル
            {"Fannel", false},
            {"RailGun", false},
            {"Midareuti", false},
            {"ShotGun", false},
            {"MachineGun", false},
            //魔法スキル
            {"WaterMasic", false},
            {"FireTower", false},
            {"Tyoson", false},
            {"Lightning-Strike", false},
            {"BlackHole", false},
            //移動スキル
            {"ALanding", false},
            {"ADrift", false},
            //召喚スキル
            {"SummonTaka", false},
            {"SummonYosei", false},
            {"SummonSaboten", false},
            {"SummonOokami", false},
            //ポーション
            {"Potion", false}
        };

    GameObject skill;
    GameObject warning;

    bool islearned;

    //string buttonSelected="none";
    Transform buttonSelected;

    static ColorBlock selectedColor;

    // Start is called before the first frame update
    void Start()
    {
        skill = GameObject.Find("Canvas").transform.GetChild(0).Find("Skill").gameObject;
        warning = GameObject.Find("Canvas").transform.GetChild(1).gameObject;

        warning.SetActive(false);
        islearned = false;

        selectedColor.highlightedColor = Color.blue;
        selectedColor.normalColor = Color.blue;
        selectedColor.selectedColor = Color.blue;
        selectedColor.pressedColor = new Color(200f / 255f, 200f / 255f, 200f / 255f, 255f / 255f);
        selectedColor.disabledColor = new Color(245f / 255f, 245f / 255f, 245f / 255f, 128f / 255f);
        selectedColor.colorMultiplier = 1.0f;
        selectedColor.fadeDuration = 0.1f;

        ChangeBottunColor();
    }

    public static void AllSkillLreaned()
    {
        List<string> TmpList = new List<string>();
        foreach (KeyValuePair<string, bool> skillstate in skillTable)
        {
            TmpList.Add(skillstate.Key);
        }

        foreach (string skillKey in TmpList)
        {
            skillTable[skillKey] = true;
        }
    }

    public void DebugD()
    {
        foreach ( KeyValuePair<string,bool> skill in skillTable)
            Debug.Log(skill.Key+":"+skill.Value);
    }

    public void BottunSelected(Transform buttonSelected)
    {
        this.buttonSelected = buttonSelected;
    }

    public void SetSkillActive()
    {
        if (buttonSelected == null)
            return;

        if (!islearned)
        {
            buttonSelected.GetComponent<Button>().colors=selectedColor;

            skillTable[buttonSelected.name] = true;
            islearned = true;
        }
    }

    public static bool GetSkillActive(string skillName)
    {
        return skillTable[skillName];
    }

    public void NextScene()
    {
        if (islearned)
            SceneManager.LoadScene("Stage5");
        else
            warning.SetActive(true);

    }

    public void ChangeColor(string name)
    {
        skill.transform.Find(name).GetComponent<Button>().colors=selectedColor;
    }

    public void ChangeBottunColor()
    {
        foreach (KeyValuePair<string, bool> skillstate in skillTable)
        {
            if(skillstate.Value)
                ChangeColor(skillstate.Key);
        }
    }

    public void ButtonYes()
    {
        islearned = true;
        NextScene();
    }

    public void ButtonNo()
    {
        warning.SetActive(false);
    }
}
