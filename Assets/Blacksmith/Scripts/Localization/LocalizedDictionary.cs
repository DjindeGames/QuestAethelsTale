using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Blacksmith
{
    using LocalizedEntries = Dictionary<string, Dictionary<string, string>>;

    public abstract class LocalizedDictionary
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
        LocalizedEntries m_Entries = new LocalizedEntries(); 
        #endregion

        #region Methods
        //PUBLIC
        public void LoadEntriesFromFile(string filePath)
        {
            try
            {
                JSONObject dictionaryContent = new JSONObject(System.IO.File.ReadAllText(filePath));
                Dictionary<string, string> languageDictionaryByID = dictionaryContent.ToDictionary();
                foreach(string key in languageDictionaryByID.Keys)
                {
                    if (!m_Entries.ContainsKey(key))
                    {
                        Dictionary<string, string> valuesByLanguage = dictionaryContent.GetField(key).ToDictionary();
                        Dictionary<string, string> validatedValuesByLanguage = new Dictionary<string, string>();
                        foreach(string language in valuesByLanguage.Keys)
                        {

                        }
                        m_Entries[key] = validatedValuesByLanguage;
                    }
                }
                /*
                Directory.CreateDirectory(savesPath + date);
                System.IO.File.WriteAllText(savesPath + date + "/" + date + Constants.SaveFilesExtension, serializedSave.Print(true));
                */
            }
            catch
            {
                DebugUtils.LogError(this, "Dictionary at given path does not exist!");
            }
            
        }
        //PROTECTED
        //PRIVATE
        #endregion
    }
}