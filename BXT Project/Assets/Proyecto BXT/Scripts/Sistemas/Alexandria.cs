//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// Alexandria.cs (11/12/2017)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Manager general del combate									\\
// Fecha Mod:		11/12/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections.Generic;
#endregion

namespace MoonAntonio
{
	/// <summary>
	/// <para>Manager general del combate.</para>
	/// </summary>
	public class Alexandria : MonoBehaviour 
	{
		#region Variables Publicas
		[Header("Estado")]
		public Procesado estadosBatalla;
		[Header("Acciones")]
		public List<HandleTurno> acciones = new List<HandleTurno>();
		[Header("Unidades de Combate")]
		public List<GameObject> heroes = new List<GameObject>();
		public List<GameObject> monstruos = new List<GameObject>();
		#endregion

		#region Enums
		public enum Procesado
		{
			ESPERAR,
			ELEGIR,
			ACCION
		}
		#endregion

		#region Inicializadores
		private void Start()
		{
			estadosBatalla = Procesado.ESPERAR;
			monstruos.AddRange(GameObject.FindGameObjectsWithTag("Monstruo"));
			heroes.AddRange(GameObject.FindGameObjectsWithTag("Heroe"));
		}
		#endregion

		#region Actualizadores
		private void Update()
		{
			switch (estadosBatalla)
			{
				case Procesado.ESPERAR:
					break;

				case Procesado.ELEGIR:
					break;

				case Procesado.ACCION:
					break;
			}
		}
		#endregion

		#region Metodos Publicos
		public void ColeccionAcciones(HandleTurno value)
		{
			acciones.Add(value);
		}
		#endregion
	}
}