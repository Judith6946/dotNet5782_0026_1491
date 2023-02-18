using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

internal static class Validation
{
    /// <summary>
    /// Check if a string is an email.
    /// </summary>
    /// <param name="s">String to be checked.</param>
    /// <returns>True if the string is an email and false otherwise.</returns>
    public static bool IsEmail(string? s)
    {
        if (s == null) return false;
        int t = 0, c = 0;
        for (int i = 0; i < s.Length; i++)
        {//בדיקה שאין אותיות בעברית
            if ((s[i] < 'א' || s[i] >= 'ת') && (s[i] == ' '))
                return false;
            if (s[i] == '@')
            {
                c++;
                if (c > 1)
                    return false;
            }

        }
        if (!s.Contains("@"))//@ בדיקה אם יש 
            return false;
        if (s.IndexOf('@') == 0)// לא ראשון @ בדיקה  
            return false;
        for (int i = s.IndexOf('@'); i < s.Length; i++)
        {
            if (s[i] == '.')
            {
                if (t == 0)
                {
                    t++;
                    if (s.IndexOf("@") + 1 >= i)//בדיקה שיש אחרי שטרודל נקודה אבל לא ברצף
                        return false;
                    if (i == s.Length - 1)//בדיקה שהנקודה לא אחרונה
                        return false;
                }
            }
        }
        if (t == 0)//בדיקה אם יש נקודה
            return false;
        return true;

    }

    /// <summary>
    /// Check if a string is a number
    /// </summary>
    /// <param name="s">String to be checked</param>
    /// <returns>True if the string is a number and false otherwise.</returns>
    public static bool IsNum(string s)
    {

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] < '0' || s[i] > '9')
                return false;
        }

        return true;
    }





    /// <summary>
    /// Check if a name (at least 3 chars)
    /// </summary>
    /// <param name="s">String to be checked</param>
    /// <returns>True if the string is at least 3 chars and false otherwise.</returns>
    public static bool IsName(string? s) => s != null && s.Length >= 3;


}
