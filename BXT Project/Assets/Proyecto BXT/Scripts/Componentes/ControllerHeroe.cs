//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// ControllerHeroe.cs (11/12/2017)												\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Control del heroe											\\
// Fecha Mod:		11/12/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace MoonAntonio
{
	/// <summary>
	/// <para>Control del heroe</para>
	/// </summary>
	public class ControllerHeroe : MonoBehaviour
	{
		#region Variables Publicas
		[Header("Info Unidad")]
		public Unidad unidad;
		[Header("Estado")]
		public EstadoTurno estadoActual;
		[Header("Progress")]
		public Image progress;
		#endregion

		#region Variables Privadas
		private float cdActual = 0f;
		private float cdMax = 5f;
		#endregion

		#region Enums
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
			float cdProcesado = cdActual / cdMax;
			progress.transform.localScale = new Vector3(Mathf.Clamp(cdProcesado,0,1),progress.transform.localScale.y,progress.transform.localScale.z);
			if (cdActual >= cdMax)
			{
				estadoActual = EstadoTurno.AGREGANDO;
			}
		}
		#endregion
	}
}