using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamButton : MonoBehaviour
{
    public string backgroundType;
    private void OnMouseDown()
    {
        GameMaster.instance.SwitchCams(backgroundType);
    }
}
