//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// clsAtaques.cs (10/12/2016)                                              	    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Clase de los diferentes ataques                					\\
// Fecha Mod:       10/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.Sistemas.SFreya
{
    /// <summary>
    /// <para>Clase de los diferentes ataques  </para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Clases/clsAtaques")]
    [System.Serializable]
    public class clsAtaques : MonoBehaviour
    {
        #region Propiedades
        /// <summary>
        /// <para>Nombre del ataque</para>
        /// </summary>
        public string nombreAtaque;                                 // Nombre del ataque
        /// <summary>
        /// <para>Descripcion del ataque</para>
        /// </summary>
        public string descripcionAtaque;                            // Descripcion del ataque
        /// <summary>
        /// <para>Daño que ocasiona el ataque</para>
        /// </summary>
        public float fuerzaAtaque;                                  // Daño que ocasiona el ataque
        /// <summary>
        /// <para>Coste del ataque (EP)</para>
        /// </summary>
        public float costeAtaque;                                   // Coste del ataque (EP)
        #endregion
    }
}
