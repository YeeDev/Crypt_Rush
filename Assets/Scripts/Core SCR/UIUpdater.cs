using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] RectTransform currentHearts = null;
    [SerializeField] RectTransform backgroundHearts = null;

    float singleHeartSize;
    float maxSize;

    private void Awake()
    {
        singleHeartSize = currentHearts.sizeDelta.x;
    }

    //Called in HitTaker
    public void InitializeUI(float maxHearts)
    {
        maxSize = maxHearts * singleHeartSize;

        ResizeHeartBar(maxHearts, true);
    }

    public void ResizeHeartBar(float modifier, bool changeBackground = false)
    {
        Vector2 newDeltaSize = currentHearts.sizeDelta;
        newDeltaSize.x = Mathf.Clamp(newDeltaSize.x + modifier * singleHeartSize, 0, maxSize);
        currentHearts.sizeDelta = newDeltaSize;

        if (changeBackground)
        {
            backgroundHearts.sizeDelta = newDeltaSize;
        }
    }
}
