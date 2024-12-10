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
    /// Zuständig für das Gameplay.
    /// </summary>
    public partial class GameBoardViewModel : BaseViewModel
    {
        private readonly IUIInteractionService _uiInteractionService;
        private readonly IViewFactory _viewFactory;
        private readonly IGameHandlerService _gameHandlerService;

        public GameBoardViewModel(IViewFactory viewFactory, IUIInteractionService uIInteractionService, IGameHandlerService gameHandlerService)
        {
            _viewFactory = viewFactory;
            _uiInteractionService = uIInteractionService;
            _gameHandlerService = gameHandlerService;
        }
    }
}
