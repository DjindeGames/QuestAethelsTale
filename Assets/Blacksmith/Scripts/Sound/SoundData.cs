using NaughtyAttributes;
using UnityEngine;

namespace Blacksmith
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "Blacksmith/Sound/SoundData")]
    public class SoundData : DBEntry
    {
        #region Serialized Fields
        //PUBLIC
        public AudioClip m_AudioClip;
        public ESoundType m_SoundType = ESoundType.Music;
        public ESoundLocalizationType m_LocalizaionType = ESoundLocalizationType.Global;
        [ShowIf("ShowRangeAttributes")]
        [AllowNesting]
        public float m_MinRange;
        [ShowIf("ShowRangeAttributes")]
        [AllowNesting]
        public float m_MaxRange;
        //PROTECTED
        //PRIVATE
        private bool ShowRangeAttributes() { return m_LocalizaionType == ESoundLocalizationType.Point; }
        #endregion
    }
}