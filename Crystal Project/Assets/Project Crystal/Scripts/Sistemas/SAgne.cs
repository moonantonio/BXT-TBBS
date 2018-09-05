//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// SAgne.cs (15/12/2016)                                              	        \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Sistema que controla las areas de los enemigos y su AI    		\\
// Fecha Mod:       15/12/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections.Generic;
#endregion

namespace MoonPincho.Sistemas.Agne
{
    /// <summary>
    /// <para>Sistema que controla las areas de los enemigos y su AI </para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Sistemas/SAgne/SAgne")]
    public class SAgne : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// <para>Spawn de enemigos</para>
        /// </summary>
        public List<clsSpawnEnemigos> spawnEnemigos = new List<clsSpawnEnemigos>();             // Spawn de enemigos
        #endregion

        #region Actualizadores
        /// <summary>
        /// <para>Actualizador</para>
        /// </summary>
        public void Update()// Actualizador
        {
            // Actualizar el tiempo actual de cada spawn
            ActualizarTiempo();

            // Recorrer la lista
            for (int i = 0; i < spawnEnemigos.Count; i++)
            {
                // Si el tiempo actual llega al limite
                if (spawnEnemigos[i].tiempoActualSpawn >= spawnEnemigos[i].tiempoSpawn)
                {
                    // Si se ha llegado al maximo de enemigos
                    if (spawnEnemigos[i].actualSpawn == spawnEnemigos[i].maxSpawn)
                    {
                        // Resetear tiempo
                        spawnEnemigos[i].tiempoActualSpawn = 0.0f;
                    }
                    else
                    {
                        // Si no se ha llegado al maximo de enemigos se instancia uno
                        GameObject enemigoInstancia = Instantiate(spawnEnemigos[i].prefabEnemigo, new Vector3( spawnEnemigos[i].spawnPoint.transform.position.x, spawnEnemigos[i].spawnPoint.transform.position.y, spawnEnemigos[i].spawnPoint.transform.position.z), Quaternion.identity) as GameObject;

                        // Agregar uno al Spawn actual
                        spawnEnemigos[i].actualSpawn++;

                        // Resetear Tiempo
                        spawnEnemigos[i].tiempoActualSpawn = 0.0f;
                    }                    
                }
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// <para>Actualiza el tiempo de los spawn</para>
        /// </summary>
        private void ActualizarTiempo()// Actualiza el tiempo de los spawn
        {
            for (int i = 0; i < spawnEnemigos.Count; i++)
            {
                spawnEnemigos[i].tiempoActualSpawn = spawnEnemigos[i].tiempoActualSpawn + 1 * Time.deltaTime;
            }
        }
        #endregion
    }
}
