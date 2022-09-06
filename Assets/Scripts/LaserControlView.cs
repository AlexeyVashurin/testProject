using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserControlView : MonoBehaviour
{
    [SerializeField] private Button rotateLeftPlatformButton, rotateRightPlatformButton, moveUpButton, moveDownButton;

    public void Subscribe(Action moveLeft, Action moveRight, Action moveUp, Action moveDown)
    {
        rotateLeftPlatformButton.onClick.AddListener(moveLeft.Invoke);
        rotateRightPlatformButton.onClick.AddListener(moveRight.Invoke);
        moveUpButton.onClick.AddListener(moveUp.Invoke);
        moveDownButton.onClick.AddListener(moveDown.Invoke);
    }
}
