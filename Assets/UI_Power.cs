using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Power : MonoBehaviour
{
    public TextMeshProUGUI powerText;

    private void Update()
    {
        powerText.SetText("Power: "+ GameMaster.instance.power + "%" + "\n" + "Usage Level: " + 
            (GameMaster.instance.usageLevelLights + GameMaster.instance.usageLevelDoors));
    }
}
