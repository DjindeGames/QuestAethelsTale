using System;
using System.Collections.Generic;
using System.IO;

namespace Blacksmith
{
    using LocalizedEntries = Dictionary<string, Dictionary<string, string>>;

    public class LocalizationUtils
    {
        #region Methods
        //PUBLIC
        public static bool TryGetLocalizedEntriesFromJSONObject(JSONObject dictionary, out LocalizedEntries entries, bool safeMode = true)
        {
            entries = new LocalizedEntries();
            if (dictionary.Count > 0)
            {
                foreach (string stringID in dictionary.keys)
                {
                    if (!entries.ContainsKey(stringID))
                    {
                        Dictionary<string, string> valuesByLanguage = new Dictionary<string, string>();
                        foreach (string languageID in dictionary.GetField(stringID).keys)
                        {
                            if (!valuesByLanguage.ContainsKey(languageID))
                            {
                                valuesByLanguage[languageID] = dictionary.GetField(stringID).GetField(languageID).str;
                            }
                            else if (safeMode)
                            {
                                DebugUtils.LogError(null, "Found a duplicated language ID: " + DebugUtils.ToQuote(languageID) + " for string ID: " + DebugUtils.ToQuote(stringID) + "!");
                                return false;
                            }
                        }
                        entries[stringID] = valuesByLanguage;
                    }
                    else if (safeMode)
                    {
                        DebugUtils.LogError(null, "Found a duplicated string ID: " + DebugUtils.ToQuote(stringID) + "!");
                        return false;
                    }
                }
            }
            return true;
        }

        public static JSONObject GetJSONObjectFromLocalizedEntries(LocalizedEntries entries)
        {
            JSONObject jsonObject = new JSONObject();
            foreach (string stringID in entries.Keys)
            {
                JSONObject valuesByLanguage = new JSONObject();
                foreach (string languageID in entries[stringID].Keys)
                {
                    valuesByLanguage.AddField(languageID, entries[stringID][languageID]);
                }
                jsonObject.AddField(stringID, valuesByLanguage);
            }
            return jsonObject;
        }
        #endregion
    }
}