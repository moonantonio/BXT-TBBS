//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// VenenoSpell.cs (10/12/2016)                                              	\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Base de la magia veneno                         				\\
// Fecha Mod:       10/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.Sistemas.SFreya
{
    /// <summary>
    /// <para>Base de la magia veneno</para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Habilidades/Magicas/Alterados/VenenoSpell")]
    public class VenenoSpell : clsAtaques
    {
        #region Constructor
        public VenenoSpell()
        {
            nombreAtaque = "Veneno";
            descripcionAtaque = "Ataque que baja la vida poco a poco.";
            fuerzaAtaque = 20.0f;
            costeAtaque = 3.0f;
        }
        #endregion
    }
}
