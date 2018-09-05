//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoTickets.cs (15/11/2016)                                              	\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Clase de los tickets del Todo    								\\
// Fecha Mod:       15/11/2016                                                  \\
// Ultima Mod:  Version Inicial    												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.MEditor.ToDo
{
    /// <summary>
    /// <para>Clase de los tickets del Todo </para>
    /// </summary>
    [System.Serializable]
    public class MTodoTickets
    {
        #region Variables Publicas
        #region Propiedades
        /// <summary>
        /// <para>Texto del ticket</para>
        /// </summary>
        public string Texto;                                    // Texto del ticket
        /// <summary>
        /// <para>Nota del ticket</para>
        /// </summary>
        public string Nota;                                     // Nota del ticket
        /// <summary>
        /// <para>Etiqueta del ticket</para>
        /// </summary>
        public string Tag;                                      // Etiqueta del ticket
        /// <summary>
        /// <para>Archivo del ticket</para>
        /// </summary>
        public string Archivo;                                  // Archivo del ticket
        /// <summary>
        /// <para>Linea del ticket</para>
        /// </summary>
        public int Linea;                                       // Linea del ticket
        #endregion

        /// <summary>
        /// <para>Ruta a mostrar el archivo</para>
        /// </summary>
        public string PathAMostrar;                             // Ruta a mostrar el archivo
        #endregion

        #region Constructor
        /// <summary>
        /// <para>Constructor de la clase MTodoTickets</para>
        /// </summary>
        /// <param name="texto">Texto del ticket</param>
        /// <param name="nota">Nota del ticket</param>
        /// <param name="tag">Etiqueta del ticket</param>
        /// <param name="archivo">Archivo del ticket</param>
        /// <param name="linea">Linea del ticket</param>
        public MTodoTickets(string texto, string nota, string tag, string archivo, int linea)// Constructor de la clase MTodoTickets
        {
            Texto = texto;
            Nota = nota;
            Tag = tag;
            Archivo = archivo;
            Linea = linea;

            PathAMostrar = Archivo.Remove(0, Application.dataPath.Length - 6).Replace("\\", "/") + "(" + Linea + ")";
        }
        #endregion

        #region Metodos
        /// <summary>
        /// <para>Comprueba si el obj es igual a este</para>
        /// </summary>
        /// <param name="obj">Objeto a comprobar</param>
        /// <returns>Resultado de la igualacion</returns>
        public override bool Equals(object obj)// Comprueba si el obj es igual a este
        {
            var x = this;
            var y = (MTodoTickets)obj;
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return string.Equals(x.Texto, y.Texto) && string.Equals(x.Nota, y.Nota) && string.Equals(x.Tag, y.Tag) && string.Equals(x.Archivo, y.Archivo) && x.Linea == y.Linea;
        }

        /// <summary>
        /// <para>Obtener el hash del ticket especifico</para>
        /// </summary>
        /// <returns>El Hash Code del ticket especifico</returns>
        public override int GetHashCode()// Obtener el hash del ticket especifico
        {
            unchecked
            {
                var obj = this;
                var hashCode = (obj.Texto != null ? obj.Texto.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.Nota != null ? obj.Nota.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.Tag != null ? obj.Tag.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.Archivo != null ? obj.Archivo.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ obj.Linea;
                return hashCode;
            }
        }
        #endregion
    }
}
