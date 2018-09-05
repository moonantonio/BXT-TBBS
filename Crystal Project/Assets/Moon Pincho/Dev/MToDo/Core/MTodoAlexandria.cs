//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoAlexandria.cs (15/11/2016)                                              \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Centro data de MTodo    										\\
// Fecha Mod:       15/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace MoonPincho.MEditor.ToDo
{
    /// <summary>
    /// <para>Centro data de MTodo </para>
    /// </summary>
    public class MTodoAlexandria : MonoBehaviour
    {
        #region Estaticas
        /// <summary>
        /// <para>Sistemas de MTodo</para>
        /// </summary>
        public static Sistemas sistemaMTodo = Sistemas.Todo;                        // Sistemas de MTodo
        #endregion
    }

    /// <summary>
    /// <para>Enum con los sistemas de MTodo</para>
    /// </summary>
    public enum Sistemas
    {
        Todo,
        Tareas,
        Trello
    }
}