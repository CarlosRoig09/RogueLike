using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomMethods
{
    public static int ReturnARandomObject(ItemData[] SO, float dropNothingChane, int max, int min)
    {
        float minRange = 0;
        float maxRange = 0;
        var random = Random.Range(minRange, SetMaxValueOfRandom(SO) + dropNothingChane);
        for (var i = min; i < max; i++)
        {
            //Debug.Log(random);
            if (random >= minRange && random <= (maxRange += SO[i].RateAperance / SO.Length))
            {
                return i;
            }
            else
                minRange = maxRange;
        }
        return -1;
    }

    public static float SetMaxValueOfRandom(ItemData[] SO)
    {
        float totalValue = 0;
        foreach (var Rate in SO)
        {
            totalValue += Rate.RateAperance / SO.Length;
        }
        return totalValue;
    }
}
