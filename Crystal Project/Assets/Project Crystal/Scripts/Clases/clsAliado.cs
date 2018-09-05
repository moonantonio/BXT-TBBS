//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// clsAliado.cs (10/11/2016)                                              	    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Clase de los aliados                							\\
// Fecha Mod:       10/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections.Generic;
#endregion

namespace MoonPincho.Sistemas.SFreya
{
    /// <summary>
    /// <para>Clase de los aliados</para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Clases/clsClases")]
    [System.Serializable]
    public class clsAliado : clsClases
    {
        #region Propiedades
        /// <summary>
        /// <para>Fuerza del aliado</para>
        /// </summary>
        public int fuerza;                                  // Fuerza del aliado
        /// <summary>
        /// <para>Magia del aliado</para>
        /// </summary>
        public int magia;                                   // Magia del aliado
        /// <summary>
        /// <para>Espiritu del aliado</para>
        /// </summary>
        public int espiritu;                                // Espiritu del aliado
        /// <summary>
        /// <para>Velocidad del aliado</para>
        /// </summary>
        public int velocidad;                               // Velocidad del aliado
        /// <summary>
        /// <para>Ataques magicos</para>
        /// </summary>
        public List<clsAtaques> magias = new List<clsAtaques>();// Ataques magicos
        #endregion
    }
}
