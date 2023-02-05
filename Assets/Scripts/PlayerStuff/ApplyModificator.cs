using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyModificator : MonoBehaviour
{
    [SerializeField]
    private PlayerStats _stats;
    private PlayerController _controller;
    private void Start()
    {
        _controller = GetComponent<PlayerController>();
    }
    public void UpdateSpeed()
    {
        _controller.PlayerDataSO.speed  *=_stats.Speed;
        _controller.PlayerDataSO.dashSpeed *= _stats.Speed;
    }
    public void UpdateWeaponStats(IWeaponControler weapon)
    {
        weapon.WeaponDamage = _stats.Damage;
        weapon.WeaponSpeed = _stats.Speed;
        weapon.ProyectileDamage = _stats.ProyectileDamage;
        weapon.ProyectileSpeed = _stats.ProyectileSpeed;
    }
}
