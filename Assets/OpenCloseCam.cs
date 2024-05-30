using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseCam : MonoBehaviour
{
    public bool camToggle = false;

    private void OnMouseDown()
    {
        if (GameMaster.instance.power <= 0) return;
        camToggle = !camToggle;
        GameMaster.instance.SetMonitorOpened(camToggle);
    }
}
