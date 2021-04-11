using UnityEngine;

namespace Blacksmith
{
    [CreateAssetMenu(fileName = "LocalizedString", menuName = "Blacksmith/Localization/LocalizedString")]
    public class LocalizedStringData : DBEntry
    {
        #region Serialized Fields
        //PUBLIC
        //PROTECTED
        //PRIVATE
        [SerializeField]
        private string m_StringID;
        [SerializeField]
        private string m_TempString = Constants.BLACKSMITH_UNSET_TEMP_STRING;
        #endregion

        #region Attributes
        //PUBLIC
        //PROTECTED
        //PRIVATE
        private bool m_LoggedWarning = false;
        private string m_CachedString = Constants.BLACKSMITH_INVALID_LOCALIZED_STRING_VALUE;
        #endregion

        #region Methods
        //PUBLIC
        public string GetID()
        {
            return m_StringID;
        }

        public string GetValue()
        {
            //String exists and is already cached, return it
            if (m_CachedString != Constants.BLACKSMITH_INVALID_LOCALIZED_STRING_VALUE)
            {
                return m_CachedString;
            }
            else if (LocalizationHelper.TryGetDictionaryHolder(out LocalizedDictionaryHolderComponent dictionaryHolder))
            {
                //String is not yet cached
                if (dictionaryHolder.TryGetLocalizedStringFromID(m_StringID, out m_CachedString))
                {
                    return m_CachedString;
                }
            }
            if (!m_LoggedWarning)
            {
                m_LoggedWarning = true;
                DebugUtils.LogLocalizationWarning(m_StringID);
            }
            //String is not available, use temporary string instead
            if (m_TempString == "")
            {
                DebugUtils.LogLocalizationWarning(m_StringID, "Temporary string is not set!");
            }
            return m_TempString;
        }

        public string GetLocalizedValue() { return ""; }
        //PROTECTED
        //PRIVATE
        #endregion
    }
}