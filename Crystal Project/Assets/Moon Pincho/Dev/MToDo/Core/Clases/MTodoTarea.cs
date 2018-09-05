//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoTarea.cs (15/11/2016)                                               	\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Clase de las tareas del Todo    								\\
// Fecha Mod:       15/11/2016                                                  \\
// Ultima Mod:  Version Inicial    												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.MEditor.ToDo
{
    /// <summary>
    /// <para>Clase de las tareas del Todo </para>
    /// </summary>
    [System.Serializable]
    public class MTodoTarea
    {
        #region Variables Publicas
        public string Titulo;
        public string Descripcion;
        public string Categoria;
        public string Fecha;
        public bool Terminada;
        public Color Color;
        #endregion

        #region Constructor
        /// <summary>
        /// <para>Constructor de la clase MTodoTarea</para>
        /// </summary>
        /// <param name="titulo">Titulo de la tarea</param>
        /// <param name="descripcion">Descripcion de la tarea</param>
        /// <param name="categoria">Categoria de la tarea</param>
        /// <param name="fecha">Fecha de la categoria</param>
        /// <param name="terminada">Ha sido terminada</param>
        /// <param name="color">Color del titulo</param>
        public MTodoTarea(string titulo,string descripcion,string categoria,string fecha,bool terminada,Color color)// Constructor de la clase MTodoTarea
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Categoria = categoria;
            Fecha = fecha;
            Terminada = terminada;
            Color = color;
        }
        #endregion

    }
}