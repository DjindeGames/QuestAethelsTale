namespace Blacksmith
{
    public abstract class SingletonComponent<T> : BaseComponent where T : BaseComponent
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
        private static T s_Instance = default(T);
        #endregion

        #region Methods
        //MONOBEHAVIOUR
        protected override void Awake()
        {
            base.Awake();
            if (s_Instance != null)
            {
                DebugUtils.LogError(s_Instance, "Singleton already defined, this one will not be instantiated!");
                Destroy(gameObject);
            }
            else
            {
                s_Instance = GetComponent<T>();
            }
        }
        //PUBLIC
        public static T GetInstance()
        {
            if (s_Instance == null)
            {
                DebugUtils.LogError(s_Instance, "Singleton has no instance!");
            }
            return s_Instance;
        }

        public static bool TryGetInstance(out T instance)
        {
            instance = GetInstance();
            return (instance != null);
        }
        //PROTECTED
        //PRIVATE
        #endregion
    }
}