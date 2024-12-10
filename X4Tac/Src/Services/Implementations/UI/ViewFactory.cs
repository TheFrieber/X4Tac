using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4Tac.Src.Services.Interfaces.UI;

namespace X4Tac.Src.Services.Implementations.UI
{

    /// <summary>
    /// Implementiert die IViewFactory-Schnittstelle und erstellt Views basierend auf der Benutzerrolle.
    /// </summary>
    public class ViewFactory : IViewFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initialisiert eine neue Instanz der ViewFactory-Klasse mit einem Dienstanbieter.
        /// </summary>
        /// <param name="serviceProvider">Der Dienstanbieter, der verwendet wird, um Instanzen der Views zu erhalten.</param>
        public ViewFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public T GetView<T>()
        {
            // Verwende DI, um die View zu erstellen
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
