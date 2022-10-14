#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Editor
{
    public class BuilderInternal : MonoBehaviour
    {
        [MenuItem("Build/BuildAndroidProject")]
        public static void BuildAndroidProject()
        {
            var folderName = "BuildAndroid";
            DeleteFolder (folderName);
            CreateFolder (folderName);
            string[] scenes = GetScenes();
            BuildReport error = BuildPipeline.BuildPlayer(scenes, folderName, BuildTarget.Android, BuildOptions.None);
            if (error != null)
            {
                throw new Exception("Build failed: " + error);
            }
        }
        
        [MenuItem("Build/BuildApk")]
        public static void BuildApk()
        {
            var outdir = System.Environment.CurrentDirectory + "/BuildOutPutPath/Android";
            var outputPath = Path.Combine(outdir, Application.productName + ".apk");
            // Обработка папки
            if (!Directory.Exists(outdir)) Directory.CreateDirectory(outdir);
            if (File.Exists(outputPath)) File.Delete(outputPath);
 
            // Запускаем проект в один клик
            string[] scenes = GetScenes();
            UnityEditor.BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.Android, BuildOptions.None);
            if (File.Exists(outputPath))
            {
                Debug.Log("Build Success :" + outputPath);
            }
            else
            {
                Debug.LogException(new Exception("Build Fail! Please Check the log! "));
            }
        }

        private static void CreateFolder(string name)
        {
            if (!Directory.Exists(name))
            {
                Directory.CreateDirectory(name);
            }
        }

        private static void DeleteFolder(string name)
        {
            if (Directory.Exists(name))
            {
                Directory.Delete(name, true);
            }
        }

        private static string[] GetScenes()
        {
            string[] scenes = new string[EditorBuildSettings.scenes.Length];
            for(int i = 0; i < scenes.Length; i++)
            {
                scenes[i] = EditorBuildSettings.scenes[i].path;
            }
            return scenes;
        }
    }
}
#endif