using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Player _player;
    private GestionInventory _gestionInventory;
    private float _countDash;
    // Start is called before the first frame update
    void Start()
    {
        _player = gameObject.GetComponent<Player>();
        _gestionInventory = gameObject.GetComponent<GestionInventory>();
        _countDash = _player._playerDataSO.dashCountdown;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        ShootInput();
        DashInput();
        ChangeWeapon();
    }

    private void MovementInput()
    {
        _player.Movement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void ShootInput()
    {
     if (Input.GetMouseButtonDown(0))
         _player.Shoot();           
    }

    private void DashInput()
    {
        if (_countDash >= _player._playerDataSO.dashCountdown)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                _player.Dash();
                _countDash = 0;
            }
        }
        else _countDash += Time.deltaTime;
    }

    private void ChangeWeapon()
    {
       if (Input.GetMouseButtonUp(2))
            _gestionInventory.ChangeWeapon();
    }
}
