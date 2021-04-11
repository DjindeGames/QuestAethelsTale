using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Blacksmith
{
    public enum EDebugLogType
    {
        Error,
        Warning,
        LocalizationWarning
    }

    public class DebugLoggerComponent : FakeSingletonComponent
    {
        #region Serialized Fields
        //PUBLIC
        //PROTECTED
        //PRIVATE
        [SerializeField]
        private bool m_DoLog = true;
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
        List<string> m_Errors = new List<string>();
        List<string> m_Warnings = new List<string>();
        List<string> m_LocalizationWarnings = new List<string>();
        #endregion

        #region Methods
        //MONOBEHAVIOUR
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        private void OnDestroy()
        {
            WriteLogs();
        }

        //PUBLIC
        public void RegisterLog(string log, EDebugLogType logType)
        {
            switch(logType)
            {
                case (EDebugLogType.Error):
                    m_Errors.Add(log);
                    break;
                case (EDebugLogType.Warning):
                    m_Errors.Add(log);
                    break;
                case (EDebugLogType.LocalizationWarning):
                    m_Errors.Add(log);
                    break;
            }
        }

        //PROTECTED
        protected override EBaseFlags[] GetBaseFlags()
        {
            return new EBaseFlags[]
            {
            };
        }
        //PRIVATE
        void WriteLogs()
        {
            List<string> logs = new List<string>();
            logs.Add("Errors:");
            foreach (string error in m_Errors)
            {
                logs.Add(error);
            }
            logs.Add("Warnings:");
            foreach (string warning in m_Warnings)
            {
                logs.Add(warning);
            }
            logs.Add("Localization:");
            foreach (string localizationWarning in m_LocalizationWarnings)
            {
                logs.Add(localizationWarning);
            }
            Directory.CreateDirectory(Constants.BLACKSMITH_ERROR_LOGS_FOLDER);
            File.WriteAllLines(Constants.BLACKSMITH_ERROR_LOGS_FOLDER + "/" + SceneManager.GetActiveScene().name + Constants.BLACKSMITH_ERROR_LOGS_FILENAME, logs);
        }
        #endregion
    }
}