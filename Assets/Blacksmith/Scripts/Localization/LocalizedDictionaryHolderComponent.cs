using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blacksmith
{
    public abstract class LocalizedDictionaryHolderComponent : FakeSingletonComponent
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
        //PRIVATE
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
        //PUBLIC
        public bool TryGetLocalizedStringFromID(string stringID, out string localizedString)
        {
            localizedString = Constants.BLACKSMITH_INVALID_LOCALIZED_STRING_VALUE;
            return false;
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