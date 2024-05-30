using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public SpriteRenderer curBackground;
    public SpriteRenderer camBackground;
    public Sprite[] backgrounds;
    public Dictionary<string, Sprite> backgroundDict = new Dictionary<string, Sprite>();

    public List<string> lights; //[], [left], [left, middle, right]
    public List<string> doors; //[], [left,right]

    public Animator leftDoor;
    public Animator rightDoor;

    public GameObject monitor;

    public Dictionary<string, string[]> animatronicLocations = new Dictionary<string, string[]>();

    public Bear bear;
    public Bunny bunny;
    public Chicken chicken;
    public Fox fox;
    public Gorilla gorilla;

    public bool monitorUp;
    public bool jumpscared = false;
    bool gameOver = false;

    public int power = 100;
    public int usageLevelLights = 1;
    public int usageLevelDoors = 1;

    public int time = 0;

    public AudioSource audioSource;
    public AudioClip bearJumpscare;
    public AudioClip bunnyJumpscare;
    public AudioClip chickenJumpscare;
    public AudioClip foxJumpscare;
    public AudioClip gorillaJumpscare;


    private void Awake()
    {
        instance = this;   
    }



    private void Start()
    {
        InvokeRepeating(nameof(UsePower), 4f, 4f);
        InvokeRepeating(nameof(AddTime), 30f, 30f);
        foreach(Sprite sprite in backgrounds)
        {
            backgroundDict.Add(sprite.name,sprite);
        }
        animatronicLocations.Add("bear", bear.locations);
        animatronicLocations.Add("bunny", bunny.locations);
        animatronicLocations.Add("chicken", chicken.locations);

        SwitchCams("Showroom");
    }

    private void Update()
    {
        if (jumpscared)
        {
            if (!gameOver) StartCoroutine(GoToGameOver());
        }
    }

    IEnumerator GoToGameOver()
    {
        gameOver = true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOver");
    }

    public void UpdateLights()
    {
        //It can either be empty, left, middle, or right. No duplicates, any combination
        bunny.UpdatePosition();
        chicken.UpdatePosition();
        gorilla.UpdatePosition();

        if(lights.Count <= 0)
        {
            usageLevelLights = 1;
            curBackground.sprite = backgroundDict["Office"];
            return;
        }

        //All three lights enabled
        if(lights.Count >= 3)
        {
            usageLevelLights = 10;
            curBackground.sprite = backgroundDict["OfficeAllLight"];

        }
        //2 lights enabled
        //[left, right], [left, middle], [middle,right]
        else if (lights.Count == 2)
        {
            if(!lights.Contains("left"))
            {
                usageLevelLights = 5;
                curBackground.sprite = backgroundDict["OfficeMiddleRightLight"];
            } else if (!lights.Contains("right"))
            {
                usageLevelLights = 5;
                curBackground.sprite = backgroundDict["OfficeMiddleLeftLight"];
            } else
            {
                usageLevelLights = 5;
                curBackground.sprite = backgroundDict["OfficeLeftRightLight"];
            }
        }
        //1 light enabled
        else
        {
            if (lights.Contains("left"))
            {
                usageLevelLights = 3;
                curBackground.sprite = backgroundDict["OfficeLeftLight"];
            }
            else if (lights.Contains("right"))
            {
                usageLevelLights = 3;
                curBackground.sprite = backgroundDict["OfficeRightLight"];
            }
            else
            {
                usageLevelLights = 3;
                curBackground.sprite = backgroundDict["OfficeMiddleLight"];
            }
        }
    }

    public void UpdateDoors()
    {
        if(doors.Count <= 0)
        {
            usageLevelDoors = 1;
            leftDoor.Play("leftdoor_open");
            rightDoor.Play("rightdoor_open");
            return;
        }

        if(doors.Count >= 2)
        {
            usageLevelDoors = 10;
            leftDoor.Play("leftdoor_close");
            rightDoor.Play("rightdoor_close");

        } else if (doors.Contains("left"))
        {
            usageLevelDoors = 5;
            leftDoor.Play("leftdoor_close");
            rightDoor.Play("rightdoor_open");
        } else
        {
            usageLevelDoors = 5;
            leftDoor.Play("leftdoor_open");
            rightDoor.Play("rightdoor_close");
        }
    }

    public void SetMonitorOpened(bool opened)
    {
        string monitoranim = opened ? "monitor_open": "monitor_close";
        monitor.GetComponent<Animator>().Play(monitoranim);
        monitorUp = opened;
    }

    public void SwitchCams(string backgroundType)
    {
        camBackground.sprite = backgroundDict[backgroundType];
        bear.UpdatePosition();
        bunny.UpdatePosition();
        chicken.UpdatePosition();
        fox.UpdatePosition();
        gorilla.UpdatePosition();
        if(monitorUp) UsePower();
    }


    public void UsePower()
    {
        power -= Mathf.RoundToInt((usageLevelLights + usageLevelDoors) / 5) ;
        if(power <= 0)
        {
            power = 0;
            SetMonitorOpened(false);
            doors.Clear();
            lights.Clear();

            UpdateLights();
            UpdateDoors();
            curBackground.color = new Color(0, 0, 0, 1);
        }
    }

    public void AddTime()
    {
        time++;
        if (time >= 6) SceneManager.LoadScene("YouWin");
    }

}
