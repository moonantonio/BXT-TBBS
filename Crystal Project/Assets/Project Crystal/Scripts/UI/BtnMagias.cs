//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// BtnMagias.cs (07/12/2016)                                                    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Boton del proceso de magias                           			\\
// Fecha Mod:       07/12/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using MoonPincho.Sistemas.SFreya;
#endregion

namespace MoonPincho.Sistemas.MUI
{
    /// <summary>
    /// <para>Boton del proceso de magias </para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Sistemas/SFreya/Interno/BtnMagias")]
    public class BtnMagias : MonoBehaviour
    {
        public clsAtaques procesoAtaqueMagico;

        public void CastAtaqueMagico()
        {
            GameObject.Find("SFreya").GetComponent<SFreya.SFreya>().AccionMagia(procesoAtaqueMagico);
        }
    }
}
