//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// clsEscenas.cs (15/12/2016)                                              	    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Clase para el cambio de escenas                     			\\
// Fecha Mod:       15/12/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.Sistemas.Clases
{
    /// <summary>
    /// <para>Clase para el cambio de escenas</para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Clases/clsEscenas")]
    public class clsEscenas : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// <para>Escenas</para>
        /// </summary>
        private Escenas escenas;                                // Escenas
        #endregion
    }

    /// <summary>
    /// <para>Todas las escenas</para>
    /// </summary>
    public enum Escenas// Todas las escenas
    {
        Null,
        Capital,
        Omali,
        PasoAntiguo
    }
}
