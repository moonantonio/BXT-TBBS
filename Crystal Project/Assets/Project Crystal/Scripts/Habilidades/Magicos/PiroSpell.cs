//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// PiroSpell.cs (10/12/2016)                                              	    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Base de la magia piro                         					\\
// Fecha Mod:       10/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.Sistemas.SFreya
{
    /// <summary>
    /// <para>Base de la magia piro</para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Habilidades/Magicas/Piros/PiroSpell")]
    public class PiroSpell : clsAtaques
    {
        #region Constructor
        public PiroSpell()
        {
            nombreAtaque = "Piro";
            descripcionAtaque = "Ataque elemental de fuego.";
            fuerzaAtaque = 100.0f;
            costeAtaque = 10.0f;          
        }
        #endregion
    }
}
