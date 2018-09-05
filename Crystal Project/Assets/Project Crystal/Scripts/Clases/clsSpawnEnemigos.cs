//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// clsClases.cs (15/12/2016)                                                    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Clase general de spawn                    						\\
// Fecha Mod:       15/12/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.Sistemas.Agne
{
    /// <summary>
    /// <para>Clase general de spawn</para>
    /// </summary>
    [System.Serializable]
    [AddComponentMenu("Moon Pincho/Sistemas/SAgne/Interno/clsSpawnEnemigos")]
    public class clsSpawnEnemigos
    {
        #region Propiedades
        /// <summary>
        /// <para>Nombre del Spawn</para>
        /// </summary>
        public string nombreSpawn;                      // Nombre del Spawn
        /// <summary>
        /// <para>Prefab del enemigo</para>
        /// </summary>
        public GameObject prefabEnemigo;                // Prefab del enemigo
        /// <summary>
        /// <para>Punto de Spawn del enemigo</para>
        /// </summary>
        public Transform spawnPoint;                    // Punto de Spawn del enemigo
        /// <summary>
        /// <para>Tiempo actual de spawn del enemigo</para>
        /// </summary>
        public float tiempoActualSpawn;                 // Tiempo actual de spawn del enemigo
        /// <summary>
        /// <para>Tiempo maximo del Spawn del enemigo</para>
        /// </summary>
        public float tiempoSpawn;                       // Tiempo maximo del Spawn del enemigo
        /// <summary>
        /// <para>Actual spawn del enemigo</para>
        /// </summary>
        public int actualSpawn;                         // Actual spawn del enemigo
        /// <summary>
        /// <para>Maximo del spawn del enemigo</para>
        /// </summary>
        public int maxSpawn;                            // Maximo del spawn del enemigo
        #endregion
    }
}
