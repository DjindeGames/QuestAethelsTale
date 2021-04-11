using System;

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
        #endregion
    }
}