//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// clsEnemigo.cs (10/11/2016)                                              	    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Clase de los enemigos               							\\
// Fecha Mod:       10/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.Sistemas.SFreya
{
    /// <summary>
    /// <para>Clase de los enemigos </para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Clases/clsEnemigo")]
    [System.Serializable]
    public class clsEnemigo : clsClases
    {
        #region Propiedades
        /// <summary>
        /// <para>Tipo de enemigo</para>
        /// </summary>
        public Tipo tipo;                                   // Tipo de enemigo
        /// <summary>
        /// <para>Rareza del enemigo</para>
        /// </summary>
        public Rareza rareza;                               // Rareza del enemigo
        #endregion
    }

    /// <summary>
    /// <para>Enum del tipo del enemigo</para>
    /// </summary>
    public enum Tipo
    {
        Ninguno,
        Fuego,
        Agua,
        Electrico,
    }

    /// <summary>
    /// <para>Enum de la rareza del enemigo</para>
    /// </summary>
    public enum Rareza
    {
        Comun,
        Raro,
        Super,
        Boss
    }
}
