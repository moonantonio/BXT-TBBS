//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// PanelNotificaciones.cs (07/12/2016)                                          \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Controla las notificaciones en pantalla    						\\
// Fecha Mod:       07/12/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections;
#endregion

namespace MoonPincho.Sistemas.MUI
{
    /// <summary>
    /// <para>Controla las notificaciones en pantalla </para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Sistemas/SUI/Interno/PanelNotificaciones")]
    public class PanelNotificaciones : MonoBehaviour
    {
        #region Variables
        public GameObject btnInterrogante;
        public UILabel txtInterrogante;
        public GameObject notificacion;
        public UILabel txtNotificacion;
        #endregion

        #region Inicializaciones
        /// <summary>
        /// <para>Iniciador base de PanelNotificaciones</para>
        /// </summary>
        private void Start()// Iniciador base de PanelNotificaciones
        {

        }
        #endregion

        #region Actualizador
        /// <summary>
        /// <para>Actualizador base de PanelNotificaciones</para>
        /// </summary>
        private void Update() { }
        #endregion

        #region API
        public void Notificacion()
        {

        }
        #endregion
    }


}
