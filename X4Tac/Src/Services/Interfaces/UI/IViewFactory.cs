using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4Tac.Src.Services.Interfaces.UI
{
    /// <summary>
    /// Definiert die Schnittstelle für eine ViewFactory, die eine View basierend auf der Benutzerrolle erstellt.
    /// </summary>
    public interface IViewFactory
    {
        /// <summary>
        /// Holt die View-Instanz des angegebenen Typs T.
        /// Diese Methode wird verwendet, um eine Instanz der entsprechenden View zu erhalten,
        /// die anhand des Typs T erstellt oder zurückgegeben wird.
        /// </summary>
        /// <typeparam name="T">Der Typ der View, die abgerufen werden soll.</typeparam>
        /// <returns>Die Instanz der View vom Typ T.</returns>
        T GetView<T>();
    }
}
