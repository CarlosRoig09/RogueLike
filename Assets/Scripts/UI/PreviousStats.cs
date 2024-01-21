using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class PreviousStats : MonoBehaviour
{
    [SerializeField]
    private TMP_Text puntuation;
    [SerializeField]
    private TMP_Text Rooms;
    [SerializeField]
    private TMP_Text EnemyDefeated;
    [SerializeField]
    private GameObject[] IconWeapon;
    [SerializeField]
    private GameObject[] Stats;
    [SerializeField]
    private Sprite[] _weaponImages;
    // Start is called before the first frame update
    void Start()
    {
        var player = JsonFitxerMethods.ReturnLastPlayer("Ranking.json");
        PutStatsLastGame(player);
        StatsPlayer(player);
        WeaponsUsed(player);
    }

    private void PutStatsLastGame(Player player)
    {
        if (player != null)
        {
            puntuation.text = "Puntuation : " + player.Puntuation.ToString();
            Rooms.text = "Rooms : " + player.RoomsCompleated.ToString();
            EnemyDefeated.text = "Enemies : " + player.EnemyDefeated.ToString();
        }
    }

    private void StatsPlayer(Player player)
    {
        if (player != null)
        {
            Stats[0].transform.GetChild(0).GetComponent<TMP_Text>().text = "x" + player.ModStats[0];
            Stats[1].transform.GetChild(0).GetComponent<TMP_Text>().text = "x" + player.ModStats[1];
            Stats[2].transform.GetChild(0).GetComponent<TMP_Text>().text = "x" + player.ModStats[2];
            Stats[3].transform.GetChild(0).GetComponent<TMP_Text>().text = "x" + player.ModStats[3];
            Stats[4].transform.GetChild(0).GetComponent<TMP_Text>().text = "x" + player.ModStats[4];
        }
    }

    private void WeaponsUsed(Player player)
    {
        if (player != null)
        {
            for (int i = 0; i < player.Inventory.Length; i++)
            {
                IconWeapon[i].transform.GetChild(0).GetComponent<TMP_Text>().text = player.Inventory[i];
                IconWeapon[i].transform.GetChild(1).GetComponent<Image>().sprite = RelationWeaponAndSprite(player.Inventory[i]);
            }
        }
    }

    private Sprite RelationWeaponAndSprite(string weapon)
    {
        switch (weapon)
        {
            case "Sword":
                return _weaponImages[0];
            case "Saber":
                return _weaponImages[1];
            case "Rapier":
                return _weaponImages[2];
            case "Scythe":
                return _weaponImages[3];
            case "Axe":
                return _weaponImages[4];
            case "Bow":
                return _weaponImages[5];
            case "Lance":
                return _weaponImages[6];
            default
                : return _weaponImages[0];
        }
    }
}
