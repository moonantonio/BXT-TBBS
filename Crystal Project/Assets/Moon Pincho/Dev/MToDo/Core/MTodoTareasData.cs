//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoTareasData.cs (15/11/2016)                                              \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Clase ScriptableObject generica     							\\
// Fecha Mod:       15/11/2016                                                  \\
// Ultima Mod: Version inicial      											\\
//******************************************************************************\\

#region Librerias
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace MoonPincho.MEditor.ToDo
{
    /// <summary>
    /// <para>Clase ScriptableObject generica </para>
    /// </summary>
    [Serializable]
    public class MTodoTareasData : ScriptableObject
    {
        #region Variables Publicas
        /// <summary>
        /// <para>Lista de las tareas.</para>
        /// </summary>
        public List<MTodoTarea> Tareas = new List<MTodoTarea>();                            // Lista de las tareas
        /// <summary>
        /// <para>Lista de las categorias.</para>
        /// </summary>
        public List<MTodoTareaCategoria> Categorias = new List<MTodoTareaCategoria>();      // Lista de las categorias
        #endregion

        #region API
        /// <summary>
        /// <para>Obtiene la cantidad de tareas en data</para>
        /// </summary>
        public int GetTareasCount// Obtiene la cantidad de tareas en data
        {
            get { return Tareas.Count; }
        }

        /// <summary>
        /// <para>Obtiene la cantidad de categorias en data</para>
        /// </summary>
        public int GetCategoriasCount// Obtiene la cantidad de categorias en data
        {
            get { return Categorias.Count; }
        }

        /// <summary>
        /// <para> Obtiene la cantidad por categorias</para>
        /// </summary>
        /// <param name="categoria">Categoria</param>
        /// <returns>La cantidad de Categorias</returns>
        public int GetCountPorCategorias(int categoria)// Obtiene la cantidad por categorias
        {
            if (categoria == -1)
            {
                return categoria = GetTareasCount;
            }
            else
            {
                return categoria = Tareas.Count(e => e.Categoria == Categorias[categoria].ToString());
            }
        }

        /// <summary>
        /// <para>Obtiene la tarea de una posicion</para>
        /// </summary>
        /// <param name="index">Posicion</param>
        /// <returns>La tarea de la posicion.</returns>
        public MTodoTarea GetTareaDe(int index)// Obtiene la tarea de una posicion
        {
            return Tareas[index];
        }

        /// <summary>
        /// <para>Agrega una categoria</para>
        /// </summary>
        /// <param name="categoria">Categoria que se quiere agregar</param>
        public void AddCategoria(MTodoTareaCategoria categoria)// Agrega una categoria
        {
            /*if (Categorias.Contains(categoria) || string.IsNullOrEmpty(categoria.Nombre))
                return;*/
                
            Categorias.Add(categoria);
        }

        /// <summary>
        /// <para>Quita una categoria de una posicion</para>
        /// </summary>
        /// <param name="index">Posicion de la categoria</param>
        public void RemoveCategoria(int index)// Quita una categoria de una posicion
        {
            if (Categorias.Count >= (index + 1))
                Categorias.RemoveAt(index);
        }
        #endregion
    }
}