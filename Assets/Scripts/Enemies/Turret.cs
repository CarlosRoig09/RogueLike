using UnityEngine;
public class Turret : Enemy
{
   //private ShootControler _shootControler;
    [SerializeField]
    private ShootSO _shootSO;
    private ShootControler _shootControler;
    protected override void Start()
    {
        base.Start();
      _shootControler = gameObject.GetComponent<ShootControler>();
      _shootControler.NewWeapon(Instantiate(_shootSO));
    }
    protected override void Update()
    {
        base.Update();
      Shoot();
    }
    private void Shoot()
    {
        gameObject.GetComponent<ShootControler>().ProyectileSpawn();
    }
}
