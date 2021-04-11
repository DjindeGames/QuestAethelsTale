using System.Collections;
using UnityEngine;

namespace Blacksmith
{
    [RequireComponent(typeof(AudioSource))]
    public class OneShotSoundComponent : BaseComponent
    {
        private enum ESoundFadeType
        {
            In,
            Out
        };

        #region Serialized Fields
        //PUBLIC
        //PROTECTED
        //PRIVATE
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
        private AudioSource m_AudioSource;
        private bool m_IsPaused = false;
        private bool m_ForcePause = false;
        private ESoundType m_SoundType = ESoundType.SFX;
        #endregion

        #region Methods
        //MONOBEHAVIOUR
        protected override void Awake()
        {
            base.Awake();
            if (TryGetComponent(out m_AudioSource))
            {
                m_AudioSource.Play();
            }
            else
            {
                DebugUtils.LogWarning(this, "No AudioSource component on gameObject.");
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            //Destroy gameobject as soon as the sound is over
            if (!m_AudioSource.isPlaying && !m_IsPaused)
            {
                Destroy(gameObject);
            }
        }

        //PUBLIC
        public void SetSoundType(ESoundType soundType)
        {
            m_SoundType = soundType;
        }

        public void FadeIn(float duration)
        {
            StartCoroutine(PerformFade(ESoundFadeType.In, duration));
        }

        public void FadeOut(float duration, bool destroyWhenComplete = false)
        {
            StartCoroutine(PerformFade(ESoundFadeType.Out, duration, destroyWhenComplete));
        }

        public void Pause()
        {
            InternPause(true);
        }

        public void Resume()
        {
            if (m_AudioSource != null)
            {
                m_AudioSource.Play();
                m_IsPaused = false;
                m_ForcePause = false;
            }
            else
            {
                DebugUtils.LogWarning(this, "Trying to resume but AudioSource is null.");
            }
        }

        //PROTECTED
        protected override EBaseFlags[] GetBaseFlags()
        {
            return new EBaseFlags[]
            {
                EBaseFlags.Pausable,
                EBaseFlags.SoundMaker
            };
        }

        protected override void OnSoundSettingChanged()
        {
            base.OnSoundSettingChanged();
        }

        protected override void OnGamePaused()
        {
            base.OnGamePaused();
            Pause();
        }

        protected override void OnGameResumed()
        {
            base.OnGameResumed();
            if (!m_ForcePause)
            {
                Resume();
            }
        }

        //PRIVATE
        private void InternPause(bool force = false)
        {
            if (m_AudioSource != null)
            {
                m_AudioSource.Pause();
                m_IsPaused = true;
                m_ForcePause = force;
            }
            else
            {
                DebugUtils.LogWarning(this, "Trying to pause but AudioSource is null.");
            }
        }

        IEnumerator PerformFade(ESoundFadeType type, float duration, bool destroyWhenComplete = false)
        {
            while(m_AudioSource == null)
            {
                yield return new WaitForEndOfFrame();
            }

            if (type == ESoundFadeType.In)
            {
                float volumeStep = duration / (1f - m_AudioSource.volume);
                while (m_AudioSource.volume < 1f)
                {
                    m_AudioSource.volume += volumeStep * Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                float volumeStep = duration / m_AudioSource.volume;
                while(m_AudioSource.volume > 0)
                {
                    m_AudioSource.volume -= volumeStep * Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                Pause();
            }

            //Raise SoundFadeCompleteEvent here

            if (destroyWhenComplete)
            {
                Destroy(gameObject);
            }
        }
        #endregion
    }
}