using System;
using System.Collections.Generic;

namespace Blacksmith
{
    public abstract class FakeSingletonComponent : BaseComponent
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
        private static List<Type> s_InstantiatedSingletons = new List<Type>();
        #endregion

        #region Methods
        //MONOBEHAVIOUR
        protected override void Awake()
        {
            base.Awake();
            if (s_InstantiatedSingletons.Contains(this.GetType()))
            {
                DebugUtils.LogError(this, "There is already one instance created, please remove this one!");
            }
            else
            {
                s_InstantiatedSingletons.Add(this.GetType());
            }
        }

        protected override void Start()
        {
            base.Start();
        }
        //PUBLIC
        //PROTECTED
        //PRIVATE
        #endregion
    }
}