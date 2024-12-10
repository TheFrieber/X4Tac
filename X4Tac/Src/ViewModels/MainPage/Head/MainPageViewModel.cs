using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4Tac.Src.Services.Interfaces.Interaction;
using X4Tac.Src.Services.Interfaces.UI;
using X4Tac.Src.Views.MainPage.Content;

namespace X4Tac.Src.ViewModels.MainPage.Head
{
    /// <summary>
    /// Handling vom MainUI.
    /// </summary>
    public partial class MainPageViewModel : BaseViewModel
    {
        private readonly IViewFactory _viewFactory;
        private readonly IGameHandlerService _gameHandlerService;

        /// <summary>
        /// Game Content Container basierend auf ausgewähltem Tab.
        /// </summary>
        private View _currentContentView;
        public View CurrentContentView
        {
            get => _currentContentView;
            set => SetProperty(ref _currentContentView, value);
        }

        /// <summary>
        /// Main UI Titel um den ausgewählten Spieler anzuzeigen.
        /// </summary>
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public MainPageViewModel(IViewFactory viewFactory, IUIInteractionService uIInteractionService, IGameHandlerService gameHandlerService) 
        {
            _viewFactory = viewFactory;
            uIInteractionService.RegisterModel(this);
            _gameHandlerService = gameHandlerService;

            // Setze den Start-Content
            _currentContentView = _viewFactory.GetView<NewGameView>();
        }
    }
}
