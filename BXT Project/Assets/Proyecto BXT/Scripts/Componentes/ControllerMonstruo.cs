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
		#region Variables Publicas
		[Header("Info Unidad")]
		public Unidad unidad;
		public ElementoMonstruo elemento;
		public TipoMonstruo tipo;
		[Header("Estado")]
		public EstadoTurno estadoActual;
		#endregion

		#region Variables Privadas
		private Alexandria alexandria;
		private float cdActual = 0f;
		private float cdMax = 5f;
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

		public enum EstadoTurno
		{
			PROCESANDO,
			AGREGANDO,
			ESPERANDO,
			SELECCIONANDO,
			ACCION,
			MUERTO
		}
		#endregion

		#region Inicializadores
		private void Start()
		{
			estadoActual = EstadoTurno.PROCESANDO;
			alexandria = GameObject.Find("Alexandria").GetComponent<Alexandria>();
		}
		#endregion

		#region Actualizadores
		private void Update()
		{
			switch (estadoActual)
			{
				case EstadoTurno.PROCESANDO:
					ActualizarProgressBar();
					break;

				case EstadoTurno.AGREGANDO:
					break;

				case EstadoTurno.ESPERANDO:
					break;

				case EstadoTurno.SELECCIONANDO:
					break;

				case EstadoTurno.ACCION:
					break;

				case EstadoTurno.MUERTO:
					break;
			}
		}
		#endregion

		#region Metodos
		private void ActualizarProgressBar()
		{
			cdActual = cdActual + Time.deltaTime;

			if (cdActual >= cdMax)
			{
				estadoActual = EstadoTurno.AGREGANDO;
			}
		}
		#endregion
	}
}