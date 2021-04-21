using TMPro;
using UnityEngine;

namespace Blacksmith
{
    [System.Serializable]
    public class LocalizedTMPText
    {
        #region Serialized Fields
        //PUBLIC
        //PROTECTED
        //PRIVATE
        [SerializeField]
        private LocalizedStringData m_Data;
        [SerializeField]
        private TMP_Text m_Text;
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
        #endregion

        #region Methods
        //PUBLIC
        public void Display()
        {
            m_Text.text = m_Data.GetValue();
        }

        public void Hide()
        {
            m_Text.text = "";
        }

        public void SetArg<T>(int index, T arg)
        {

        }
        //PROTECTED
        //PRIVATE
        #endregion
    }
}