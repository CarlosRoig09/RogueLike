using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonFitxerMethods
{
    public static string Dir = Application.streamingAssetsPath;
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
        List<Player> players = new();
        if (IfFileExists(nameFitxer))
        {
            players = SelectAll(Path.Combine(Dir, nameFitxer));
        }
        players.Add(newPlayer);
        string[] playersString = new string[players.Count];
        using StreamWriter sw = File.CreateText(Path.Combine(Dir, nameFitxer));
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
        if (IfFileExists(Path.Combine(Dir, nameFitxer)))
        {
            using (StreamReader jsonStream = File.OpenText(Path.Combine(Dir, nameFitxer)))
            {
                string line;
                while ((line = jsonStream.ReadLine()) != null)
                {
                    Player user = JsonConvert.DeserializeObject<Player>(line);
                    player1.Add(user);
                }
            }
        }
        return player1;
    }
    public static Player ReturnLastPlayer(string nameFitxer)
    {
        if (IfFileExists(Path.Combine(Dir, nameFitxer)))
        {
            var players = SelectAll(Path.Combine(Dir, nameFitxer));
            return players[^1];
        }
        return null;
    }
    public static bool ComproveIfIsTheMaxPunt(int newPuntuation, string nameFitxer)
    {
        if (IfFileExists(Path.Combine(Dir, nameFitxer)))
        {
            foreach (var played in SelectAll(Path.Combine(Dir, nameFitxer)))
            {
                if (newPuntuation < played.Puntuation)
                    return false;
            }
        }
            return true;
    }

    public static int ReturnBestPuntuation(string nameFitxer)
    {
        var bestPuntuation = 0;
        if (IfFileExists(Path.Combine(Dir, nameFitxer)))
        {
            foreach (var played in SelectAll(Path.Combine(Dir, nameFitxer)))
            {
                if (played.Puntuation > bestPuntuation) bestPuntuation = played.Puntuation;
            }
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
        if (File.Exists(file))
        {
            return true;
        }
        return false;
    }
}
