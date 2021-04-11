using UnityEngine;

namespace Blacksmith
{
    //THIS IS A CONTAINER CLASS, SHOULD NOT HANDLE ANY LOGIC
    [RequireComponent(typeof(SoundSpawnerComponent))]
    public class SoundManagerComponent : FakeSingletonsHolderComponent<SoundManagerComponent>
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