namespace Blacksmith
{
    public abstract class FakeSingletonsHolderComponent<T> : SingletonComponent<T> where T : BaseComponent
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
        private FakeSingletonComponent[] m_FakeSingletons;
        #endregion

        #region Methods
        //MONOBEHAVIOUR
        protected override void Awake()
        {
            base.Awake();
            m_FakeSingletons = GetComponents<FakeSingletonComponent>();
        }

        protected override void Start()
        {
            base.Start();
        }
        //PUBLIC
        public T GetFakeSingleton<T>()
        {
            T singleton = default(T);
            if (!ObjectUtils.TryCast(singleton, out FakeSingletonComponent fakeSingleton))
            {
                DebugUtils.LogError(this, DebugUtils.GetObjectType(singleton) + " is not a fake singleton component!");
            }
            else
            {
                foreach (FakeSingletonComponent component in m_FakeSingletons)
                {
                    if (ObjectUtils.TryCast(component, out singleton))
                    {
                        return singleton;
                    }
                }
            }
            DebugUtils.LogWarning(this, "Trying to get a fake singleton of type \"" + DebugUtils.GetObjectType(singleton) + "\" but it is not registered in this holder.");
            return singleton;
        }

        public bool TryGetFakeSingleton<T>(out T singleton)
        {
            singleton = GetFakeSingleton<T>();
            return (singleton != null);
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