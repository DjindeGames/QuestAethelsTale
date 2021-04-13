using System;
using System.Collections.Generic;
using System.IO;

namespace Blacksmith
{
    public class FileUtils
    {
        #region Methods
        //PUBLIC
        public static bool TryReadFileFromPath(string path, out string content, bool createFile = false)
        {
            bool success = true;
            content = "";
            
            try
            {
                content = System.IO.File.ReadAllText(path);
            }
            catch (Exception exception) when (
                exception is FileNotFoundException ||
                exception is DirectoryNotFoundException
                )
            {
                if (createFile)
                {
                    System.IO.File.WriteAllText(path, "");
                }
                else
                {
                    DebugUtils.LogError(null, "FileUtils.TryReadFileFromPath: File " + DebugUtils.ToQuote(path) + " does not exists!");
                    success = false;
                }
            }

            return success;
        }

        public static bool WriteToFile(string path, string content, bool needLogs = false, bool createFile = true)
        {
            if (File.Exists(path) || createFile)
            {
                System.IO.File.WriteAllText(path, content);
                if (needLogs)
                {
                    DebugUtils.LogSuccess(null, "FileUtils.WriteToFile: successfully wrote file " + DebugUtils.ToQuote(path) + ".");
                }
                return true;
            }
            else if (createFile)
            {
                DebugUtils.LogError(null, "FileUtils.WriteToFile: File " + DebugUtils.ToQuote(path) + " does not exists.");
            }
            return false;
        }

        public static bool DeleteFile(string path, bool needLogs = false)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                File.Delete(path + ".meta");
                if (needLogs)
                {
                    DebugUtils.LogSuccess(null, "FileUtils.DeleteFile: successfully deleted file " + DebugUtils.ToQuote(path) + ".");
                }
                return true;
            }
            else if (needLogs)
            {
                DebugUtils.LogWarning(null, "FileUtils.DeleteFile: File " + DebugUtils.ToQuote(path) + " does not exists.");
            }
            return false;
        }

        public static bool CreateFolder(string path, bool needLogs = false)
        {
            bool success = true;
            try
            {
                Directory.CreateDirectory(path);
                if (needLogs)
                {
                    DebugUtils.LogSuccess(null, "FileUtils.CreateFolder: successfully created folder " + DebugUtils.ToQuote(path));
                }
                
            }
            catch
            {
                success = false;
                if (needLogs)
                {
                    DebugUtils.LogError(null, "FileUtils.CreateFolder: Could not create folder " + DebugUtils.ToQuote(path));
                }
            }
            return success;
        }

        public static bool DeleteFolder(string path, bool recursive = true, bool needLogs = false)
        {
            bool success = true;
            try
            {
                Directory.Delete(path, recursive);
                if (needLogs)
                {
                    DebugUtils.LogSuccess(null, "FileUtils.DeleteFolder: successfully deleted folder " + DebugUtils.ToQuote(path));
                }

            }
            catch
            {
                success = false;
                if (needLogs)
                {
                    DebugUtils.LogError(null, "FileUtils.DeleteFolder: Could not delete folder " + DebugUtils.ToQuote(path));
                }
            }
            return success;
        }

        public static bool CopyFile(string fromPath, string toPath, bool needLogs = false)
        {
            bool success = true;
            try
            {
                File.Copy(fromPath, toPath);
                if (needLogs)
                {
                    DebugUtils.LogSuccess(null, "FileUtils.CopyFile: Successfully copied file " + DebugUtils.ToQuote(fromPath) + " to " + DebugUtils.ToQuote(toPath));
                }
            }
            catch
            {
                success = false;
                if (needLogs)
                {
                    DebugUtils.LogError(null, "FileUtils.CopyFile: Could not copy file " + DebugUtils.ToQuote(fromPath) + " to " + DebugUtils.ToQuote(toPath));
                }
            }
            return success;
        }

        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static string InsertFolderInPath(string path, string folder, int index)
        {
            List<string> newPath = new List<string>(path.Split('/'));
            if (index <= newPath.Count)
            {
                newPath.Insert(index, folder);
            }
            for (int i = 0; i < newPath.Count - 1; i++)
            {
                newPath[i] = newPath[i] + '/';
            }
            return String.Concat(newPath);
        }

        public static string RemoveFolderFromPath(string path, int index)
        {
            List<string> newPath = new List<string>(path.Split('/'));
            if (index <= newPath.Count)
            {
                newPath.RemoveAt(index);
            }
            for (int i = 0; i < newPath.Count - 1; i++)
            {
                newPath[i] = newPath[i] + '/';
            }
            return String.Concat(newPath);
        }

        public static string RemoveFileFromPath(string path)
        {
            List<string> newPath = new List<string>(path.Split('/'));
            if (newPath.Count > 1)
            {
                newPath.RemoveAt(newPath.Count - 1);
            }
            for (int i = 0; i < newPath.Count; i++)
            {
                newPath[i] = newPath[i] + '/';
            }
            return String.Concat(newPath);
        }

        public static string InsertBeforeFileExtension(string path, string token)
        {
            string[] newPath = path.Split('.');
            if (newPath.Length >= 2)
            {
                newPath[newPath.Length - 2] += token + '.';
            }
            return String.Concat(newPath);
        }

        public static int GetPathLength(string path)
        {
            return path.Split('/').Length;
        }
        public static string GetFileName(string path)
        {
            string[] splittedPath = path.Split('/');
            return (splittedPath.Length > 0) ? splittedPath[splittedPath.Length - 1] : path;
        }

        public static string GetFileNameWithoutExtension(string path)
        {
            string fileName = GetFileName(path);
            string[] splittedPath = fileName.Split('.');
            return (splittedPath.Length > 0) ? splittedPath[0] : fileName;
        }

        public static string GetFileExtension(string path)
        {
            List<string> SplittedPath = new List<string>(path.Split('.'));
            return (SplittedPath.Count > 0) ? SplittedPath[SplittedPath.Count - 1] : "";
        }
        #endregion
    }
}