using System;
using UnityEngine;
using TMPro;
using System.IO;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Inventory playerInventory;
    [SerializeField]
    private PlayerStats playerStats;
    private GameObject _newRecordPanel;
    private TMP_Text _maxRecordText;
    void Start()
    {
        _newRecordPanel = GameObject.Find("NewRecord");
        _maxRecordText = GameObject.Find("MaxPuntuation").GetComponent<TMP_Text>();
        string[] Weapons = Array.Empty<string>();
        foreach (var weapon in playerInventory.Weapons)
        {
           Array.Resize(ref Weapons, Weapons.Length+1);
            Weapons[^1] = weapon.Name;
        }
        string[] stats = { playerStats.Damage.ToString(), playerStats.AttackSpeed.ToString(), playerStats.ProyectileDamage.ToString(), playerStats.ProyectileSpeed.ToString(), playerStats.Speed.ToString()};
        
       JsonFitxerMethods.CreateFiledJsonFile("Ranking.json",GameManager.Instance.Puntuation, GameManager.Instance.Rooms, GameManager.Instance.NumberOfEnemyKilled,Weapons,stats);
        if(!JsonFitxerMethods.ComproveIfIsTheMaxPunt(GameManager.Instance.Puntuation,"Ranking.json"))
            _newRecordPanel.SetActive(false);
        _maxRecordText.text = "Score : " + JsonFitxerMethods.ReturnBestPuntuation("Ranking.json").ToString();
    }
}
