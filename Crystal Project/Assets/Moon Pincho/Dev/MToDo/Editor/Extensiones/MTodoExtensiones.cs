//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoExtensiones.cs (15/11/2016)                                             \\
// Autor: densylkin & Antonio Mateo (Moon Pincho) 								\\
// Descripcion: Extensiones varias para MTodo    								\\
// Fecha Mod:       15/11/2016                                                  \\
// Ultima Mod: Version inicial     												\\
//******************************************************************************\\

#region Librerias
using System;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
#endregion

namespace MoonPincho.MEditor.ToDo.Extensiones
{
#if UNITY_EDITOR
    public class VerticalBlock : IDisposable
    {
        public VerticalBlock(params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(options);
        }

        public VerticalBlock(GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(style, options);
        }

        public void Dispose()
        {
            GUILayout.EndVertical();
        }
    }

    public class ScrollviewBlock : IDisposable
    {
        public ScrollviewBlock(ref Vector2 scrollPos, params GUILayoutOption[] options)
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos, options);
        }

        public void Dispose()
        {
            GUILayout.EndScrollView();
        }
    }

    public class HorizontalBlock : IDisposable
    {
        public HorizontalBlock(params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(options);
        }

        public HorizontalBlock(GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(style, options);
        }

        public void Dispose()
        {
            GUILayout.EndHorizontal();
        }
    }

    public class ColoredBlock : System.IDisposable
    {
        public ColoredBlock(Color color)
        {
            GUI.color = color;
        }

        public void Dispose()
        {
            GUI.color = Color.white;
        }
    }

    public static class ScriptableObjectUtils
    {
        public static T CreateAsset<T>(string path) where T : ScriptableObject
        {
            var temp = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(temp, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return temp;
        }

        public static T LoadAsset<T>(string path) where T : ScriptableObject
        {
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        public static T LoadOrCreateAsset<T>(string path) where T : ScriptableObject
        {
            var temp = LoadAsset<T>(path);
            return temp ?? CreateAsset<T>(path);
        }

        public static bool DeleteAsset(string path)
        {
            var file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                return true;
            }
            else
                return false;
        }
    }
#endif

    public static class PathUtils
    {
        /// <summary>
        /// Convert global path to relative
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GlobalPathToRelative(string path)
        {
            if (path.StartsWith(Application.dataPath))
                return "Assets" + path.Substring(Application.dataPath.Length);
            else
                throw new ArgumentException("Incorrect path. PAth doed not contain Application.datapath");
        }

        /// <summary>
        /// Convert relative path to global
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string RelativePathToGlobal(string path)
        {
            return Path.Combine(Application.dataPath, path);
        }

        /// <summary>
        /// Convert path from unix style to windows
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string UnixToWindowsPath(string path)
        {
            return path.Replace("/", "\\");
        }

        /// <summary>
        /// Convert path from windows style to unix
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string WindowsToUnixPath(string path)
        {
            return path.Replace("\\", "/");
        }
    }


}