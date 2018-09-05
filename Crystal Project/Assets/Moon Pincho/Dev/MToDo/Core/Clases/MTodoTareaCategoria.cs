//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoTareaCategoria.cs (15/11/2016)                                          \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Clase de las categorias de las tareas    						\\
// Fecha Mod:       15/11/2016                                                  \\
// Ultima Mod:  Version Inicial    												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.MEditor.ToDo
{
    /// <summary>
    /// <para>Clase de las categorias de las tareas</para>
    /// </summary>
    [System.Serializable]
    public class MTodoTareaCategoria
    {
        #region Variables Publicas
        /// <summary>
        /// <para>Nombre de la categoria</para>
        /// </summary>
        public string Nombre;                                       // Nombre de la categoria
        /// <summary>
        /// <para>Color de la categoria</para>
        /// </summary>
        public Color Color;                                         // Color de la categoria
        #endregion

        #region Constructor
        /// <summary>
        /// <para>Constructor de la clase MTodoTareaCategoria</para>
        /// </summary>
        /// <param name="nombre">Nombre de la categoria</param>
        /// <param name="color">Color de la cateogria</param>
        public MTodoTareaCategoria(string nombre,Color color)// Constructor de la clase MTodoTareaCategoria
        {
            Nombre = nombre;
            Color = color;
        }
        #endregion

    }
}