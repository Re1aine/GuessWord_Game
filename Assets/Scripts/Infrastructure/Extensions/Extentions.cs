using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public static class Extentions
{
    public static char GetRandomChar(List<char> list)
    {
        return list[new Random().Next(0, list.Count)];
    }

    public static List<char> GetRandomCharThenDelete(List<char> list)
    {
        list.RemoveAt(new Random().Next(list.Count));
        return list;
    }

    public static char GetRandomAlphabetLetter()
    {
        return (char)('A' + new Random().Next(26));
    }

    public static IList<T> Shuffle<T>(this IList<T> list)
    {
        Random random = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }
}