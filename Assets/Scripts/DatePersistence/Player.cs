using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Puntuation { get; set; }
    public int RoomsCompleated { get; set; }
    public int EnemyDefeated { get; set; }
    public string[] Inventory { get; set;}
    public int[] ModStats { get; set; }
}
