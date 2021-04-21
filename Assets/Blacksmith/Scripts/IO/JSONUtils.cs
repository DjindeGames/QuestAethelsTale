namespace Blacksmith
{
    public class JSONUtils
    {
        #region Methods
        //PUBLIC
        public static bool TryLoadFromPath(string path, out JSONObject jsonObject, bool createFile = false)
        {
            jsonObject = new JSONObject();
            if (FileUtils.TryReadFileFromPath(path, out string content, createFile))
            {
                jsonObject = new JSONObject(content);
                return true;
            }
            return false;
        }
        public static JSONObject GetJSONFromString(string content)
        {
            return new JSONObject(content);
        }

        public static bool WriteToPath(string path, JSONObject jsonObject, bool needLogs = false, bool createFile = true)
        {
            return FileUtils.WriteToFile(path, jsonObject.Print(true), needLogs, createFile);
        }
        #endregion
    }
}