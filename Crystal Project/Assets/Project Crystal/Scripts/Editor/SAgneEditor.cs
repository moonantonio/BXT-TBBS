//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// SAgneEditor.cs (16/12/2016)                                              	\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Editor de SAgne    												\\
// Fecha Mod:       16/12/2016                                                  \\
// Ultima Mod: Version inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using MoonPincho.Sistemas.Agne;
#endregion

namespace MoonPincho.MEditor
{
    /// <summary>
    /// <para>Editor de SAgne </para>
    /// </summary>
    [CustomEditor(typeof(SAgne))]
    [AddComponentMenu("Moon Pincho/Editor/SAgneEditor")]
    public class SAgneEditor : Editor
    {
        #region Variables
        #region Animacion Interna
        AnimBool generales;
        AnimBool puntoSpawn;
        AnimBool tiempoSpawn;
        AnimBool enemigosSpawn;
        #endregion
        #endregion

        #region Inicializaciones
        /// <summary>
        /// <para>Cuando esta activado el SAgneEditor</para>
        /// </summary>
        private void OnEnable()// Cuando esta activado el SAgneEditor
        {
            // Almacenar las referencias de todas las variables.
            AlexandriaBiblioteca();
        }
        #endregion

        #region Actualizador
        /// <summary>
        /// <para>Interfaz</para>
        /// </summary>
        public override void OnInspectorGUI()// Interfaz
        {
            SAgne sagn = (SAgne)target;

            #region Header
            EditorGUILayout.BeginVertical("box");
            GUILayout.Space(5);
            EditorGUILayout.BeginHorizontal("box");
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Spawn Activos: " + sagn.spawnEnemigos.Count);
            if (GUILayout.Button("Agregar Spawn"))
            {
                AgregarSpawn();
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            //EditorGUILayout.BeginVertical("box");

            GUILayout.Space(10);
            #endregion

            for (int i = 0; i < sagn.spawnEnemigos.Count; i++)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Grupo Spawn " + sagn.spawnEnemigos[i].nombreSpawn);
                EditorGUILayout.EndHorizontal();
                #region Generales
                RenderizarHeader("General", "MP_SAgne_Generales", generales);
                if (EditorGUILayout.BeginFadeGroup(generales.faded))
                {
                    EditorGUILayout.BeginHorizontal();
                    sagn.spawnEnemigos[i].nombreSpawn = EditorGUILayout.TextField(sagn.spawnEnemigos[i].nombreSpawn, GUILayout.Width(100));
                    sagn.spawnEnemigos[i].prefabEnemigo = EditorGUILayout.ObjectField("Enemigo spawn " + sagn.spawnEnemigos[i].nombreSpawn + " :", sagn.spawnEnemigos[i].prefabEnemigo, typeof(GameObject), true) as GameObject;
                    if (GUILayout.Button("X"))
                    {
                        QuitarSpawn(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndFadeGroup();
                #endregion

                #region Punto
                RenderizarHeader("Spawn", "MP_SAgne_PuntoSpawn", puntoSpawn);
                if (EditorGUILayout.BeginFadeGroup(puntoSpawn.faded))
                {
                    EditorGUILayout.BeginHorizontal();
                    if (sagn.spawnEnemigos[i].spawnPoint == null)
                    {
                        EditorGUILayout.LabelField("Punto de Spawn: ");
                        if (GUILayout.Button("Crear Punto"))
                        {
                            CrearSpawnPoint(sagn.spawnEnemigos[i].nombreSpawn, i);
                        }
                    }
                    else
                    {
                        sagn.spawnEnemigos[i].spawnPoint = EditorGUILayout.ObjectField("Punto de Spawn: ", sagn.spawnEnemigos[i].spawnPoint, typeof(Transform), true) as Transform;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndFadeGroup();
                #endregion

                #region Tiempo
                RenderizarHeader("Tiempo Spawn", "MP_SAgne_TiempoSpawn", tiempoSpawn);
                if (EditorGUILayout.BeginFadeGroup(tiempoSpawn.faded))
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.BeginVertical();
                    EditorGUILayout.LabelField("Tiempo Spawn: ");
                    sagn.spawnEnemigos[i].tiempoActualSpawn = EditorGUILayout.Slider(sagn.spawnEnemigos[i].tiempoActualSpawn, 0, sagn.spawnEnemigos[i].tiempoSpawn);

                    EditorGUILayout.LabelField("Tiempo Max Spawn: ");
                    sagn.spawnEnemigos[i].tiempoSpawn = EditorGUILayout.Slider(sagn.spawnEnemigos[i].tiempoSpawn, 0, 200);
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndFadeGroup();
                #endregion

                #region Enemigos
                RenderizarHeader("Enemigo Spawn", "MP_SAgne_EnemigosSpawn", enemigosSpawn);
                if (EditorGUILayout.BeginFadeGroup(enemigosSpawn.faded))
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.BeginVertical();
                    EditorGUILayout.LabelField("Enemigos Spawns: ");
                    sagn.spawnEnemigos[i].actualSpawn = EditorGUILayout.IntField(sagn.spawnEnemigos[i].actualSpawn);

                    EditorGUILayout.LabelField("Maximo Enemigos Spawn: ");
                    sagn.spawnEnemigos[i].maxSpawn = EditorGUILayout.IntField(sagn.spawnEnemigos[i].maxSpawn);
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndFadeGroup();
                #endregion

                EditorGUILayout.EndVertical();
            }

            #region Final Header
           // EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
            #endregion

            Repaint();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// <para>Almacena las referencias de las variables.</para>
        /// </summary>
        private void AlexandriaBiblioteca()// Almacena las referencias de las variables.
        {
            generales = new AnimBool(EditorPrefs.GetBool("MP_SAgne_Generales"));
            puntoSpawn = new AnimBool(EditorPrefs.GetBool("MP_SAgne_PuntoSpawn"));
            tiempoSpawn = new AnimBool(EditorPrefs.GetBool("MP_SAgne_TiempoSpawn"));
            enemigosSpawn = new AnimBool(EditorPrefs.GetBool("MP_SAgne_EnemigosSpawn"));
        }

        /// <summary>
        /// <para>Agrega un Spawn</para>
        /// </summary>
        public void AgregarSpawn()// Agrega un Spawn
        {
            SAgne sagn = (SAgne)target;

            sagn.spawnEnemigos.Add(new clsSpawnEnemigos());
        }

        /// <summary>
        /// <para>Quita un Spawn</para>
        /// </summary>
        /// <param name="index">Index de la lista</param>
        public void QuitarSpawn(int index)// Quita un Spawn
        {
            SAgne sagn = (SAgne)target;

            sagn.spawnEnemigos.RemoveAt(index);
        }

        /// <summary>
        /// <para>Crea un spawn point y lo asigna al index pasado</para>
        /// </summary>
        /// <param name="nombreSpawn">Nombre Spawn</param>
        /// <param name="index">Index de la lista</param>
        public void CrearSpawnPoint(string nombreSpawn,int index)// Crea un spawn point y lo asigna al index pasado
        {
            SAgne sagn = (SAgne)target;
            GameObject root;
            GameObject nuevoGame = (GameObject)EditorGUIUtility.Load("Moon Pincho/Sistemas/Objeto.prefab"); ;
            GameObject hijo;

            root = GameObject.FindGameObjectWithTag("SAgne");

            hijo = root.AddChild(nuevoGame);
            hijo.transform.parent = root.transform;
            hijo.name = sagn.spawnEnemigos[index].nombreSpawn;
            sagn.spawnEnemigos[index].spawnPoint = hijo.transform;
            Selection.activeGameObject = hijo;
        }

        /// <summary>
        /// <para>Extension para mostrar un header.</para>
        /// </summary>
        /// <param name="nombreHeader">Nombre del header.</param>
        /// <param name="editorPref">Path del pref.</param>
        /// <param name="targetAnim">Objetivo para la animacion.</param>
        private void RenderizarHeader(string nombreHeader, string editorPref, AnimBool targetAnim)// Extension para mostrar un header
        {
            EditorGUILayout.BeginVertical("Toolbar");
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(nombreHeader, EditorStyles.boldLabel);
            if (GUILayout.Button(EditorPrefs.GetBool(editorPref) == true ? "Ocultar" : "Mostrar", EditorStyles.miniButton, GUILayout.Width(50), GUILayout.Height(14f)))
            {
                EditorPrefs.SetBool(editorPref, EditorPrefs.GetBool(editorPref) == true ? false : true);
                targetAnim.target = EditorPrefs.GetBool(editorPref);
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        #endregion

    }
}