using UnityEngine;

namespace Blacksmith
{
    public class DebugUtils
    {
        #region Methods
        //PUBLIC
        public static void LogError(object obj, string desc)
        {
            if (ObjectUtils.TryCast<MonoBehaviour>(obj, out MonoBehaviour mbObject))
            {
                Debug.Log("<color=red><b>[" + mbObject.name + "]" + " Error in script \"" + obj.GetType().ToString() + "\": " + desc + "</b></color>");
            }
            else
            {
                Debug.Log("<color=red><b> Error in script \"" + obj.GetType().ToString() + "\": " + desc + "</b></color>");
            }
        }

        public static void LogWarning(object obj, string desc)
        {
            if (ObjectUtils.TryCast<MonoBehaviour>(obj, out MonoBehaviour mbObject))
            {
                Debug.Log("<color=yellow><b>[" + mbObject.name + "]" + " Warning in script \"" + obj.GetType().ToString() + "\": " + desc + "</b></color>");
            }
            else
            {
                Debug.Log("<color=yellow> Warning in script \"" + obj.GetType().ToString() + "\": " + desc + "</b></color>");
            }
        }

        public static void LogSuccess(object obj, string desc)
        {
            if (ObjectUtils.TryCast<MonoBehaviour>(obj, out MonoBehaviour mbObject))
            {
                Debug.Log("<color=green>[" + mbObject.name + "]\"" + obj.GetType().ToString() + "\": " + desc + "</color>");
            }
            else
            {
                Debug.Log("<color=green>\"" + obj.GetType().ToString() + "\": " + desc + "</color>");
            }
        }

        public static void LogLocalizationWarning(string ID)
        {
            Debug.Log("<color=purple>\"" + ID + "\" was not found in LocalizationDictionary, will use temporary string instead.</color>");
        }

        public static void Log(object obj, string desc)
        {
            if (ObjectUtils.TryCast<MonoBehaviour>(obj, out MonoBehaviour mbObject))
            {
                Debug.Log("[" + mbObject.name + "]\"" + obj.GetType().ToString() + "\": " + desc);
            }
            else
            {
                Debug.Log("\"" + obj.GetType().ToString() + "\": " + desc);
            }
        }
        #endregion
    }
}