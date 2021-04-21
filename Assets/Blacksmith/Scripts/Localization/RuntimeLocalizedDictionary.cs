using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Blacksmith
{
    using LocalizedEntries = Dictionary<string, Dictionary<string, string>>;

    public class RuntimeLocalizedDictionary
    {

        #region Events
        //PUBLIC
        //PROTECTED
        //PRIVATE
        #endregion

        #region Attributes
        //PUBLIC
        //PROTECTED
        //PRIVATE
        LocalizedEntries m_Entries;
        #endregion

        #region Methods
        //PUBLIC
        public void LoadFromText(string content)
        {
            LocalizationUtils.TryGetLocalizedEntriesFromJSONObject(JSONUtils.GetJSONFromString(content), out m_Entries, false);
        }

        public bool TryGetLocalizedTextFromID(string stringID, string languageID, out string value)
        {
            value = Constants.BLACKSMITH_INVALID_LOCALIZED_STRING_VALUE;
            if (m_Entries.ContainsKey(stringID) && m_Entries[stringID].ContainsKey(languageID) && m_Entries[stringID][languageID] != "")
            {
                value = m_Entries[stringID][languageID];
                return true;
            }
            return false;
        }

        //PROTECTED
        //PRIVATE
        #endregion
    }
}