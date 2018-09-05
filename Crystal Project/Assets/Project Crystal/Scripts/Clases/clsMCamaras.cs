//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// clsMCamaras.cs (28/11/2016)                                              	\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Clase representativa de la camaras    							\\
// Fecha Mod:       28/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.Sistemas.Camaras
{
    /// <summary>
    /// <para>Clase representativa de la camaras  </para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Clases/clsMCamaras")]
    [System.Serializable]
    public class clsMCamaras
    {
        #region Propiedades
        /// <summary>
        /// <para>Camara</para>
        /// </summary>
        public Camera camara;                         // Camara
        /// <summary>
        /// <para>Id de la camara</para>
        /// </summary>
        public int id;                                // Id de la camara
        /// <summary>
        /// <para>Pivote de la camara</para>
        /// </summary>
        public GameObject pivote;                     // Pivote de la camara
        #endregion

        #region API
        /// <summary>
        /// <para>Esta la camara activada</para>
        /// </summary>
        /// <param name="activado">Activar o desactivar la camara</param>
        public void enabled(bool activado)// Esta la camara activada
        {
            if(activado == false)
                this.camara.enabled = false;

            if (activado == true)
                this.camara.enabled = true;
        }
        #endregion
    }
}
