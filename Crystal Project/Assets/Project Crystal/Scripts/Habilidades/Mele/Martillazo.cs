//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// Martillazo.cs (10/12/2016)                                              	    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Base del ataque martillazo                  					\\
// Fecha Mod:       10/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.Sistemas.SFreya
{
    /// <summary>
    /// <para>Base del ataque martillazo</para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Habilidades/Ataques/Martillazo")]
    public class Martillazo : clsAtaques
    {
        #region Constructor
        public Martillazo()
        {
            nombreAtaque = "Martillazo";
            descripcionAtaque = "Con el arma pega un martillazo al suelo que inabilita al enemigo.";
            fuerzaAtaque = 15.0f;
            costeAtaque = 0.0f;
        }
        #endregion
    }
}
