using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class JsonFitxerMethods
{
    public static string Path = Application.dataPath + "/StreamingAssets/";
    public static void CreateFiledJsonFile(string nameFitxer, int id, string userName, int puntuation, int rooms, int enemy, string[] inventory, int[] modStats)
    {
        var player = new Player
        {
            Id = id,
            Name = userName,
            Puntuation = puntuation,
            RoomsCompleated = rooms,
            EnemyDefeated = enemy,
            Inventory = inventory,
            ModStats = modStats
        };

        string json1 = JsonConvert.SerializeObject(player);

        using (StreamWriter sw = new(PathWithDir(Path,nameFitxer)))
         sw.WriteLine(json1);
    }

    public static void AddTextToJsonFile(string nameFitxer, int id, string userName, int puntuation, int rooms, int enemy, string[] inventory, int[] modStats)
    {
        var player = new Player
        {
            Id = id,
            Name = userName,
            Puntuation = puntuation,
            RoomsCompleated = rooms,
            EnemyDefeated = enemy,
            Inventory = inventory,
            ModStats = modStats
        };

        string json1 = JsonConvert.SerializeObject(player);
        File.AppendAllText(PathWithDir(Path,nameFitxer), json1 + "\n");
    }

    public static string PathWithDir(string path1, string dir)
    {
        path1 += "/" + dir;
        return path1;
    }
}
