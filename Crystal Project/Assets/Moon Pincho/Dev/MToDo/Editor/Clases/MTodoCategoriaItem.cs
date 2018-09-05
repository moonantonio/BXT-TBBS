//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoCategoriaItem.cs (15/11/2016)                                           \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Clase de las categorias de MTodo   								\\
// Fecha Mod:       15/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.MEditor.ToDo
{
    /// <summary>
    /// <para>Clase de las categorias de MTodo</para>
    /// </summary>
    internal class MTodoCategoriaItem
    {
        #region Variables
        /// <summary>
        /// <para>Nombre de la categoria</para>
        /// </summary>
        internal string nombre = "";                                        // Nombre de la categoria
        /// <summary>
        /// <para>Nombre la siguiente categoria</para>
        /// </summary>
        internal string nombreProx = "";                                    // Nombre la siguiente categoria
        /// <summary>
        /// <para>Color de la categoria</para>
        /// </summary>
        internal Color color = Color.black;                                 // Color de la categoria
        /// <summary>
        /// <para>Color de la siguiente categoria</para>
        /// </summary>
        internal Color colorProx = Color.black;                             // Color de la siguiente categoria
        #endregion

        #region Constructor
        /// <summary>
        /// <para>Constructor de MTodoCategoria</para>
        /// </summary>
        /// <param name="nombre">Nombre</param>
        /// <param name="color">Color</param>
        internal MTodoCategoriaItem(string nombre, Color color)// Constructor de MTodoCategoria
        {
            this.nombre = nombre;
            this.nombreProx = nombre;
            this.color = color;
            this.colorProx = color;
        }
        #endregion
    }
}