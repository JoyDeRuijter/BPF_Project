using UnityEngine;

public class ScaleMissionLog : MonoBehaviour
{
    #region Variables
    [Header ("RectTransform references")]
    [SerializeField] RectTransform scrollView;
    [SerializeField] RectTransform content;
    
    private float scrollViewHeight, scrollViewWidth, contentHeight, currentContentHeight;
    #endregion

    void Awake()
    {
        scrollViewWidth = 160;
        scrollViewHeight = 170;
        contentHeight = content.sizeDelta.y;
    }

    void Update()
    {
        ExecuteScaling();
    }

    private void Scale(bool down)
    {
        //If it has to scale down, decrease the scrollViewHeight, if it has to scale up, increase the scrollViewHeight
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

    private void ExecuteScaling()
    {
        //Execute the scale method if the current height of the content is higher or lower than it's supposed to be
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
}
