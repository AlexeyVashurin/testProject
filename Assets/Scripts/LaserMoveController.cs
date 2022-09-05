using UnityEngine;

public class LaserMoveController : MonoBehaviour
{
    [SerializeField]private GameObject _gun, _platform;
    
    

    [SerializeField] private LaserControlView _laserControlView;

    void Start()
    {
        _laserControlView.Subscribe(MoveLeft,MoveRight,MoveUp,MoveDown);
    }

    private void MoveRight()
    {
        _platform.transform.Rotate(new Vector3(0, +5, 0));
    }

    private void MoveLeft()
    {
        _platform.transform.Rotate(new Vector3(0, -5, 0));
    }

    private void MoveDown()
    {
        _gun.transform.Rotate(new Vector3(+5, 0, 0));
    }

    private void MoveUp()
    {
        _gun.transform.Rotate(new Vector3(-5, 0, 0));
    }
}