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
    private GameObject _buffCanvas;
    void Start()
    {
        playerStats.AttackSpeed = 1;
        playerStats.Damage = 1;
        playerStats.ProyectileDamage = 1;
        playerStats.ProyectileSpeed = 1;
        playerStats.Speed = 1;
        _applyModificator= GetComponent<ApplyModificator>();
        _buffCanvas = transform.GetChild(1).GetChild(0).gameObject;
        _buffCanvas.SetActive(false);
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
                if(playerStats.Damage>=1.4f)
                    return true;
                break;
            case Type.PlayerSpeed:
                if (playerStats.Speed >= 1.4f)
                    return true;
                break;
            case Type.AttackSpeed:
                if (playerStats.AttackSpeed >= 1.4f)
                    return true;
                break;
            case Type.ProyectileSpeed:
                if (playerStats.ProyectileSpeed >= 1.4f)
                    return true;
                break;
            case Type.ProyectileDamage:
                 if(playerStats.ProyectileDamage >= 1.4f)
                    return true;
                 break;
        }
        return false;
    }

    private void SearchForStats(Type stat, float modificator)
    {
        string textToShow = "";
        switch (stat)
        {
            case Type.WeaponsDamage:
                AudioManager.instance.Play("AttackBuff");
                playerStats.Damage += modificator;
                textToShow = "Attack Damage";
                break;
            case Type.PlayerSpeed:
                AudioManager.instance.Play("SpeedBuff");
                playerStats.Speed += modificator;
                _applyModificator.UpdateSpeed();
                textToShow = "Speed";
                break;
            case Type.AttackSpeed:
                AudioManager.instance.Play("SpeedBuff");
                playerStats.AttackSpeed += modificator;
                textToShow = "Attack Speed";
                break;
            case Type.ProyectileSpeed:
                AudioManager.instance.Play("SpeedBuff");
                playerStats.ProyectileSpeed += modificator;
                textToShow = "Range Speed";
                break;
            case Type.ProyectileDamage:
                AudioManager.instance.Play("AttackBuff");
                playerStats.ProyectileDamage += modificator;
                textToShow = "Range Damage";
                break;
        }
        if (modificator > 0)
            textToShow += " Up";
        else
            textToShow += " Down";
        _buffCanvas.SetActive(true);
        _buffCanvas.GetComponent<TMPro.TMP_Text>().text = textToShow;
    }

    private IEnumerator TemporaryModif(float time, Type stat, float modificator)
    {
        SearchForStats(stat, modificator);
        yield return new WaitForSeconds(time);
        SearchForStats(stat, modificator*-1);
    }
}
