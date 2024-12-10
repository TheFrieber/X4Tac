using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4Tac.Src.ViewModels;

namespace X4Tac.Src.Services.Interfaces.UI
{
    /// <summary>
    /// Definiert einen Dienst zur Interaktion mit der Benutzeroberfläche (UI), um Elemente zu registrieren und deren Eigenschaften zu aktualisieren.
    /// </summary>
    public interface IUIInteractionService
    {
        /// <summary>
        /// Registriert eine Seite (ContentView), mit der interagiert werden soll, damit UI-Elemente aktualisiert oder gesteuert werden können.
        /// </summary>
        /// <param name="contentView">Die zu registrierende Seite (ContentView).</param>
        void RegisterView(ContentView contentView);

        /// <summary>
        /// Registriert eine Seite (ContentPage), mit der interagiert werden soll, damit UI-Elemente aktualisiert oder gesteuert werden können.
        /// </summary>
        /// <param name="contentPage">Die zu registrierende Seite (ContentPage).</param>
        void RegisterPage(ContentPage contentPage);

        /// <summary>
        /// Registriert das ViewModel der MainPage, um die Interaktion mit der MainPage und deren Daten zugänglich zu machen.
        /// </summary>
        /// <param name="viewModel">Das ViewModel, das für die MainPage verwendet wird.</param>
        void RegisterModel(BaseViewModel viewModel);

        /// <summary>
        /// Aktualisiert eine Eigenschaft eines UI-Elements auf der registrierten Seite basierend auf dem Elementnamen.
        /// </summary>
        /// <typeparam name="T">Der Typ des UI-Elements, das aktualisiert werden soll.</typeparam>
        /// <param name="elementName">Der Name des Elements, das aktualisiert werden soll.</param>
        /// <param name="updateAction">Eine Aktion, die auf das UI-Element angewendet wird, um es zu aktualisieren.</param>
        void UpdateProperty<T>(string elementName, Action<T> updateAction) where T : VisualElement;



        #region MainPage Handling

        /// <summary>
        /// MainPage Handling:
        /// Setzt den Titel der MainPage, um eine dynamische Änderung des Seiten-Titels zu ermöglichen.
        /// </summary>
        /// <param name="value">Der Titel, der auf der MainPage angezeigt werden soll.</param>
        void SetUITitle(string value);

        /// <summary>
        /// MainPage Handling
        /// Setzt den Content fest, der auf der UI angezeigt werden soll
        /// </summary>
        /// <param name="value">View der angezeigt werden soll</param>
        void SetContentView(ContentView value);

        #endregion
    }
}
