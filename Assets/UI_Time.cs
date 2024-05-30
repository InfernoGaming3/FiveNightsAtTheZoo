using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Time : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void Update()
    {
        if (GameMaster.instance.time == 0) timeText.SetText("12AM"); else timeText.SetText(GameMaster.instance.time + "AM");
    }
}
