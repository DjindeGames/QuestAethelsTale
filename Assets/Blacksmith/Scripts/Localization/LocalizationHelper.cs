namespace Blacksmith
{
    public class LocalizationHelper
    {
        #region Methods
        //PUBLIC
        public static LocalizedDictionaryHolderComponent GetDictionaryHolder()
        {
            if (LocalizationManagerComponent.TryGetInstance(out LocalizationManagerComponent instance))
            {
                if (instance.TryGetFakeSingleton(out LocalizedDictionaryHolderComponent dictionaryHolder))
                {
                    return dictionaryHolder;
                }
            }
            return null;
        }

        public static bool TryGetDictionaryHolder(out LocalizedDictionaryHolderComponent dictionaryHolder)
        {
            dictionaryHolder = GetDictionaryHolder();
            return (dictionaryHolder != null);
        }
        //PROTECTED
        //PRIVATE
        #endregion
    }
}