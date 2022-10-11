


using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using YG;

public class CSVManager
{
    public static readonly string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    // Чтение CSV файла. Поиск переводов по ключу
    public static string[] ImportTransfersByKey(string CSVName, int languagesCount, string key)
    {
        TextAsset data = Resources.Load(CSVName) as TextAsset;

        string[] keys = Regex.Split(CommaFormat(data.text), LINE_SPLIT_RE);
        string[] result = new string[languagesCount];

        bool complete = false;

        for (int i = 1; i < keys.Length; i++)
        {
            string[] translates = Regex.Split(keys[i], ",");
            if (translates[0] == key)
            {
                for (int i2 = 0; i2 < languagesCount; i2++)
                {
                    result[i2] = translates[i2 + 1].Replace("*", ",").Replace(@"\n", "\n");
                    complete = true;
                }
            }
        }

        if (complete)
            return result;
        else
        {
            Debug.LogWarning("(en) Couldn't find a translation for this object! (ru) Не удалось найти перевод для данного объекта!");
            return null;
        }
    }

    public static string CommaFormat(string line)
    {
        return AsteriskFormat(line.Replace(";", ","));
    }

    static string[] CommaFormat(string[] lines)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = CommaFormat(lines[i]);
        }

        return lines;
    }

    static string SemicolonFormat(string line)
    {
        if (line.Length > 0)
            return line.Replace(",", ";");
        else
            return "";
    }

    static string AsteriskFormat(string line)
    {
        if (line.Length > 0)
            return line.Replace(", ", "* ");
        else
            return "";
    }

    static string RedLineFormat(string line)
    {
        if (line.Length > 0)
            return line.Replace("\n", @"\n");
        else
            return "";
    }
}