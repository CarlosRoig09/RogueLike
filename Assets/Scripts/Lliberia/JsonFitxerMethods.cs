using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonFitxerMethods
{
    public static string Path = Application.dataPath + "/Json";
    public static void CreateFiledJsonFile(string nameFitxer, int puntuation, int rooms, int enemy, string[] inventory, string[] modStats)
    {
        var newPlayer = new Player
        {
            Puntuation = puntuation,
            RoomsCompleated = rooms,
            EnemyDefeated = enemy,
            Inventory = inventory,
            ModStats = modStats
        };
        List<Player> players = new List<Player>();
        if (IfFileExists(nameFitxer))
        {
            players = SelectAll(nameFitxer);
        }
        players.Add(newPlayer);
        string[] playersString = new string[players.Count];
        using StreamWriter sw = File.CreateText(PathWithDir(Path, nameFitxer));
        for (int i = 0; i < playersString.Length; i++)
        {
            players[i].Id = i;
            playersString[i] = JsonConvert.SerializeObject(players[i]);
            sw.WriteLine(playersString[i]);
        }
    }

    public static List<Player> SelectAll(string nameFitxer)
    {

        List<Player> player1 = new();
        using (StreamReader jsonStream = File.OpenText(PathWithDir(Path,nameFitxer)))
        {
            string line;
            while ((line = jsonStream.ReadLine()) != null)
            {
                Player user = JsonConvert.DeserializeObject<Player>(line);
                player1.Add(user);
            }
        }
        return player1;
    }

    public static bool ComproveIfIsTheMaxPunt(int newPuntuation, string nameFitxer)
    {
        foreach (var played in SelectAll(nameFitxer))
        {
            if (newPuntuation < played.Puntuation)
                return false;
        }
            return true;
    }

    public static int ReturnBestPuntuation(string nameFitxer)
    {
        var bestPuntuation = 0;
        foreach (var played in SelectAll(nameFitxer))
        {
            if(played.Puntuation>bestPuntuation) bestPuntuation = played.Puntuation;
        }
        return bestPuntuation;
    }

    public static string PathWithDir(string path1, string dir)
    {
        path1 += "/" + dir;
        return path1;
    }
    public static bool IfFileExists(string file)
    {
        if (File.Exists(PathWithDir(Path,file)))
        {
            return true;
        }
        return false;
    }
}
