//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// Cuchillada.cs (10/12/2016)                                              	    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Base del ataque cuchillada                  					\\
// Fecha Mod:       10/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.Sistemas.SFreya
{
    /// <summary>
    /// <para>Base del ataque cuchillada</para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Habilidades/Ataques/Cuchillada")]
    public class Cuchillada : clsAtaques
    {
        #region Constructor
        public Cuchillada()
        {
            nombreAtaque = "Cuchillada";
            descripcionAtaque = "Este ataque es un veloz corte creado por la cuchilla del arma.";
            fuerzaAtaque = 10.0f;
            costeAtaque = 0.0f;
        }
        #endregion
    }
}
