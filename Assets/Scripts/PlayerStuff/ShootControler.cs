using System.Collections;
using UnityEngine;

public class ShootControler: MonoBehaviour
{
    private ShootSO _shootSO;
    public void NewWeapon(ShootSO newShoot)
    {
        _shootSO = newShoot;
    }

    private void Update()
    {
        if (_shootSO.CountCadenceTime < _shootSO.CadenceTime)
            _shootSO.CountCadenceTime += Time.deltaTime;
    }
    public void ProyectileSpawn()
    {
        if (_shootSO.CountCadenceTime >= _shootSO.CadenceTime)
        {
            if (_shootSO.currentBullets > 0)
                for (float i = 0; i < _shootSO.BulletsXShoot; i++)
                {
                   var proyectile = Instantiate(_shootSO.Proyectile, transform.position, Quaternion.identity);
                    proyectile.GetComponent<ProyectileBehaivour>().WeaponType(_shootSO);
                    _shootSO.currentBullets -= 1;
                    _shootSO.CountCadenceTime = 0;
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
                StartCoroutine(ReloadWeapon(_shootSO.Reload));
        } 
    }
    private IEnumerator ReloadWeapon(float time)
    {
        yield return new WaitForSeconds(time);
        _shootSO.currentBullets = _shootSO.TotalBullets;
    }
}

