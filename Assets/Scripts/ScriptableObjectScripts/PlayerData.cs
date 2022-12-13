using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : CharData
{
    public GameObject Weapon;
    public bool IsDamaged;
    public float dashSpeed;
    public float dashCountdown;
    public float dashDuration;
    public float invulerability;
}
