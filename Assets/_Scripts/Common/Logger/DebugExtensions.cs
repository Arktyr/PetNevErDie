using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = System.Object;

namespace _Scripts.Common.Logger
{
    public static class DebugExtensions
    {
        public static void LogDetailed(Object obj, string label)
        {
            var message = FormatObject(obj);
            
            if (!string.IsNullOrEmpty(label))
                message = $"<b>{label}</b>:\n{message}";

            Debug.Log(message);
        }

        public static void LogErrorDetailed(Object obj, string label)
        {
            var message = FormatObject(obj);
            
            if (!string.IsNullOrEmpty(label))
                message = $"<b>{label}</b>:\n{message}";

            Debug.LogError(message);
        }

        private static string FormatObject(object obj, int indent = 0)
        {
            if (obj == null)
                return "<color=grey>null</color>";

            var type = obj.GetType();
            var sb = new StringBuilder();
            
            if (type.IsPrimitive || obj is decimal)
                return FormatPrimitive(obj, type, indent);

            switch (obj)
            {
                case Enum:
                    return $"{Indent(indent)}<color=#ffa500>{obj} ({Convert.ToInt32(obj)})</color>";
                case string str:
                    return $"\"{str}\"";
                case IDictionary dictionary:
                {
                    sb.AppendLine($"{Indent(indent)}<color=yellow>{type.Name}</color> ({dictionary.Count} items):");
                    foreach (DictionaryEntry entry in dictionary)
                        sb.AppendLine($"{Indent(indent + 1)}• <b>{entry.Key}</b>: {FormatObject(entry.Value, indent + 2)}");
                    break;
                }
                case IEnumerable enumerable:
                {
                    var list = enumerable.Cast<object>().ToList();
                    sb.AppendLine($"{Indent(indent)}<color=cyan>{type.Name}</color> [{list.Count} items]:");
                    for (int i = 0; i < list.Count; i++)
                        sb.AppendLine($"{Indent(indent + 1)}[{i}] {FormatObject(list[i], indent + 2)}");
                    break;
                }
                case UnityEngine.Object unityObj:
                    return $"<color=green>{unityObj.name}</color> ({type.Name})";
                default:
                    sb.AppendLine($"{Indent(indent)}{obj}");
                    break;
            }

            return sb.ToString();
        }
        
        private static string FormatPrimitive(object value, Type type, int indent)
        {
            string color = type switch
            {
                _ when type == typeof(int) || type == typeof(long) => "#00ff00", // зелёный
                _ when type == typeof(float) || type == typeof(double) => "#33ccff", // голубой
                _ when type == typeof(bool) => ((bool)value) ? "#00ffcc" : "#ff4444",
                _ => "#ffffff"
            };

            return $"{Indent(indent)}<color={color}>{value}</color>";
        }
        
        private static string Indent(int level) => 
            new(' ', level * 2);
    }
}