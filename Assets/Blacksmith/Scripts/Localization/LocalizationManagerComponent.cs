using UnityEngine;

namespace Blacksmith
{
    //THIS IS A CONTAINER CLASS, SHOULD NOT HANDLE ANY LOGIC
    [RequireComponent(typeof(LocalizedDictionaryHolderComponent))]
    public class LocalizationManagerComponent : FakeSingletonsHolderComponent<LocalizationManagerComponent>
    {
        #region Methods
        //PROTECTED
        protected override EBaseFlags[] GetBaseFlags()
        {
            return new EBaseFlags[]
            {
            };
        }
        #endregion
    }
}