using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController _player;
    private GestionInventory _gestionInventory;
    private float _countDash;
    // Start is called before the first frame update
    void Start()
    {
        _player = gameObject.GetComponent<PlayerController>();
        _gestionInventory = gameObject.GetComponent<GestionInventory>();
        _countDash = _player._playerDataSO.dashCountdown;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        MeleeInput();
        ShootInput();
        DashInput();
        ChangeWeapon();
    }

    private void MovementInput()
    {
        _player.MovementBehaivour(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void ShootInput()
    {
     if (Input.GetMouseButtonDown(1))
         _player.Shoot();           
    }

    private void MeleeInput()
    {
        if (Input.GetMouseButtonDown(0))
            _player.MeleeAttack();
    }

    private void DashInput()
    {
        if (_countDash >= _player._playerDataSO.dashCountdown)
        {
            if (Input.GetKey(KeyCode.LeftShift))
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
