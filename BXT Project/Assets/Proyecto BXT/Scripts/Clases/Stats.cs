//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// Stats.cs (11/12/2017)														\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Clase base de las estadisticas								\\
// Fecha Mod:		11/12/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio
{
	/// <summary>
	/// <para>Clase base de las estadisticas.</para>
	/// </summary>
	[System.Serializable]
	public class Stats
	{
		#region Variables
		public float hPMax;
		public float hPActual;
		public float mPMax;
		public float mPActual;

		public int fuerza;
		public int agilidad;
		public int inteligencia;
		#endregion
	}
}