namespace Blacksmith
{
    public class DebugHelper
    {
        #region Methods
        //PUBLIC
        public static DebugLoggerComponent GetDebugLogger()
        {
            if (DebugManagerComponent.TryGetInstance(out DebugManagerComponent instance))
            {
                if (instance.TryGetFakeSingleton(out DebugLoggerComponent debugLogger))
                {
                    return debugLogger;
                }
            }
            return null;
        }

        public static bool TryGetDebugLogger(out DebugLoggerComponent debugLogger)
        {
            debugLogger = GetDebugLogger();
            return (debugLogger != null);
        }
        //PROTECTED
        //PRIVATE
        #endregion
    }
}