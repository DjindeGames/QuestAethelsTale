using NaughtyAttributes;
using UnityEngine;

namespace Blacksmith
{
    public class LocalizedDictionaryHolderComponent : FakeSingletonComponent
    {
        #region Serialized Fields
        //PUBLIC
        //PROTECTED
        //PRIVATE
        [BoxGroup("References")]
        [SerializeField]
        private LocalizedDictionaryData m_DictionaryData;
        [BoxGroup("Settings")]
        [SerializeField]
        private string m_CurrentLanguageID;
        #endregion

        #region Events
        //PUBLIC
        //PROTECTED
        //PRIVATE
        #endregion

        #region Attributes
        //PUBLIC
        //PROTECTED
        //PRIVATE
        private RuntimeLocalizedDictionary m_RuntimeDictionary = new RuntimeLocalizedDictionary();
        #endregion

        #region Methods
        //MONOBEHAVIOUR
        protected override void Awake()
        {
            base.Awake();
            if (m_DictionaryData != null)
            {
                TextAsset dictionary = m_DictionaryData.GetGeneratedDictionary();
                if (dictionary != null)
                {
                    m_RuntimeDictionary.LoadFromText(dictionary.text);
                }
                else
                {
                    DebugUtils.LogError(this, "Dictionary datas have not been generated!");
                }
            }
            else
            {
                DebugUtils.LogError(this, "No dictionary data!");
            }
        }

        protected override void Start()
        {
            base.Start();
        }
        //PUBLIC
        public bool TryGetLocalizedStringFromID(string stringID, out string localizedString)
        {
            return m_RuntimeDictionary.TryGetLocalizedTextFromID(stringID, m_CurrentLanguageID, out localizedString);
        }
        //PROTECTED
        protected override EBaseFlags[] GetBaseFlags()
        {
            return new EBaseFlags[]
            {
            };
        }
        //PRIVATE
        #endregion
    }
}