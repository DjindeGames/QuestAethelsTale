using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Blacksmith
{
    using LocalizedEntries = Dictionary<string, Dictionary<string, string>>;

    [CreateAssetMenu(fileName = "LocalizedDictionary", menuName = "Blacksmith/Localization/Dictionary")]
    public class LocalizedDictionaryData : DBEntry
    {
        #region Serialized Fields
        //PUBLIC
        [BoxGroup("Content")]
        public string[] m_SupportedLanguages;
        [BoxGroup("Content")]
        public LocalizedStringData[] m_Strings;
#if UNITY_EDITOR
        [BoxGroup("Generation")]
        public string m_FilePath;
        [Button("GenerateDictionary")]
        public void GenerateDictionary()
        {
            try
            {
                JSONObject dictionaryContent = new JSONObject(System.IO.File.ReadAllText(m_FilePath));
                UpdateDictionary(dictionaryContent);

            }
            catch (Exception exception) when (
                exception is FileNotFoundException || 
                exception is DirectoryNotFoundException
                )

            {
                DebugUtils.LogWarning(this, "File does not exists, creating a new dictionary file.");
                System.IO.File.WriteAllText(m_FilePath, "");
                JSONObject dictionaryContent = new JSONObject(System.IO.File.ReadAllText(m_FilePath));
                UpdateDictionary(dictionaryContent);
            }
        }

        private void UpdateDictionary(JSONObject dictionary)
        {
            if (m_SupportedLanguages.Length == 0)
            {
                DebugUtils.LogWarning(this, "No supported languages, aborting generation!");
                return;
            }

            List<string> supportedLanguages = new List<string>();
            foreach(string language in m_SupportedLanguages)
            {
                if (!supportedLanguages.Contains(language))
                {
                    supportedLanguages.Add(language);
                }
                else
                {
                    DebugUtils.LogError(this, "Found a duplicated language ID: " + DebugUtils.ToQuote(language) + " in supported languages, aborting generation!");
                }
            }

            //Start by parsing existing content from file
            LocalizedEntries dictionaryEntries = new LocalizedEntries();

            foreach (string stringID in dictionary.ToDictionary().Keys)
            {
                if (!dictionaryEntries.ContainsKey(stringID))
                {
                    Dictionary<string, string> valuesByLanguage = new Dictionary<string, string>();
                    foreach (string languageID in dictionary.GetField(stringID).ToDictionary().Keys)
                    {
                        if (!valuesByLanguage.ContainsKey(languageID))
                        {
                            valuesByLanguage[languageID] = dictionary.GetField(stringID).GetField(languageID).ToString();
                            if (!IsLanguageSupported(languageID))
                            {
                                DebugUtils.LogWarning(this, "Found an unsupported language ID: " + DebugUtils.ToQuote(languageID) + " for string ID: " + DebugUtils.ToQuote(stringID) + ", consider removing this entry.");
                            }
                        }
                        //Duplicated language, abort mission!
                        else
                        {
                            DebugUtils.LogError(this, "Found a duplicated language ID: " + DebugUtils.ToQuote(languageID) + " for string ID: " + DebugUtils.ToQuote(stringID) + ", aborting generation!");
                            return;
                        }
                    }
                    //Adding new supported languages
                    foreach(string languageID in m_SupportedLanguages)
                    {
                        if (!valuesByLanguage.ContainsKey(languageID))
                        {
                            valuesByLanguage[languageID] = Constants.BLACKSMITH_INVALID_LOCALIZED_STRING_VALUE;
                            DebugUtils.Log(this, DebugUtils.ToQuote(languageID) + " language entry added for string ID: " + DebugUtils.ToQuote(stringID) + ".");
                        }
                    }
                    dictionaryEntries[stringID] = valuesByLanguage;
                }
                //Duplicated string ID, abort mission!
                else
                {
                    DebugUtils.LogError(this, "Found a duplicated string ID: " + DebugUtils.ToQuote(stringID) + ", aborting generation!");
                    return;
                }
            }
            //Adding new string IDs
            foreach (LocalizedStringData stringData in m_Strings)
            {
                if (!dictionaryEntries.ContainsKey(stringData.GetID()))
                {
                    if (stringData.GetID() != "")
                    {
                        Dictionary<string, string> valuesByLanguage = new Dictionary<string, string>();
                        foreach (string language in m_SupportedLanguages)
                        {
                            valuesByLanguage[language] = Constants.BLACKSMITH_INVALID_LOCALIZED_STRING_VALUE;
                        }
                        dictionaryEntries[stringData.GetID()] = valuesByLanguage;
                    }
                    else
                    {
                        DebugUtils.LogError(this, "Localized string \"" + stringData.name + "\" has no defined ID, aborting generation!");
                        return;
                    }
                }
            }

            //Write dictionary back to file
            JSONObject updatedDictionary = new JSONObject();

            foreach(string stringID in dictionaryEntries.Keys)
            {
                JSONObject valuesByLanguage = new JSONObject();
                foreach (string languageID in dictionaryEntries[stringID].Keys)
                {
                    valuesByLanguage.AddField(languageID, dictionaryEntries[stringID][languageID]);
                }
                updatedDictionary.AddField(stringID, valuesByLanguage);
            }

            System.IO.File.WriteAllText(m_FilePath, updatedDictionary.Print(true));
        }

        private bool IsLanguageSupported(string languageID)
        {
            foreach (string supportedLanguage in m_SupportedLanguages)
            {
                if (supportedLanguage == languageID)
                {
                    return true;
                }
            }
            return false;
        }
#endif
        #endregion

        #region Methods

        #endregion
    }
}