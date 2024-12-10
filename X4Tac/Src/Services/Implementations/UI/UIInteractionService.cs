using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4Tac.Src.Services.Interfaces.UI;
using X4Tac.Src.ViewModels;
using X4Tac.Src.ViewModels.MainPage.Head;

namespace X4Tac.Src.Services.Implementations.UI
{
    public class UIInteractionService : IUIInteractionService
    {
        private WeakReference<ContentView> _viewReference;
        private WeakReference<ContentPage> _pageReference;
        private WeakReference<BaseViewModel> _modelReference;

        /// <inheritdoc />
        public void RegisterView(ContentView contentView)
        {
            _viewReference = new WeakReference<ContentView>(contentView);
        }

        /// <inheritdoc />
        public void RegisterPage(ContentPage contentPage)
        {
            _pageReference = new WeakReference<ContentPage>(contentPage);
        }

        /// <inheritdoc />
        public void RegisterModel(BaseViewModel viewModel)
        {
            _modelReference = new WeakReference<BaseViewModel>(viewModel);
        }

        /// <inheritdoc />
        public void UpdateBackgroundColor(string elementName, Color color)
        {
            UpdateProperty<VisualElement>(elementName, element =>
            {
                element.BackgroundColor = color;
            });
        }

        /// <inheritdoc />
        public void UpdateProperty<T>(string elementName, Action<T> updateAction) where T : VisualElement
        {
            if (_viewReference != null && _viewReference.TryGetTarget(out var page))
            {
                // Sucht das Element nach Name und castet es auf den erwarteten Typ
                if (page.FindByName<T>(elementName) is T element)
                {
                    updateAction(element);
                }
                else
                {
                    // Optional: Log oder Fehlerbehandlung, wenn das Element nicht gefunden wurde
                    // Debug.WriteLine($"Element mit Namen '{elementName}' wurde nicht gefunden oder hat den falschen Typ.");
                }
            }
        }


        /// <inheritdoc />
        public void SetUITitle(string value)
        {
            if (_modelReference != null && _modelReference.TryGetTarget(out var target) && target is MainPageViewModel inst)
            {
                inst.Title = value;
            }
        }

        /// <inheritdoc />
        public void SetContentView(ContentView contentView)
        {
            if (_modelReference != null && _modelReference.TryGetTarget(out var target) && target is MainPageViewModel inst)
            {
                inst.CurrentContentView = contentView;
            }
        }
    }
}
