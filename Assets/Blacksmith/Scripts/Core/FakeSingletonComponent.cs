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
        private static int s_Instances = 0;
        #endregion

        #region Methods
        //MONOBEHAVIOUR
        protected override void Awake()
        {
            base.Awake();
            s_Instances++;
            if (s_Instances > 1)
            {
                DebugUtils.LogError(this, "There is already one instance created, please remove this one!");
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