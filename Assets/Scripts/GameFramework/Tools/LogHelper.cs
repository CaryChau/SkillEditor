using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogHelper
{
    public static bool LogEnable { get; set; } = true;
    public static bool WarningEnable { get; set; } = true;
    public static bool ErrorEnable { get; set; } = true;

    public static void Print(string str, string stack = "")
    {
        if (LogEnable)
        {
            Debug.Log($"{str}\n{stack}");
        }
    }

    public static void PrintGreen(string str, string stack = "")
    {
        if (LogEnable)
        {
            Print($"<color=#008000>{str}\n{stack}</color>");
        }
    }

    public static void PrintRed(string str, string stack = "")
    {
        if (LogEnable)
        {
            Print($"<color=#FF0000>{str}\n{stack}</color>");
        }
    }

    public static void PrintYellow(string str, string stack = "")
    {
        if (LogEnable)
        {
            Print($"<color=#FFFF00>{str}\n{stack}</color>");
        }
    }

    public static void PrintWarning(string str, string stack = "")
    {
        if (WarningEnable)
        {
            Debug.LogWarning($"<color=#FFFF00>{str}\n{stack}</color>");
        }
    }

    public static void PrintError(string str, string stack = "")
    {
        if (ErrorEnable)
        {
            Debug.LogError($"<color=#FF0000>{str}\n{stack}</color>");
        }
    }
}
