using System;
using UnityEngine;

namespace Blacksmith
{
    public class ObjectUtils
    {
        #region Methods
        //PUBLIC
        public static bool TryCast<T>(object obj, out T result)
        {
            result = default(T);
            try
            {
                result = (T)obj;
                return true;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        public static bool GetComponent<T>(GameObject obj, out T result)
        {
            result = obj.GetComponent<T>();
            return (result != null);
        }

        public static bool HasFlag(GameObject obj, EBaseFlags flag)
        {
            if (TryCast<BaseComponent>(obj, out BaseComponent baseComponent))
            {
                return baseComponent.HasFlag(flag);
            }
            return false;
        }
        #endregion
    }
}