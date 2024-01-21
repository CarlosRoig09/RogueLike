using System.Collections;
using UnityEngine;

public class ShootControler: MonoBehaviour
{
    private ShootSO _shootSO;
    private float _count;
    private bool _reolading;

    public void Awake()
    {
        _reolading= false;
    }
    public void NewWeapon(ShootSO newShoot)
    {
        _shootSO = newShoot;
        _count = _shootSO.CadenceTime;
    }

    public void ProyectileSpawn()
    {
        if (_shootSO.CadenceTime <= _count)
        {
            _count = 0;
            if (_shootSO.currentBullets > 0&&!_reolading)
                for (float i = 0; i < _shootSO.BulletsXShoot; i++)
                {
                    AudioManager.instance.Play("ShootSound");
                    var proyectile = Instantiate(_shootSO.Proyectile, transform.position, Quaternion.identity);
                    proyectile.GetComponent<ProyectileBehaivour>().WeaponType(_shootSO);
                    _shootSO.currentBullets -= 1;
                    switch (_shootSO.proyectileUser)
                    {
                        case ProyectileUser.Player:
                            proyectile.GetComponent<ProyectileBehaivour>().Tragectory(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                            break;
                        case ProyectileUser.Enemy:
                            proyectile.GetComponent<ProyectileBehaivour>().Tragectory(gameObject.GetComponent<Enemy>().Player.transform.position);
                            break;
                    }
                }
            else
            {
                if(!_reolading)
                StartCoroutine(ReloadWeapon(_shootSO.Reload));
            }
        }
        else _count += Time.deltaTime;
    }
    private IEnumerator ReloadWeapon(float time)
    {
        _reolading= true;
        yield return new WaitForSeconds(time);
        _shootSO.currentBullets = _shootSO.TotalBullets;
        _reolading = false;
    }
}

