//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// ControllerMonstruo.cs (11/12/2017)											\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Control del monstruo										\\
// Fecha Mod:		11/12/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio
{
	/// <summary>
	/// <para>Control del monstruo</para>
	/// </summary>
	public class ControllerMonstruo : MonoBehaviour
	{
		#region Variables
		[Header("Info Unidad")]
		public Unidad unidad;
		public ElementoMonstruo elemento;
		public TipoMonstruo tipo;
		#endregion

		#region Enums
		public enum ElementoMonstruo
		{
			Fuego,
			Agua,
			Rayo
		}
		public enum TipoMonstruo
		{
			Comun,
			Elite,
			Boss
		}
		#endregion
	}
}