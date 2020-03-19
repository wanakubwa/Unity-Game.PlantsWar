using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class UnityExtensions
{ 
    public static string ToRGBHex(this Color c)
    {
        return string.Format("#{0:X2}{1:X2}{2:X2}", ToByte(c.r), ToByte(c.g), ToByte(c.b));
    }

    private static byte ToByte(float f)
    {
        f = Mathf.Clamp01(f);
        return (byte)(f * 255);
    }

    public static string SetColor(this string text, Color color)
    {
        string output;
        output = string.Format("<color={0}>{1}</color>", color.ToRGBHex(), text);
        return output;
    }
}
