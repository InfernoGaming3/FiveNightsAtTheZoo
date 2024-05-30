using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeButton : MonoBehaviour
{
    public string buttonType; //[lights, door]
    public string buttonLocation; //[left, middle, right]

    public bool buttonToggle; // on or off

    private void OnMouseDown()
    {
        if (GameMaster.instance.power <= 0) return;
        if(!buttonToggle)
        {
            if(buttonType=="lights")
            {
                GameMaster.instance.lights.Add(buttonLocation);
                GameMaster.instance.UpdateLights();
            } else
            {
                GameMaster.instance.doors.Add(buttonLocation);
                GameMaster.instance.UpdateDoors();
            }  
        } else
        {
            if (buttonType == "lights")
            {
                GameMaster.instance.lights.Remove(buttonLocation);
                GameMaster.instance.UpdateLights();
            }
            else
            {
                GameMaster.instance.doors.Remove(buttonLocation);
                GameMaster.instance.UpdateDoors();
            }
        }

        buttonToggle = !buttonToggle;
    }
}
