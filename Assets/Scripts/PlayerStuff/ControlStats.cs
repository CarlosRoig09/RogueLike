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
    private PlayerStats playerStats;
    private ApplyModificator _applyModificator;
    void Start()
    {
        playerStats.AttackSpeed = 1;
        playerStats.Damage = 1;
        playerStats.ProyectileDamage = 1;
        playerStats.ProyectileSpeed = 1;
        playerStats.Speed = 1;
        _applyModificator= GetComponent<ApplyModificator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModificadorDeStat(Type stat, float modificator)
    {
        if (!SuprassLimit(stat))
        {
            SearchForStats(stat, modificator);
        }
    }

    public void ModificadorDeStat(Type stat, float modificator, float time)
    {
        StartCoroutine(TemporaryModif(time, stat, modificator));
    }

    private bool SuprassLimit(Type stat)
    {
        switch (stat)
        {
            case Type.WeaponsDamage:
                if(playerStats.Damage>=1.6f)
                    return true;
                break;
            case Type.PlayerSpeed:
                if (playerStats.Speed >= 1.6f)
                    return true;
                break;
            case Type.AttackSpeed:
                if (playerStats.AttackSpeed >= 1.6f)
                    return true;
                break;
            case Type.ProyectileSpeed:
                if (playerStats.ProyectileSpeed >= 1.6f)
                    return true;
                break;
        }
        return false;
    }

    private void SearchForStats(Type stat, float modificator)
    {
        switch (stat)
        {
            case Type.WeaponsDamage:
                playerStats.Damage += modificator;
                break;
            case Type.PlayerSpeed:
                playerStats.Speed += modificator;
                _applyModificator.UpdateSpeed();
                break;
            case Type.AttackSpeed:
                playerStats.AttackSpeed += modificator;
                break;
            case Type.ProyectileSpeed: 
                playerStats.ProyectileSpeed += modificator;
                break;
        }
    }

    private IEnumerator TemporaryModif(float time, Type stat, float modificator)
    {
        SearchForStats(stat, modificator);
        yield return new WaitForSeconds(time);
        SearchForStats(stat, modificator*-1);
    }
}
