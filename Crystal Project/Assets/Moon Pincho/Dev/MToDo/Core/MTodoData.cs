//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoData.cs (15/11/2016)                                              	    \\
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
    /// <para>Clase ScriptableObject generica</para>
    /// </summary>
    [Serializable]
    public class MTodoData : ScriptableObject
    {
        #region Variables Publicas
        /// <summary>
        /// <para>Lista de los tickets.</para>
        /// </summary>
        public List<MTodoTickets> Tickets = new List<MTodoTickets>();       // Lista de los tickets
        /// <summary>
        /// <para>Lista de las tags.</para>
        /// </summary>
        public List<string> ListaTags = new List<string>()                  // Lista de las tags
        {
            "TODO",
            "BUG"
        };
        #endregion

        #region API
        /// <summary>
        /// <para>Obtiene los tickets de la data</para>
        /// </summary>
        public int GetTicketsCount// Obtiene los tickets de la data
        {
            get { return Tickets.Count; }
        }

        /// <summary>
        /// <para>Obtiene los tags de la data</para>
        /// </summary>
        public int GetTagsCount// Obtiene los tags de la data
        {
            get { return ListaTags.Count; }
        }

        /// <summary>
        /// <para>Obtiene la cantidad por tags</para>
        /// </summary>
        /// <param name="tag">Tag</param>
        /// <returns>La cantidad de tags</returns>
        public int GetCountPorTag(int tag)// Obtiene la cantidad por tags
        {
            //return tag != -1 ? Tickets.Count(e => e.Tag == ListaTags[tag]) : GetTicketsCount;

            if (tag == -1)
            {
                return tag = GetTicketsCount;
            }
            else
            {
                return tag = Tickets.Count(e => e.Tag == ListaTags[tag]);
            }
        }

        /// <summary>
        /// <para>Obtiene el ticket de una posicion</para>
        /// </summary>
        /// <param name="index">Posicion</param>
        /// <returns>El ticket de la posicion.</returns>
        public MTodoTickets GetTicketDe(int index)// Obtiene el ticket de una posicion
        {
            return Tickets[index];
        }

        /// <summary>
        /// <para>Agrega un tag</para>
        /// </summary>
        /// <param name="tag">Tag que se quiere agregar</param>
        public void AddTag(string tag)// Agrega un tag
        {
            if (ListaTags.Contains(tag) || string.IsNullOrEmpty(tag))
                return;
            ListaTags.Add(tag);
        }

        /// <summary>
        /// <para>Quita una tag de una posicion</para>
        /// </summary>
        /// <param name="index">Posicion de la tag</param>
        public void RemoveTag(int index)// Quita una tag de una posicion
        {
            if (ListaTags.Count >= (index + 1))
                ListaTags.RemoveAt(index);
        }
        #endregion
    }
}
