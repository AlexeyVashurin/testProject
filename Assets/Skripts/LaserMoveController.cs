using UnityEngine;

public class LaserMoveController : MonoBehaviour
{
    public GameObject Gun, Platform;

    private ViewsHolder instance;

    private LaserControlView _laserControlView;

    void Start()
    {
        instance = ViewsHolder.instance;
        _laserControlView = instance.laserControlView;


        _laserControlView.moveUpButton.onClick.AddListener(() => { Gun.transform.Rotate(new Vector3(-5, 0, 0)); });
        _laserControlView.moveDownButton.onClick.AddListener(() => { Gun.transform.Rotate(new Vector3(+5, 0, 0)); });
        _laserControlView.rotateRightButton.onClick.AddListener(() => { Platform.transform.Rotate(new Vector3(0, +5, 0)); });
        _laserControlView.rotateLeftPlatformButton.onClick.AddListener(() => { Platform.transform.Rotate(new Vector3(0, -5, 0)); });
    }
}