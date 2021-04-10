using UnityEngine;

namespace Blacksmith
{
    public enum EBaseFlags
    {
        Pausable,
        SoundMaker
    }

    public abstract class BaseComponent : MonoBehaviour
    {
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
        protected bool IsGamePaused { get; private set; } = false;
        //PRIVATE
        #endregion

        #region Methods
        //MONOBEHAVIOUR
        protected virtual void Awake()
        {
        }

        protected virtual void Start()
        {
            foreach (EBaseFlags flag in GetBaseFlags())
            {
                switch(flag)
                {
                    case (EBaseFlags.Pausable):
                        //Register to event
                        break;
                    case (EBaseFlags.SoundMaker):
                        //Register to event
                        break;
                }
            }
        }

        //PUBLIC
        public bool HasFlag(EBaseFlags flag)
        {
            foreach (EBaseFlags baseFlag in GetBaseFlags())
            {
                if (flag == baseFlag)
                {
                    return true;
                }
            }
            return false;
        }

        //PROTECTED
        protected abstract EBaseFlags[] GetBaseFlags();

        protected virtual void OnGamePaused()
        {
            IsGamePaused = true;
        }

        protected virtual void OnGameResumed()
        {
            IsGamePaused = false;
        }

        protected virtual void OnSoundSettingChanged() {}

        //PRIVATE
        #endregion
    }
}