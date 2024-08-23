using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public static class ColorizedHelper
{
    /// <summary>
    /// Display log with colors (HEX, RGB(float, int), Color)
    /// </summary>
    /// <param name="inputs">(string,color) params</param>
    public static void ColorizeLog(params object[] inputs)
    {
        Debug.LogFormat(ColorizeText(inputs));
    }

    /// <summary>
    /// Colorize text with colors (HEX, RGB(float, int), Color)
    /// </summary>
    /// <param name="inputs">(string,color) params</param>
    /// <returns>Colorized text</returns>
    public static string ColorizeText(params object[] inputs)
    {
        string logText = "";
        foreach (object input in inputs)
        {
            logText += input.IsConvertibleTo<ITuple>(true) ? AddColorLog(input.ConvertTo<ITuple>()) : input;
        }
        return logText;
    }

    private static string AddColorLog(ITuple tuple)
    {
        return string.Format("<color={1}>{0}</color>", tuple[0], GetColor(tuple[1]));
    }

    #region ConvertToHex
    private static string ConvertToHex(int r, int g, int b)
    {
        return "#" + string.Format("{0:X2}{1:X2}{2:X2}", (byte)r, (byte)g, (byte)b);
    }

    private static string ConvertToHex(float r, float g, float b)
    {
        return "#" + string.Format("{0:X2}{1:X2}{2:X2}",(byte)(r * 255) , (byte)(g * 255) , (byte)(b * 255));
    }

    private static string ConvertToHex(Color color)
    {
        return ConvertToHex(color.r,color.g, color.b);
    }

    private static string ConvertToHex(string hexColor)
    {
        return hexColor.Contains("#") ? hexColor : "#" + hexColor;
    }

    private static string ConvertToHex(ITuple tuple)
    {
        return tuple[0].GetType() == typeof(Int32)? 
            ConvertToHex((int)tuple[0], (int)tuple[1], (int)tuple[2]): 
            tuple[0].GetType() == typeof(Single)? 
                ConvertToHex((float)tuple[0], (float)tuple[1], (float)tuple[2]):
                "#C0C0C0";
    }

    //TODO add to double
    #endregion

    private static string GetColor(object colorTuple)
    {
        string returnLogPart;
        switch(colorTuple)
        {
            case (string):
                {
                    returnLogPart = ConvertToHex((string)colorTuple);
                    break;
                }
            case (Color):
                {
                    returnLogPart = ConvertToHex((Color)colorTuple);
                    break;
                }
            case (ITuple):
                {
                    returnLogPart = ConvertToHex((ITuple)colorTuple);
                    break;
                }
            case (int):
                {
                    returnLogPart = "#C0C0C0";
                    break;
                }
            case (float):
                {
                    returnLogPart = "#C0C0C0";
                    break;
                }
            default:
                {
                    returnLogPart = "#C0C0C0";
                    break;
                }
        }
        return returnLogPart;
    }
}