using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4Tac.Src.Services.Interfaces.Interaction;
using X4Tac.Src.Services.Interfaces.UI;

namespace X4Tac.Src.ViewModels.MainPage.Content
{
    /// <summary>
    /// Zuständig Spiele zu starten.
    /// </summary>
    public partial class NewGameViewModel : BaseViewModel
    {
        private readonly IUIInteractionService _uIInteractionService;
        private readonly IViewFactory _viewFactory;
        private readonly IGameHandlerService _gameHandlerService;

        public NewGameViewModel(IViewFactory viewFactory, IUIInteractionService uIInteractionService, IGameHandlerService gameHandlerService) 
        {
            _viewFactory = viewFactory;
            _uIInteractionService = uIInteractionService;
            _gameHandlerService = gameHandlerService;
        }
    }
}
