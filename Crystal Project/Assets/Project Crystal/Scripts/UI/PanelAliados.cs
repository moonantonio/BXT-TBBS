//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// PanelAliados.cs (07/12/2016)                                                 \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Panel que controla el panel de los aliados          			\\
// Fecha Mod:       07/12/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.Sistemas.MUI
{
    /// <summary>
    /// <para>Panel que controla el panel de los aliados  </para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Sistemas/SFreya/Interno/PanelAliados")]
    public class PanelAliados : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// <para>Texto del nombre del aliado</para>
        /// </summary>
        public UILabel txtNombre;                                   // Texto del nombre del aliado
        /// <summary>
        /// <para>Texto del HP del aliado</para>
        /// </summary>
        public UILabel txtHP;                                       // Texto del HP del aliado
        /// <summary>
        /// <para>Progress bar del HP del aliado</para>
        /// </summary>
        public UIProgressBar progressHP;                            // Progress bar del HP del aliado
        /// <summary>
        /// <para>Texto del EP del aliado</para>
        /// </summary>
        public UILabel txtEP;                                       // Texto del EP del aliado
        /// <summary>
        /// <para>Progress bar del EP del aliado</para>
        /// </summary>
        public UIProgressBar progressEP;                            // Progress bar del EP del aliado
        #endregion
    }
}
