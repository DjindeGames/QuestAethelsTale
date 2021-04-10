using NaughtyAttributes;
using UnityEngine;

namespace Blacksmith
{
    public enum ESoundType
    {
        SFX,
        Music
    }

    public enum ESoundLocalizationType
    {
        Global,
        Point
    }

    public class SoundSpawnerComponent : BaseComponent
    {
        #region Serialized Fields
        //PUBLIC
        //PROTECTED
        //PRIVATE
        [BoxGroup("References")]
        [SerializeField]
        private GameObject m_AudioSourcePrefab;
        public LocalizedString m_String;
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
        //MONOBEHAVIOUR
        protected override void Awake()
        {
            base.Awake();
            if (m_AudioSourcePrefab == null)
            {
                DebugUtils.LogWarning(this, "AudioSourcePrefab not set!");
            }
            m_String.GetValue();
        }

        //PUBLIC
        public OneShotSoundComponent PlaySound(Sound sound)
        {
            return Play2DSound(sound.m_AudioClip, sound.m_SoundType);
        }

        public OneShotSoundComponent PlaySound(Sound sound, Vector3 position)
        {
            return Play3DSound(sound.m_AudioClip, sound.m_SoundType, position, sound.m_MinRange, sound.m_MaxRange);
        }

        //PROTECTED
        protected override EBaseFlags[] GetBaseFlags()
        {
            return new EBaseFlags[]
            {
            };
        }

        //PRIVATE
        private OneShotSoundComponent Play2DSound(AudioClip sound, ESoundType type)
        {
            return PlaySound(sound, type, ESoundLocalizationType.Global, Vector3.zero);
        }

        private OneShotSoundComponent Play3DSound(AudioClip sound, ESoundType type, Vector3 position, float maxRange, float minRange)
        {
            return PlaySound(sound, type, ESoundLocalizationType.Point, position, minRange, maxRange);
        }

        private OneShotSoundComponent PlaySound(AudioClip sound, ESoundType soundType, ESoundLocalizationType localizationType, Vector3 position, float minRange = 0f, float maxRange = 0f)
        {
            OneShotSoundComponent oneShotSound = null;
            GameObject instantiatedSource = Instantiate(m_AudioSourcePrefab);
            if (ObjectUtils.GetComponent<AudioSource>(instantiatedSource, out AudioSource source))
            {
                if (ObjectUtils.GetComponent<OneShotSoundComponent>(instantiatedSource, out oneShotSound))
                {
                    instantiatedSource.transform.position = position;
                    source.clip = sound;
                    source.spatialBlend = (localizationType == ESoundLocalizationType.Global) ? 0f : 1f;
                    source.minDistance = (minRange == 0f && maxRange == 0f) ? /*GET DEFAULT VALUE FROM SETTINGS*/ 10f : minRange;
                    source.maxDistance = (minRange == 0f && maxRange == 0f) ? /*GET DEFAULT VALUE FROM SETTINGS*/ 10f : maxRange;
                    oneShotSound.SetSoundType(soundType);
                }
                else
                {
                    DebugUtils.LogWarning(this, "AudioSourcePrefab has no OneShotSoundComponent, sound will not be played");
                    Destroy(instantiatedSource);
                }
            }
            else
            {
                DebugUtils.LogWarning(this, "AudioSourcePrefab has no AudioSource component, sound will not be played");
                Destroy(instantiatedSource);
            }
            return oneShotSound;
        }
        #endregion
    }
}