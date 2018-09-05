//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoManagerSave.cs (15/11/2016)                                             \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:  Sistema de guardado de MTodo   								\\
// Fecha Mod:       15/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections;
using MoonPincho.MEditor.ToDo.Extensiones;
using UnityEditor;
#endregion

namespace MoonPincho.MEditor.ToDo.Editor
{
    /// <summary>
    /// <para>Sistema de guardado de MTodo</para>
    /// </summary>
    public class MTodoManagerSave : MonoBehaviour
    {
        #region Variables Privadas
        /// <summary>
        /// <para>Condicional para el autoescaneo</para>
        /// </summary>
        private static bool _autoScan;                                              // Condicional para el autoescaneo
        /// <summary>
        /// <para>Ruta de acceso de los datos</para>
        /// </summary>
        private static string _dataPath = "";                                       // Ruta de acceso de los datos
        /// <summary>
        /// <para>Ruta de acceso de los datos de las tareas</para>
        /// </summary>
        private static string _dataPathTarea = "";                                  // Ruta de acceso de los datos de las tareas
        #endregion

        #region GUI
        /// <summary>
        /// <para>Interfaz Grafica</para>
        /// </summary>
        [PreferenceItem("MToDo")]
        private static void MTodoManagerSaveGUI()// Interfaz Grafica
        {
            Cargar();

            _autoScan = EditorGUILayout.Toggle("Auto scan", _autoScan);
            if (_autoScan)
            {
                EditorGUILayout.HelpBox("Puede congelar Unity en grandes proyectos.", MessageType.Warning);
            }

            GUILayout.Label(_dataPath, GUILayout.ExpandWidth(true));
            if (GUILayout.Button("Buscar", EditorStyles.miniButton, GUILayout.ExpandWidth(false)))
                _dataPath = PathUtils.GlobalPathToRelative(EditorUtility.SaveFilePanel("", "Assets", "MTodo", "asset"));

            GUILayout.Label(_dataPathTarea, GUILayout.ExpandWidth(true));
            if (GUILayout.Button("Buscar", EditorStyles.miniButton, GUILayout.ExpandWidth(false)))
                _dataPathTarea = PathUtils.GlobalPathToRelative(EditorUtility.SaveFilePanel("", "Assets", "MTodoTareas", "asset"));

            if (GUI.changed)
                Guardar();

        }
        #endregion

        #region API Interna
        /// <summary>
        /// <para>Carga los datos de MTodo</para>
        /// </summary>
        private static void Cargar()// Carga los datos de MTodo
        {
            _autoScan = EditorPrefs.GetBool("MP_ToDo_Auto_Scan", true);
            _dataPath = EditorPrefs.GetString("MP_ToDo_Data_Path", @"Assets/Moon Pincho/Dev/MToDo/Alexandria/MTodo.asset");
            _dataPathTarea = EditorPrefs.GetString("MP_ToDo_Tarea_Data_Path", @"Assets/Moon Pincho/Dev/MToDo/Alexandria/MTodoTareas.asset");
        }

        /// <summary>
        /// <para>Guarda los datos de MTodo</para>
        /// </summary>
        private static void Guardar()// Guarda los datos de MTodo
        {
            EditorPrefs.SetBool("MP_ToDo_Auto_Scan", _autoScan);
            EditorPrefs.SetString("MP_ToDo_Data_Path", _dataPath);
            EditorPrefs.SetString("MP_ToDo_Tarea_Data_Path", _dataPathTarea);
        }
        #endregion
    }
}
