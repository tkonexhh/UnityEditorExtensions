using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtension
{
    /// <summary>
    /// 差不多是黑色
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool IsApproximatelyBlack(this Color self)
    {
        return self.r + self.g + self.b <= Mathf.Epsilon;
    }
    public static bool IsApproximatelyWhite(this Color self)
    {
        return self.r + self.g + self.b >= 1 - Mathf.Epsilon;
    }
    public static Color Invert(this Color self)
    {
        self.r = 1 - self.r;
        self.g = 1 - self.g;
        self.b = 1 - self.b;
        self.a = 1 - self.a;
        return self;
    }

    public static Color From255(this Color self, float r, float g, float b, float a = 255)
    {
        self.r = r / 255f;
        self.g = g / 255f;
        self.b = b / 255f;
        self.a = a / 255f;
        return self;
    }

    public static Color FromHex(this Color self, string hexValue, float alpha = 1)
    {
        if (string.IsNullOrEmpty(hexValue)) return Color.clear;
        if (hexValue[0] == '#') hexValue = hexValue.TrimStart('#');
        if (hexValue.Length > 6) hexValue = hexValue.Remove(6, hexValue.Length - 6);
        int value = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
        int r = value >> 16 & 255;
        int g = value >> 8 & 255;
        int b = value & 255;
        float a = 255 * alpha;
        return self.From255(r, g, b, a);
    }
}