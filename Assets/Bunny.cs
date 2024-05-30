using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    public int curLocationIndex;
    public string[] locations;
    public GameObject[] positions;
    public string attackDoorway;
    public int level;
    public int baseMoveTimer;

    private void Start()
    {
        if (level <= 0) level = 1;
        float moveTimer = baseMoveTimer / level;
        InvokeRepeating(nameof(MoveAnimatronic), moveTimer, moveTimer);
    }


    public void UpdatePosition()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i].SetActive(false);
        }

        //Show up if in camera
        if (GameMaster.instance.camBackground.sprite.name.ToLower() == locations[curLocationIndex].ToLower())
        {
            positions[curLocationIndex].SetActive(true);
        }
        //Show up if lights are on
        else if (GameMaster.instance.lights.Contains(attackDoorway) && locations[curLocationIndex].ToLower() == "office")
        {
            positions[curLocationIndex].SetActive(true);
        }
        //Reset to start if door is closed
        else if (GameMaster.instance.doors.Contains(attackDoorway) && locations[curLocationIndex].ToLower() == "jumpscare")
        {
            curLocationIndex = 0;
        }
        //Jumpscare if door is not closed
        else if (locations[curLocationIndex].ToLower() == "jumpscare")
        {
            GameMaster.instance.SetMonitorOpened(false);
            GameMaster.instance.jumpscared = true;
            positions[curLocationIndex].SetActive(true);

            if (!GameMaster.instance.audioSource.isPlaying) GameMaster.instance.audioSource.PlayOneShot(GameMaster.instance.bunnyJumpscare);
        }
    }

    public void MoveAnimatronic()
    {
        curLocationIndex++;
        if (curLocationIndex >= locations.Length) curLocationIndex = 0;
        GameMaster.instance.SwitchCams(GameMaster.instance.camBackground.sprite.name);
    }
}
