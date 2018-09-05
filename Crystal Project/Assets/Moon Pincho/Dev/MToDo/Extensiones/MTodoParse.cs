//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoParse.cs (15/11/2016)                                              	    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:  Extension para analizar el sistema  							\\
// Fecha Mod:       15/11/2016                                                  \\
// Ultima Mod: Version inicial     												\\
//******************************************************************************\\

#region Librerias
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
#endregion

namespace MoonPincho.MEditor.ToDo
{
    /// <summary>
    /// <para>Extension para analizar el sistema </para>
    /// </summary>
    public class MTodoParse
    {
        #region Variables Privadas
        /// <summary>
        /// <para>Ruta del archivo a analizar</para>
        /// </summary>
        private string _pathArchivo;                            // Ruta del archivo a analizar
        /// <summary>
        /// <para>Texto que analizar</para>
        /// </summary>
        private string _texto;                                  // Texto que analizar
        /// <summary>
        /// <para>Tags que analizar</para>
        /// </summary>
        private string[] _tags;                                 // Tags que analizar
        #endregion

        #region Constructor
        /// <summary>
        /// <para>Constructor del MTodoParse</para>
        /// </summary>
        /// <param name="pathArchivo">Ruta del archivo</param>
        /// <param name="tags">Tags del archivo</param>
        public MTodoParse(string pathArchivo, string[] tags = null)// Constructor del MTodoParse<
        {
            _pathArchivo = pathArchivo;
            var file = new FileInfo(_pathArchivo);
            if (file.Exists)
                _texto = File.ReadAllText(pathArchivo);
            _tags = tags;
        }
        #endregion

        #region API
        /// <summary>
        /// <para>Analiza el sistema en busca de tickets</para>
        /// </summary>
        /// <returns>Los tickets</returns>
        public MTodoTickets[] Parse()// Analiza el sistema en busca de tickets
        {
            var file = new FileInfo(_pathArchivo);
            if (!file.Exists)
                return null;
            var temp = new List<MTodoTickets>();
            foreach (var tag in _tags)
            {
                var regex = new Regex(string.Format(@"(?<=\W|^)//(\s?{0})(.*)", tag), RegexOptions.IgnoreCase);
                var matches = regex.Matches(_texto);
                temp.AddRange(
                    from Match match in matches
                    let text = match.Groups[2].Value
                    let line = IndexALinea(match.Index)
                    select new MTodoTickets(text, "", tag, _pathArchivo, line));
            }
            return temp.ToArray();
        }
        #endregion

        #region Funcionalidad
        /// <summary>
        /// <para>Obtiene la linea</para>
        /// </summary>
        /// <param name="index">Indice</param>
        /// <returns>La linea de un indice</returns>
        private int IndexALinea(int index)// Obtiene la linea
        {
            return _texto.Take(index).Count(c => c == '\n') + 1;
        }
        #endregion
    }
}
