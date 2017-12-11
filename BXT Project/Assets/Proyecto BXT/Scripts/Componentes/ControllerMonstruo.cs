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
using System.Collections;
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
		[Header("Control")]
		public GameObject objetivo;
		#endregion

		#region Variables Privadas
		private Alexandria alexandria;
		private float cdActual = 0f;
		private float cdMax = 1f;
		private Vector3 posicionInicial;
		private bool isAccionInit = false;
		private float velAnim = 5.0f;
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
			SELECCIONANDO,
			ESPERANDO,
			ACCION,
			MUERTO
		}
		#endregion

		#region Inicializadores
		private void Start()
		{
			estadoActual = EstadoTurno.PROCESANDO;
			alexandria = GameObject.Find("Alexandria").GetComponent<Alexandria>();
			posicionInicial = this.transform.position;
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

				case EstadoTurno.SELECCIONANDO:
					SeleccionarAccion();
					estadoActual = EstadoTurno.ESPERANDO;
					break;

				case EstadoTurno.ESPERANDO:
					break;

				case EstadoTurno.ACCION:
					StartCoroutine(RealizarAccion());
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
				estadoActual = EstadoTurno.SELECCIONANDO;
			}
		}

		private void SeleccionarAccion()
		{
			HandleTurno accion = new HandleTurno();
			accion.Atacante = unidad.nombre;
			accion.tipo = "Monstruo";
			accion.goAtacante = this.gameObject;
			accion.target = alexandria.heroes[Random.Range(0, alexandria.heroes.Count)];
			alexandria.ColeccionAcciones(accion);
		}

		private IEnumerator RealizarAccion()
		{
			if (isAccionInit)
			{
				yield break;
			}

			isAccionInit = true;

			Vector3 posicionObjetivo = new Vector3(objetivo.transform.position.x ,objetivo.transform.position.y,objetivo.transform.position.z + 1.5f);

			while (MoverHaciaObjetivo(posicionObjetivo))
			{
				yield return null;
			}

			Vector3 posicionInicio = posicionInicial;

			while (MoverHaciaInicio(posicionInicio))
			{
				yield return null;
			}

			alexandria.acciones.RemoveAt(0);

			alexandria.estadosBatalla = Alexandria.Procesado.ESPERAR;

			isAccionInit = false;

			cdActual = 0f;
			estadoActual = EstadoTurno.PROCESANDO;

		}
		#endregion

		#region Funcionalidad
		private bool MoverHaciaObjetivo(Vector3 target)
		{
			return target != (transform.position = Vector3.MoveTowards(transform.position, target, velAnim * Time.deltaTime));
		}

		private bool MoverHaciaInicio(Vector3 target)
		{
			return target != (transform.position = Vector3.MoveTowards(transform.position, target, velAnim * Time.deltaTime));
		}
		#endregion
	}
}