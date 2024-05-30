using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollArea : MonoBehaviour
{
    public GameObject background;

    public float scrollBoundX;
    public float scrollSpeed;

    public bool left;

    private void OnMouseOver()
    {
        if((background.transform.position.x < scrollBoundX && left) || (background.transform.position.x > scrollBoundX && !left))
        {
            background.transform.position = new Vector2(background.transform.position.x + scrollSpeed, background.transform.position.y);
        }

    }
}
