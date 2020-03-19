using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class UnityExtensions
{
    public static string SetColor(this string text, Color color)
    {
        string output;
        output = string.Format("<color = {0}>{1}</color>", color, text);
        return output;
    }
}
