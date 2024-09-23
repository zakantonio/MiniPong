using UnityEngine;
using UnityEngine.Diagnostics;

static class Utils
{
    public static int CoinFlip()
    {
        return (Random.Range(0, 2) == 0) ? -1 : 1;
    }
}