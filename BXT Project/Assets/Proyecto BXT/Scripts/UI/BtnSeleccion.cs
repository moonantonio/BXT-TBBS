//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// BtnSeleccion.cs (11/12/2017)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Boton de seleccion del enemigo								\\
// Fecha Mod:		11/12/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio
{
	/// <summary>
	/// <para>Boton de seleccion del enemigo.</para>
	/// </summary>
	public class BtnSeleccion : MonoBehaviour 
	{
		#region Variables Publicas
		public GameObject prefab;
		#endregion

		#region Metodos
		public void SeleccionarEnemigo()
		{
			GameObject.Find("Alexandria").GetComponent<Alexandria>().SegundaEleccion(prefab);
		}
		#endregion
	}
}