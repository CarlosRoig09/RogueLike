using System.Collections;
using UnityEngine;

public enum ItemEffect
{
    temporal,
    eternal,
}
public class ControlStats : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private PlayerData playerData;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModificadorDeStat(Type stat, float modificator)
    {
        SearchForStats(stat, modificator);
    }

    public void ModificadorDeStat(Type stat, float modificator, float time)
    {
        StartCoroutine(TemporaryModif(time, stat, modificator));
    }

    private void SearchForStats(Type stat, float modificator)
    {
        switch (stat)
        {
            case Type.WeaponsDamage:
                foreach (var weapon in inventory.Weapons)
                {
                    weapon.meleeData.Damage *= modificator;
                    weapon.shootData.ProyectileDamage *= modificator;
                }
                break;
            case Type.PlayerSpeed:
                playerData.speed *= modificator;
                playerData.dashSpeed *= modificator;
                break;
        }
    }

    private IEnumerator TemporaryModif(float time, Type stat, float modificator)
    {
        SearchForStats(stat, modificator);
        yield return new WaitForSeconds(time);
        SearchForStats(stat, 1/modificator);
    }
}
