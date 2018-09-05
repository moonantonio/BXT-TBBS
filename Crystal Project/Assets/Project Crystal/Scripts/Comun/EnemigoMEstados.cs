//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// clsEnemigo.cs (10/11/2016)                                              	    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Maquina de estados de los enemigos               				\\
// Fecha Mod:       10/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections;
#endregion

namespace MoonPincho.Sistemas.SFreya
{
    /// <summary>
    /// <para>Maquina de estados de los enemigos  </para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Sistemas/SFreya/Interno/EnemigoMEstados")]
    public class EnemigoMEstados : MonoBehaviour
    {
        public clsEnemigo enemigo;
        public EstadoTurno estadoTurno;
        private SFreya sFreya;
        private bool estadoSalud = true;

        private float actualCD = 0;
        private float maxCD = 5.0f;

        private Vector3 posicionInicial;
        private bool iniciarAccion = false;
        public GameObject posicionAliado;

        private float velAnimacion = 5.0f;

        public UIProgressBar progressBar;

        public GameObject selector;

        public void Start()
        {
            selector.SetActive(false);
            sFreya = GameObject.Find("SFreya").GetComponent<SFreya>();
            estadoTurno = EstadoTurno.Procesando;
            posicionInicial = transform.position;  
        }

        public void Update()
        {
            switch (estadoTurno)
            {
                case EstadoTurno.Procesando:
                    ActualizarProgressBar();
                    break;

                case EstadoTurno.EscogerAccion:
                    ElegirAccion();
                    estadoTurno = EstadoTurno.Esperando;
                    break;

                case EstadoTurno.Esperando:
                    // TODO Estado idle
                    break;

                case EstadoTurno.Accion:
                    if (enemigo.actualHP > 0)
                    {
                        StartCoroutine(TiempoDeAccion());
                    }
                    else
                    {
                        estadoTurno = EstadoTurno.Muerte;
                    }
                    break;

                case EstadoTurno.Muerte:
                    if (!estadoSalud)
                    {
                        return;
                    }
                    else
                    {
                        // Cambiar tag
                        this.gameObject.tag = "EnemigoMuerto";

                        // No atacable por el enemigo
                        sFreya.enemigos.Remove(this.gameObject);

                        // Desactivar el selector
                        selector.SetActive(false);

                        // Quitar procesos del proximo turno
                        if (sFreya.enemigos.Count > 0)
                        {
                            for (int n = 0; n < sFreya.proxTurno.Count; n++)
                            {
                                if (sFreya.proxTurno[n].prefabEnemigo == this.gameObject)
                                {
                                    sFreya.proxTurno.Remove(sFreya.proxTurno[n]);
                                }

                                if (sFreya.proxTurno[n].objetivoEnemigo == this.gameObject)
                                {
                                    sFreya.proxTurno[n].objetivoEnemigo = sFreya.enemigos[Random.Range(0, sFreya.enemigos.Count)];

                                }
                            }
                        }

                        // Cambiar color / Animacion
                        this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(105, 105, 105, 255);

                        // Activar estado de salud
                        estadoSalud = false;

                        // Reseteo de botones
                        sFreya.CrearBtnEnemigos();

                        // Comprobar estado
                        sFreya.estadoBatalla = Freya.ComprobarVida;
                    }
                    break;
            }
        }

        private void ActualizarProgressBar()
        {
            actualCD = actualCD + Time.deltaTime;
            float calculoCD = actualCD / maxCD;
            progressBar.value = Mathf.Clamp(calculoCD, 0, 1);

            if (actualCD >= maxCD)
            {
                estadoTurno = EstadoTurno.EscogerAccion;

            }
        }

        private void ElegirAccion()
        {
            clsControlTurno ataque = new clsControlTurno();
            // Obtener nombre del enemigo
            ataque.enemigo = enemigo.nombre;

            // Obtener el tipo
            ataque.tipo = "Enemigo";

            // Obtener el prefab del enemigo
            ataque.prefabEnemigo = this.gameObject;

            // Obtener el objetivo del enemigo entre todos los aliados
            ataque.objetivoEnemigo = sFreya.aliados[Random.Range(0, sFreya.aliados.Count)];

            // Realizar el ataque elegido
            int tempNum = Random.Range(0, enemigo.ataques.Count);
            ataque.ataqueElegido = enemigo.ataques[tempNum];

            // Atacar
            sFreya.Acciones(ataque);
        }

        private IEnumerator TiempoDeAccion()
        {
            if (iniciarAccion)
            {
                yield break;
            }

            iniciarAccion = true;

            //Movimiento hacia la posicion del objetivo
            Vector3 _posicionAliado = new Vector3(posicionAliado.transform.position.x - 1.5f, 
                                                    posicionAliado.transform.position.y, 
                                                    posicionAliado.transform.position.z);

            while (MoverHaciaEnemigo(_posicionAliado)){yield return null;}

            // Esperando la habilidad

            yield return new WaitForSeconds(0.5f);

            // Pasando valores
            RealizarDamage();

            // Movimiento hacia la posicion inicial

            Vector3 primeraPos = posicionInicial;
            while (MoverHaciaInicio(primeraPos)){yield return null;}

            // Quitar este proceso de la lista de SFreya

            sFreya.proxTurno.RemoveAt(0);

            // Resetear SFreya

            sFreya.estadoBatalla = Freya.Esperando;

            // Fin de la corutina

            iniciarAccion = false;

            // Resetear el estado del enemigo

            actualCD = 0.0f;
            estadoTurno = EstadoTurno.Procesando;
        }

        private bool MoverHaciaEnemigo(Vector3 objetivo)
        {
            return objetivo != (transform.position = Vector3.MoveTowards(transform.position, objetivo, velAnimacion * Time.deltaTime));
        }

        private bool MoverHaciaInicio(Vector3 objetivo)
        {
            return objetivo != (transform.position = Vector3.MoveTowards(transform.position, objetivo, velAnimacion * Time.deltaTime));
        }

        /// <summary>
        /// <para>Hace daño al enemigo</para>
        /// </summary>
        private void RealizarDamage()// Hace daño al enemigo
        {
            float calculoDamage = enemigo.actualAtack + sFreya.proxTurno[0].ataqueElegido.fuerzaAtaque;
            posicionAliado.GetComponent<AliadoMEstados>().RecibirDamage(calculoDamage);
        }

        /// <summary>
        /// <para>Recibe daño del enemigo</para>
        /// </summary>
        public void RecibirDamage(float damage)// Recibe daño del enemigo
        {
            enemigo.actualHP -= damage;
            if (enemigo.actualHP <= 0)
            {
                enemigo.actualHP = 0;
                estadoTurno = EstadoTurno.Muerte;
            } 
        }

    }
}
