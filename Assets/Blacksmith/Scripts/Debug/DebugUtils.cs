using UnityEngine;

namespace Blacksmith
{
    public class DebugUtils
    {
        #region Methods
        //PUBLIC
        public static string GetObjectType(object obj)
        {
            return (obj != null) ? obj.GetType().ToString() : "";
        }

        public static string ToQuote(string quote)
        {
            return "\"" + quote + "\"";
        }

        public static void LogError(object obj, string desc, bool printCallstack = false)
        {
            string log;

            if (obj != null && ObjectUtils.TryCast(obj, out MonoBehaviour mbObject))
            {
                log = "[" + mbObject.name + "] Error in script \"" + GetObjectType(obj) + "\": " + desc;
            }
            else if (obj != null && (ObjectUtils.TryCast(obj, out ScriptableObject soObject)))
            {
                log = "[" + soObject.name + "] Error in script \"" + GetObjectType(obj) + "\": " + desc;
            }
            else
            {
                log = "Error in script \"" + GetObjectType(obj) + "\": " + desc;
            }

            Debug.Log("<color=red><b>" + log + "</b></color>");
#if !UNITY_EDITOR
            if (DebugHelper.TryGetDebugLogger(out DebugLoggerComponent debugLogger))
            {
                debugLogger.RegisterLog(log, EDebugLogType.Error);
            }
#endif
        }

        public static void LogWarning(object obj, string desc, bool printCallstack = false)
        {
            string log;

            if (obj != null && ObjectUtils.TryCast(obj, out MonoBehaviour mbObject))
            {
                log = "[" + mbObject.name + "] Warning in script \"" + GetObjectType(obj) + "\": " + desc;
            }
            else if (obj != null && (ObjectUtils.TryCast(obj, out ScriptableObject soObject)))
            {
                log = "[" + soObject.name + "] Warning in script \"" + GetObjectType(obj) + "\": " + desc;
            }
            else
            {
                log = "Warning in script \"" + GetObjectType(obj) + "\": " + desc;
            }

            Debug.Log("<color=yellow><b>" + log + "</b></color>");

#if !UNITY_EDITOR
            if (DebugHelper.TryGetDebugLogger(out DebugLoggerComponent debugLogger))
            {
                debugLogger.RegisterLog(log, EDebugLogType.Warning);
            }
#endif
        }

        public static void LogSuccess(object obj, string desc)
        {
            if (obj != null && (ObjectUtils.TryCast(obj, out MonoBehaviour mbObject)))
            {
                Debug.Log("<color=green>[" + mbObject.name + "]\"" + GetObjectType(obj) + "\": " + desc + "</color>");
            }
            else if (obj != null && (ObjectUtils.TryCast(obj, out ScriptableObject soObject)))
            {
                Debug.Log("<color=green>[" + soObject.name + "]\"" + GetObjectType(soObject) + "\": " + desc + "</color>");
            }
            else
            {
                Debug.Log("<color=green>\"" + GetObjectType(obj) + "\": " + desc + "</color>");
            }
        }

        public static void LogLocalizationWarning(string ID)
        {
            string log = "Localized value for \"" + ID + "\" was not found in LocalizationDictionary, will use temporary string instead.";
            Debug.Log("<color=purple>" + log + "</color>");
#if !UNITY_EDITOR
            if (DebugHelper.TryGetDebugLogger(out DebugLoggerComponent debugLogger))
            {
                debugLogger.RegisterLog(log, EDebugLogType.LocalizationWarning);
            }
#endif
        }

        public static void LogLocalizationWarning(string ID, string desc)
        {
            string log = "\"" + ID + "\" " + desc;
            Debug.Log("<color=purple>" + log + "</color>");
#if !UNITY_EDITOR
            if (DebugHelper.TryGetDebugLogger(out DebugLoggerComponent debugLogger))
            {
                debugLogger.RegisterLog(log, EDebugLogType.LocalizationWarning);
            }
#endif
        }

        public static void Log(object obj, string desc)
        {
            if (obj != null && ObjectUtils.TryCast(obj, out MonoBehaviour mbObject))
            {
                Debug.Log("[" + mbObject.name + "]\"" + GetObjectType(obj) + "\": " + desc);
            }
            else if (obj != null && (ObjectUtils.TryCast(obj, out ScriptableObject soObject)))
            {
                Debug.Log("[" + soObject.name + "]\"" + GetObjectType(soObject) + "\": " + desc);
            }
            else
            {
                Debug.Log("\"" + GetObjectType(obj) + "\": " + desc);
            }
        }
#endregion
    }
}