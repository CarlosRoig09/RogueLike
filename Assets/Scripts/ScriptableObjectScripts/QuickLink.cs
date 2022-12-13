using UnityEngine;

[CreateAssetMenu(fileName = "QuickLink", menuName = "QuickLink")]
public class QuickLink : ScriptableObject
{
    public Player playerScript;
    public ShootSO weaponData;
    public ShootControler weaponScript;
}
