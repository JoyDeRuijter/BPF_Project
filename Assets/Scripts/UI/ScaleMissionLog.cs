using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleMissionLog : MonoBehaviour
{
    [SerializeField] RectTransform scrollView, content;
    private float scrollViewHeight, scrollViewWidth, contentHeight, currentContentHeight;

    void Start()
    {
        scrollViewWidth = 160;
        contentHeight = content.sizeDelta.y;
        scrollViewHeight = 170;
    }

    void Update()
    {
        currentContentHeight = content.sizeDelta.y;

        if (currentContentHeight < contentHeight)
        {
            contentHeight = currentContentHeight;
            Scale(true);
        }
        else if (currentContentHeight > contentHeight)
        {
            contentHeight = currentContentHeight;
            Scale(false);
        }
    }

    private void Scale(bool down)
    {
        if (down)
        {
            scrollViewHeight -= 32f;
            scrollView.sizeDelta = new Vector2(scrollViewWidth, scrollViewHeight);
            return;
        }
        else
        { 
            scrollViewHeight += 32f;
            scrollView.sizeDelta = new Vector2 (scrollViewWidth, scrollViewHeight);
            return;
        }
            

        
    }
        
}
