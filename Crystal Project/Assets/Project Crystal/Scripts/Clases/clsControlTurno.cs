//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// clsEnemigo.cs (10/11/2016)                                              	    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Clase de control para los turnos               					\\
// Fecha Mod:       10/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.Sistemas.SFreya
{
    /// <summary>
    /// <para>Clase de control para los turnos</para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Clases/clsControlTurno")]
    [System.Serializable]
    public class clsControlTurno
    {
        #region Propiedades
        /// <summary>
        /// <para>Nombre del enemigo</para>
        /// </summary>
        public string enemigo;                                      // Nombre del enemigo
        /// <summary>
        /// <para>Tipo del enemigo</para>
        /// </summary>
        public string tipo;                                         // Tipo del enemigo
        /// <summary>
        /// <para>Prefab del enemigo</para>
        /// </summary>
        public GameObject prefabEnemigo;                            // Prefab del enemigo
        /// <summary>
        /// <para>Objetivo del enemigo</para>
        /// </summary>
        public GameObject objetivoEnemigo;                          // Objetivo del enemigo
        /// <summary>
        /// <para>Ataque elegido</para>
        /// </summary>
        public clsAtaques ataqueElegido;                            // Ataque elegido
        #endregion

    }
}
