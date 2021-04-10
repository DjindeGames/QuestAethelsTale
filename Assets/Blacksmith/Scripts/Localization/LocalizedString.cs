using UnityEngine;

namespace Blacksmith
{
    [System.Serializable]
    public class LocalizedString
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
        private bool m_LoggedWarning = false;
        #endregion

        #region Methods
        //PUBLIC
        public string GetValue()
        {
            if (true && !m_LoggedWarning)
            {
                m_LoggedWarning = true;
                DebugUtils.LogLocalizationWarning(m_StringID);
                return m_TempString;
            }
            return m_TempString;
        }

        public string GetLocalizedValue() { return ""; }
        //PROTECTED
        //PRIVATE
        #endregion
    }
}