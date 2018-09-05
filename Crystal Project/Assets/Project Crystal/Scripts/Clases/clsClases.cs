//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// clsClases.cs (07/12/2016)                                                    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Clase general de las clases              						\\
// Fecha Mod:       07/12/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections.Generic;
#endregion

namespace MoonPincho.Sistemas.SFreya
{
    /// <summary>
    /// <para>Clase general de las clases  </para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Clases/clsClases")]
    public class clsClases
    {
        #region Propiedades
        /// <summary>
        /// <para>Nombre del aliado/enemigo</para>
        /// </summary>
        public string nombre;                                   // Nombre del aliado/enemigo
        /// <summary>
        /// <para>Maxima vida del aliado/enemigo</para>
        /// </summary>
        public float maxHP;                                     // Maxima vida del aliado/enemigo
        /// <summary>
        /// <para>Actual vida del aliado/enemigo</para>
        /// </summary>
        public float actualHP;                                  // Actual vida del aliado/enemigo
        /// <summary>
        /// <para>Maximo eterion del aliado/enemigo</para>
        /// </summary>
        public float maxEterion;                                // Maximo eterion del aliado/enemigo
        /// <summary>
        /// <para>Actual eterion del aliado/enemigo</para>
        /// </summary>
        public float actualEterion;                             // Actual eterion del aliado/enemigo
        /// <summary>
        /// <para>Maximo ataque del aliado/enemigo</para>
        /// </summary>
        public float maxAtack;                                  // Maximo ataque del aliado/enemigo
        /// <summary>
        /// <para>Actual ataque del aliado/enemigo</para>
        /// </summary>
        public float actualAtack;                               // Actual ataque del aliado/enemigo
        /// <summary>
        /// <para>Maxima defensa del aliado/enemigo</para>
        /// </summary>
        public float maxDef;                                    // Maxima defensa del aliado/enemigo
        /// <summary>
        /// <para>Actual defensa del aliado/enemigo</para>
        /// </summary>
        public float actualDef;                                 // Actual defensa del aliado/enemigo
        /// <summary>
        /// <para>Lista de ataques que se pueden realizar</para>
        /// </summary>
        public List<clsAtaques> ataques = new List<clsAtaques>();// Lista de ataques que se pueden realizar
        #endregion
    }
}
