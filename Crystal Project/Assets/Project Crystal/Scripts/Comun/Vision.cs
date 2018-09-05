//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// Vision.cs (16/12/2016)													    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:	Controla la vision del enemigo en el campo						\\
// Fecha Mod:		16/12/2016													\\
// Ultima Mod:	Version Inicial													\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections;
#endregion

namespace MoonPincho.Sistemas.Agne
{
    /// <summary>
    /// <para>Controla la vision del enemigo en el campo</para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Sistemas/SAgne/Interno/Vision")]
    public class Vision : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// <para>Radio de la vision</para>
        /// </summary>
        public float radio;                                     // Radio de la vision
        /// <summary>
        /// <para>Angulo de la vision</para>
        /// </summary>
        [Range(0,360)]
        public float angulo;                                    // Angulo de la vision
        /// <summary>
        /// <para>Mascara del objetivo</para>
        /// </summary>
        public LayerMask mascaraObjetivo;                       // Mascara del objetivo
        /// <summary>
        /// <para>Mascara de los obstaculos</para>
        /// </summary>
        public LayerMask mascaraObstaculos;                     // Mascara de los obstaculos

        public Transform objetivoVision;
        #endregion

        private void Start()
        {
            StartCoroutine("BuscarObjetivosDelay",1.0f);
        }

        #region Funcionalidad
        IEnumerator BuscarObjetivosDelay(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                BuscarObjetivosVisibles();
            }
        }

        /// <summary>
        /// <para>Direccion del angulo</para>
        /// </summary>
        /// <param name="grados">Grados del angulo</param>
        /// <param name="anguloGlobal">Si el angulo es global o estatico</param>
        /// <returns></returns>
        public Vector3 dirDelAngulo(float grados,bool anguloGlobal)// Direccion del angulo
        {
            if (!anguloGlobal)
            {
                grados += transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(grados * Mathf.Deg2Rad), 0, Mathf.Cos(grados * Mathf.Deg2Rad));
        }

        public void BuscarObjetivosVisibles()
        {
            Collider[] objetivosEnRadio = Physics.OverlapSphere(transform.position, radio, mascaraObjetivo);

            for(int i = 0;i < objetivosEnRadio.Length;i++)
            {
                Transform objetivo = objetivosEnRadio[i].transform;
                Vector3 dirObjetivo = (objetivo.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirObjetivo) < angulo / 2)
                {
                    float distanciaObjetivo = Vector3.Distance(transform.position, objetivo.position);

                    if (!Physics.Raycast(transform.position, dirObjetivo, distanciaObjetivo, mascaraObstaculos))
                    {
                        objetivoVision = objetivo;
                    }
                }
            }
        }
        #endregion
    }
}