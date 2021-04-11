using UnityEngine;

namespace Blacksmith
{
    //THIS IS A CONTAINER CLASS, SHOULD NOT HANDLE ANY LOGIC
    [RequireComponent(typeof(DebugLoggerComponent))]
    public class DebugManagerComponent : FakeSingletonsHolderComponent<DebugManagerComponent>
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