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
using UnityEngine.UI;
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
		public GUIHero heroInput;
		[Header("Acciones")]
		public List<HandleTurno> acciones = new List<HandleTurno>();
		[Header("Unidades de Combate")]
		public List<GameObject> heroes = new List<GameObject>();
		public List<GameObject> monstruos = new List<GameObject>();
		public List<GameObject> heroesManager = new List<GameObject>();
		[Header("GUI")]
		public GameObject btnMonstruo;
		public Transform spacio;
		public GameObject panelAtaque;
		public GameObject panelSeleccionEnemigos;
		#endregion

		#region Variables Privadas
		private HandleTurno eleccionHeroe;
		#endregion

		#region Enums
		public enum Procesado
		{
			ESPERAR,
			ELEGIR,
			ACCION
		}

		public enum GUIHero
		{
			ACTIVADA,
			ESPERANDO,
			UNI1,
			UNI2,
			TERMINADA
		}
		#endregion

		#region Inicializadores
		private void Start()
		{
			estadosBatalla = Procesado.ESPERAR;
			monstruos.AddRange(GameObject.FindGameObjectsWithTag("Monstruo"));
			heroes.AddRange(GameObject.FindGameObjectsWithTag("Heroe"));
			heroInput = GUIHero.ACTIVADA;

			panelAtaque.SetActive(false);
			panelSeleccionEnemigos.SetActive(false);

			BtnsEnemigos();
		}
		#endregion

		#region Actualizadores
		private void Update()
		{
			switch (estadosBatalla)
			{
				case Procesado.ESPERAR:
					if (acciones.Count > 0)
					{
						estadosBatalla = Procesado.ELEGIR;
					}
					break;

				case Procesado.ELEGIR:
					GameObject u = GameObject.Find(acciones[0].Atacante);
					if (acciones[0].tipo == "Monstruo")
					{
						ControllerMonstruo controlM = u.GetComponent<ControllerMonstruo>();
						controlM.objetivo = acciones[0].target;
						controlM.estadoActual = ControllerMonstruo.EstadoTurno.ACCION;
					}

					if (acciones[0].tipo == "Heroe")
					{

					}

					estadosBatalla = Procesado.ACCION;
					break;

				case Procesado.ACCION:
					break;
			}

			switch (heroInput)
			{
				case (GUIHero.ACTIVADA):
					if (heroesManager.Count > 0)
					{

					}
					break;

				case GUIHero.ESPERANDO:
					break;

				case GUIHero.TERMINADA:
					break;
			}
		}
		#endregion

		#region Metodos
		public void ColeccionAcciones(HandleTurno value)
		{
			acciones.Add(value);
		}

		private void BtnsEnemigos()
		{
			foreach (GameObject monstruo in monstruos)
			{
				GameObject newBtn = Instantiate(btnMonstruo) as GameObject;
				BtnSeleccion btn = newBtn.GetComponent<BtnSeleccion>();

				ControllerMonstruo monstruoActual = monstruo.GetComponent<ControllerMonstruo>();

				Text btnText = newBtn.transform.Find("Text").gameObject.GetComponent<Text>();
				btnText.text = monstruoActual.unidad.nombre;

				btn.prefab = monstruo;

				newBtn.transform.SetParent(spacio, false);
			}
		}
		#endregion
	}
}