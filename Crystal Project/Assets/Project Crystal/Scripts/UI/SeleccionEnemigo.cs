//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// PanelNotificaciones.cs (07/12/2016)                                          \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Boton de seleccion de enemigos          						\\
// Fecha Mod:       07/12/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.Sistemas.MUI
{
    /// <summary>
    /// <para>Boton de seleccion de enemigos</para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Sistemas/SFreya/Interno/SeleccionEnemigo")]
    public class SeleccionEnemigo : MonoBehaviour
    {
        public GameObject prefabEnemigo;

        public void SeleccionarEnemigo()
        {
            GameObject.Find("SFreya").GetComponent<SFreya.SFreya>().SeleccionEnemigo(prefabEnemigo);
            Time.timeScale = 1.0f;
        }

        public void MostrarSelector()
        {
            prefabEnemigo.transform.Find("Selector").gameObject.SetActive(true);
        }

        public void OcultarSelector()
        {
            prefabEnemigo.transform.Find("Selector").gameObject.SetActive(false);
        }
    }
}
