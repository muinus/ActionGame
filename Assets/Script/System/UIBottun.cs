using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBottun : MonoBehaviour
{
    bool ispressed;
    bool ispressedDown;
    bool ispressedUp;

    private void Start()
    {
        ispressed = false;
        ispressedDown = false;
        ispressedUp = false;
    }

    private void Update()
    {
        this.ispressedDown = false;
        this.ispressedUp = false;
    }

    // Update is called once per frame
    public void PointerDown()
    {
        ispressed = true;
        ispressedDown=true;
    }

    public void PointerUP()
    {
        ispressed = false;
        ispressedUp=true;
    }
    


    public bool GetIsPressed()
    {
        return ispressed;
    }

    public bool GetIsPressedDown()
    {
        bool ispressedDown = this.ispressedDown;
                
        return ispressedDown;
    }

    public bool GetIsPressedUp()
    {
        bool ispressedUp = this.ispressedUp;
        return ispressedUp;
    }
}
