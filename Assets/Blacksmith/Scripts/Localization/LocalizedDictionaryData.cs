using NaughtyAttributes;
using System.Collections.Generic;
using UnityEditor;
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
        [Header("Generation")]
        public string m_FilePath;
        #endregion

#if UNITY_EDITOR
        #region Methods
        [Button("Generate Dictionary")]
        public void ButtonGenerateDictionary()
        {
            GenerateDictionary();
        }

        [Button("Backup Dictionary")]
        public void ButtonBackupDictionary()
        {
            if (BackupDictionary())
            {
                DebugUtils.LogSuccess(this, "Successfully backuped dictionary.");
            }
            else
            {
                DebugUtils.LogWarning(this, "Could not backup dictionary.");
            }
        }

        [Button("Clean Dictionary")]
        public void ButtonCleanDictionary()
        {
            if (EditorUtility.DisplayDialog("Warning", "This will remove all unsuported entries from the following dictionary:\n" + m_FilePath + "\nAre you sure?", "Confirm (No turning back!)", "Cancel"))
            {
                GenerateDictionary(false);
            }
        }

        [Button("Delete Dictionary")]
        public void ButtonDeleteDictionary()
        {
            if (EditorUtility.DisplayDialog("Warning", "Are you sure you want to delete the following dictionary?\n" + m_FilePath, "Confirm (No turning back!)", "Cancel"))
            {
                if (BackupDictionary())
                {
                    FileUtils.DeleteFile(m_FilePath, true);
                }
                else
                {
                    DebugUtils.LogError(this, "Could not backup dictionary, aborting deletion!");
                }
            }
        }

        [Button("Delete Backup")]
        public void ButtonDeleteBackup()
        {
            if (EditorUtility.DisplayDialog("Warning", "Are you sure you want to delete existing backup for the following dictionary?\n" + m_FilePath, "Confirm (No turning back!)", "Cancel"))
            {
                DeleteBackup();
            }
        }

        private void GenerateDictionary(bool safeMode = true)
        {
            if (m_SupportedLanguages.Length == 0)
            {
                DebugUtils.LogError(this, "No supported languages, aborting generation!");
                return;
            }

            if (HasDuplicatedLanguages(out string duplicatedLanguage))
            {
                DebugUtils.LogError(this, "Found a duplicated language ID: " + DebugUtils.ToQuote(duplicatedLanguage) + " in supported languages, aborting generation!");
                return;
            }

            if (HasDuplicatedStrings(out string duplicatedString))
            {
                DebugUtils.LogError(this, "Found a duplicated string ID: " + DebugUtils.ToQuote(duplicatedString) + " in registered strings, aborting generation!");
                return;
            }

            if (JSONUtils.TryLoadFromPath(m_FilePath, out JSONObject dictionary, true))
            {
                //Start by parsing existing content from file
                if (LocalizationUtils.TryGetLocalizedEntriesFromJSONObject(dictionary, out LocalizedEntries dictionaryEntries, safeMode))
                {
                    //Adding new languages for existing strings
                    List<string> stringIDsToRemove = new List<string>();
                    foreach (string stringID in dictionaryEntries.Keys)
                    {
                        //Remove unsupported strings if needed
                        if (!IsStringSupported(stringID))
                        {
                            if (safeMode)
                            {
                                DebugUtils.LogWarning(this, "Found a non supported string ID: " + DebugUtils.ToQuote(stringID) + ". Please consider removing this entry.");
                            }
                            else
                            {
                                stringIDsToRemove.Add(stringID);
                                break;
                            }
                        }

                        List<string> missingLanguages = new List<string>(m_SupportedLanguages);
                        List<string> languagesToRemove = new List<string>();
                        foreach (string languageID in dictionaryEntries[stringID].Keys)
                        {
                            if (IsLanguageSupported(languageID))
                            {
                                missingLanguages.Remove(languageID);
                            }
                            else
                            {
                                if (safeMode)
                                {
                                    DebugUtils.LogWarning(this, "Found a non supported language ID: " + DebugUtils.ToQuote(languageID) + " for string " + DebugUtils.ToQuote(stringID) + ". Please consider removing this entry.");
                                }
                                else
                                {
                                    languagesToRemove.Add(languageID);
                                }
                            }
                        }

                        //Remove unsupported languages if needed
                        foreach (string languageID in languagesToRemove)
                        {
                            dictionaryEntries[stringID].Remove(languageID);
                            DebugUtils.Log(this, "Removed a non supported language entry: " + DebugUtils.ToQuote(languageID) + " for string " + DebugUtils.ToQuote(stringID) + ".");
                        }

                        //Adding missing language entries
                        foreach (string languageID in missingLanguages)
                        {
                            if (!dictionaryEntries[stringID].ContainsKey(languageID))
                            {
                                dictionaryEntries[stringID][languageID] = "";
                            }
                        }
                    }

                    //Remove unsupported string Ids if needed
                    foreach (string stringID in stringIDsToRemove)
                    {
                        dictionaryEntries.Remove(stringID);
                        DebugUtils.Log(this, "Removed a non supported entry: " + DebugUtils.ToQuote(stringID) + ".");
                    }

                    //Adding missing entries
                    foreach (LocalizedStringData stringData in m_Strings)
                    {
                        if (!dictionaryEntries.ContainsKey(stringData.GetID()))
                        {
                            if (stringData.GetID() != "")
                            {
                                Dictionary<string, string> valuesByLanguage = new Dictionary<string, string>();
                                foreach (string language in m_SupportedLanguages)
                                {
                                    valuesByLanguage[language] = "";
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
                    if (BackupDictionary())
                    {
                        JSONUtils.WriteToPath(m_FilePath, LocalizationUtils.GetJSONObjectFromLocalizedEntries(dictionaryEntries), true);
                    }
                    else
                    {
                        DebugUtils.LogError(this, "Could not backup dictionary, aborting generation!");
                    }
                }
                else
                {
                    DebugUtils.LogError(this, "Generation failure because of one or more errors in file " + DebugUtils.ToQuote(m_FilePath) + ". Please fix them before generation.");
                }
            }
            else
            {
                DebugUtils.LogError(this, "Could not create/read file at " + DebugUtils.ToQuote(m_FilePath));
            }
        }

        private bool BackupDictionary()
        {
            return FileUtils.CreateFolder(GetBackupFolderPath()) &&
                (!FileUtils.FileExists(m_FilePath) || FileUtils.CopyFile(m_FilePath, GetBackupFolderPath() + '/' + GenerateBackupFileName()));
        }

        private bool DeleteBackup()
        {
            return FileUtils.DeleteFolder(GetBackupFolderPath(), true, true);
        }

        private string GenerateBackupFileName()
        {
            return FileUtils.InsertBeforeFileExtension(FileUtils.GetFileName(m_FilePath), (TimeUtils.GetFormatedTime("MM-dd-yyyy_HH-mm-ss") + Constants.BLACKSMITH_DICTIONARY_BACKUP_FILE_EXTENSION));
        }

        private string GetBackupFolderPath()
        {
            return FileUtils.RemoveFileFromPath(m_FilePath) + FileUtils.GetFileNameWithoutExtension(m_FilePath) + Constants.BLACKSMITH_DICTIONARY_BACKUP_FOLDER_NAME;
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

        private bool IsStringSupported(string languageID)
        {
            foreach (LocalizedStringData stringData in m_Strings)
            {
                if (stringData.GetID() == languageID)
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasDuplicatedLanguages(out string duplicated)
        {
            duplicated = "";
            List<string> supportedLanguages = new List<string>();
            foreach (string language in m_SupportedLanguages)
            {
                if (!supportedLanguages.Contains(language))
                {
                    supportedLanguages.Add(language);
                }
                else
                {
                    duplicated = language;
                    return true;
                }
            }
            return false;
        }

        private bool HasDuplicatedStrings(out string duplicated)
        {
            duplicated = "";
            List<string> registeredStrings = new List<string>();
            foreach (LocalizedStringData localizedString in m_Strings)
            {
                if (!registeredStrings.Contains(localizedString.GetID()))
                {
                    registeredStrings.Add(localizedString.GetID());
                }
                else
                {
                    duplicated = localizedString.GetID();
                    return true;
                }
            }
            return false;
        }
        #endregion
#endif
    }
}