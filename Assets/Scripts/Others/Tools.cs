using UnityEngine;
using System.Collections;

public static class Tools
{
    public static bool throwOfDice(int percentage)
    {
        if (Random.Range(0, 100) < percentage)
            return true;
        return false;
    }
}
