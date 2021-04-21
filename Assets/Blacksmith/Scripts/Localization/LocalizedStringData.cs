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
        private string m_TempString;
        #endregion

        #region Attributes
        //PUBLIC
        //PROTECTED
        //PRIVATE
        #endregion

        #region Methods
        //PUBLIC
        public string GetID()
        {
            return m_StringID;
        }

        public string GetValue()
        {
            string value = "";
            if (LocalizationHelper.TryGetDictionaryHolder(out LocalizedDictionaryHolderComponent dictionaryHolder))
            {
                if (dictionaryHolder.TryGetLocalizedStringFromID(m_StringID, out value))
                {
                    return value;
                }
                else
                {
                    DebugUtils.LogLocalizationWarning(m_StringID);
                }
            }
            //String is not available, use temporary string instead
            if (m_TempString == "")
            {
                DebugUtils.LogLocalizationWarning(m_StringID, "Temporary string is not set!");
                value = "!" + m_StringID;
            }
            else
            {
                value = "!" + m_TempString;
            }
            return value;
        }
        //PROTECTED
        //PRIVATE
        #endregion
    }
}