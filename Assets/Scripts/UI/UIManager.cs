using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        string[] stats = { "MeleeDamgage : " + playerStats.Damage, "MeleeSpeed : " + playerStats.AttackSpeed, "ShootDamage : " + playerStats.ProyectileDamage, "ShootSpeed: " + playerStats.ProyectileSpeed, "PlayerSpeed : " + playerStats.Speed };
        
       JsonFitxerMethods.CreateFiledJsonFile("Ranking.json",GameManager.Instance.Puntuation, GameManager.Instance.Rooms, GameManager.Instance.NumberOfEnemyKilled,Weapons,stats);
        if(!JsonFitxerMethods.ComproveIfIsTheMaxPunt(GameManager.Instance.Puntuation,"Ranking.json"))
            _newRecordPanel.SetActive(false);
        _maxRecordText.text = "Score : " + JsonFitxerMethods.ReturnBestPuntuation("Ranking.json").ToString();
    }
}
