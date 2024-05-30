using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gorilla : MonoBehaviour
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

        //Show up if lights are on
        if (GameMaster.instance.lights.Contains(attackDoorway) && locations[curLocationIndex].ToLower() == "office")
        {
            positions[curLocationIndex].SetActive(true);
            if (GameMaster.instance.lights.Contains(attackDoorway)) curLocationIndex--;
            if (curLocationIndex <= 0) curLocationIndex = 0;
        } else if (locations[curLocationIndex].ToLower() == "officepet")
        {
            positions[curLocationIndex].SetActive(true);
        }
        //Jumpscare if door is not closed
        else if (locations[curLocationIndex].ToLower() == "jumpscare")
        {
            GameMaster.instance.SetMonitorOpened(false);
            GameMaster.instance.jumpscared = true;

            if (!GameMaster.instance.audioSource.isPlaying) GameMaster.instance.audioSource.PlayOneShot(GameMaster.instance.gorillaJumpscare);
            positions[curLocationIndex].SetActive(true);
        }
    }

    public void MoveAnimatronic()
    {
        if (!GameMaster.instance.lights.Contains(attackDoorway))
        {
            curLocationIndex++;
            if (curLocationIndex >= locations.Length) curLocationIndex = 0;
            GameMaster.instance.SwitchCams(GameMaster.instance.camBackground.sprite.name);
        }

    }
}
