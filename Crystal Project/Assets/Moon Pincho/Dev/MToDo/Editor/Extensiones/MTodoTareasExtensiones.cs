//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoTareasExtensiones.cs (15/11/2016)                                       \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Extensiones para MTodo Tareas          							\\
// Fecha Mod:       15/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using System;
using System.Collections;
using UnityEngine;
using System.IO;
#endregion

namespace MoonPincho.MEditor.ToDo
{
    /// <summary>
    /// <para>Extension para leer y escribir en txt</para>
    /// </summary>
    [System.Serializable]
    internal static class MTodoManagerLecturaEscritura
    {
        #region API
        /// <summary>
        /// <para>Lee un archivo desde una ruta</para>
        /// </summary>
        /// <param name="pathArchivo">Ruta del archivo</param>
        /// <returns></returns>
        internal static string[] LeerArchivo(string pathArchivo)// Lee un archivo desde una ruta
        {
            if (!File.Exists(Application.dataPath + pathArchivo))
             {
                 TextWriter tw = new StreamWriter(Application.dataPath + pathArchivo, true);
                 tw.WriteLine("");
                 tw.Close();
                 return null;
             }
             StreamReader sourse = new StreamReader(Application.dataPath + pathArchivo);
             string fileContents = sourse.ReadToEnd();
             sourse.Close();
             string[] lineas = fileContents.Split("\n"[0]);

             return lineas;
        }

        /// <summary>
        /// <para>Guarda los datos en un archivo</para>
        /// </summary>
        /// <param name="datos">Datos que guardar</param>
        /// <param name="pathArchivo">Ruta del archivo</param>
        internal static void GuardarArchivo(string datos, string pathArchivo)// Guarda los datos en un archivo
        {
            if (!File.Exists(Application.dataPath + pathArchivo))
            {
                TextWriter tw = new StreamWriter(Application.dataPath + pathArchivo, true);
                tw.Close();
                return;
            }
            var st = File.CreateText(Application.dataPath + pathArchivo);

            st.Write(datos);
            st.Close();
        }
        #endregion
    }

    /// <summary>
    /// <para>Extension para colores hexadecimales</para>
    /// </summary>
    internal class MTodoConvertidorHex
    {
        #region API
        /// <summary>
        /// <para>Convierte un color a Hexadecimal</para>
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        internal static string ColorAHex(Color32 color)// Convierte un color a Hexadecimal
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }

        /// <summary>
        /// <para>Convierte un hexadecimal en un color</para>
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        internal static Color HexAColor(string hex)// Convierte un hexadecimal en un color
        {
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        }
        #endregion
    }

    /// <summary>
    /// <para>Extension para los ajustes</para>
    /// </summary>
    internal static class MTodoAjustes
    {
        #region Variables
        /// <summary>
        /// <para>Ruta del archivo</para>
        /// </summary>
        private const string pathArchivo = "/Moon Pincho/Dev/MToDo/Alexandria/MTodoAjustes.txt";        // Ruta del archivo
        /// <summary>
        /// <para>Ajustes</para>
        /// </summary>
        private static Hashtable ajustes = new Hashtable();                                             // Ajustes
        #endregion

        #region Hashtable
        /// <summary>
        /// <para>Obtiene los ajustes</para>
        /// </summary>
        internal static Hashtable getAjustes// Obtiene los ajustes
        {
            get
            {
                if (ajustes.Count == 0)
                    CargarAjustes();
                return ajustes;
            }
        }
        #endregion

        #region API
        /// <summary>
        /// <para>Obtiene los ajustes de las tareas</para>
        /// </summary>
        /// <param name="clave"></param>
        /// <param name="valorDevuelto"></param>
        /// <param name="maxIndex"></param>
        /// <param name="defaultValue"></param>
        internal static void GetAjustesTareas(string clave, out int valorDevuelto, int maxIndex = -1, int defaultValue = 0)// Obtiene los ajustes de las tareas
        {
            valorDevuelto = defaultValue;

            string itemValue = GetAjustesTareas(clave);
            if (itemValue != "")
            {
                valorDevuelto = int.Parse(itemValue);
                if (maxIndex != -1 && valorDevuelto >= maxIndex)
                    valorDevuelto = defaultValue;
            }
        }

        /// <summary>
        /// <para>Obtiene los ajustes de las tareas</para>
        /// </summary>
        /// <param name="clave"></param>
        /// <param name="valorDevuelto"></param>
        /// <param name="defaultValue"></param>
        internal static void GetAjustesTareas(string clave, out bool valorDevuelto, bool defaultValue = false)// Obtiene los ajustes de las tareas
        {
            valorDevuelto = defaultValue;

            string itemValue = GetAjustesTareas(clave);
            if (itemValue != "")
                valorDevuelto = int.Parse(itemValue) == 1 ? true : false;
        }

        /// <summary>
        /// <para>Obtiene los ajustes de las tareas</para>
        /// </summary>
        /// <param name="clave"></param>
        /// <returns></returns>
        private static string GetAjustesTareas(string clave)// Obtiene los ajustes de las tareas
        {
            if (ajustes.ContainsKey(clave))
                return ajustes[clave].ToString();
            return "";
        }

        /// <summary>
        /// <para>Fija los ajustes de las tareas</para>
        /// </summary>
        /// <param name="clave"></param>
        /// <param name="value"></param>
        internal static void SetAjustesTareas(string clave, bool value)// Fija los ajustes de las tareas
        {
            SetAjustesTareas(clave, value ? "1" : "0");
        }

        /// <summary>
        /// <para>Fija los ajustes de las tareas</para>
        /// </summary>
        /// <param name="clave"></param>
        /// <param name="value"></param>
        internal static void SetAjustesTareas(string clave, int value)// Fija los ajustes de las tareas
        {
            SetAjustesTareas(clave, value.ToString());
        }

        /// <summary>
        /// <para>Fija los ajustes de las tareas</para>
        /// </summary>
        /// <param name="clave"></param>
        /// <param name="value"></param>
        internal static void SetAjustesTareas(string clave, string value)// Fija los ajustes de las tareas
        {
            if (ajustes.ContainsKey(clave))
                ajustes[clave] = value;
            else
                ajustes.Add(clave, value);

            GuardarAjustes();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// <para>Carga los ajustes</para>
        /// </summary>
        private static void CargarAjustes()// Carga los ajustes
        {
            ajustes = new Hashtable();
            string[] textoCargado = MTodoManagerLecturaEscritura.LeerArchivo(pathArchivo);

            try
            {
                foreach (string stringItem in textoCargado)
                {
                    string[] splitedString = stringItem.Split('☐');
                    if (splitedString.Length != 2)
                    {
                        Debug.LogWarning("[TodoTareas]: La longitud de los elementos no es 2");
                        continue;
                    }

                    ajustes.Add(splitedString[0], splitedString[1]);

                }
            }
            catch (NullReferenceException)
            {
                ajustes = new Hashtable();
            }

        }

        /// <summary>
        /// <para>Guarda los ajustes</para>
        /// </summary>
        private static void GuardarAjustes()// Guarda los ajustes
        {
            bool primero = true;
            string textoGuardado = "";
            string nombreAjustes;

            foreach (DictionaryEntry data in ajustes)
            {
                if (!primero)
                    textoGuardado += "\n";
                else
                    primero = false;

                nombreAjustes = data.Key as string;
                if (ajustes.ContainsKey(data.Key))
                    textoGuardado += nombreAjustes + "☐" + ajustes[nombreAjustes];
            }

            MTodoManagerLecturaEscritura.GuardarArchivo(textoGuardado, pathArchivo);
        }
        #endregion
    }
}