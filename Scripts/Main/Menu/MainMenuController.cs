using sm_application.Scripts.Main.Events;
using Cysharp.Threading.Tasks;

namespace sm_application.Scripts.Main.Menu
{
    public class MainMenuController : MenuController
    {
        private void Start()
        {
            _ = EnterState(MenuStates.MainMenu);
        }


        public void QuitGame()
        {
            new QuitGameEvent().Fire();
        }

        public void StartNewGame()
        {
            new StartNewGameEvent().Fire();
        }

        protected override async UniTask EnterState(MenuStates newState)
        {
            await base.EnterState(newState);

            switch (newState)
            {
                case MenuStates.MainMenu:
                    break;
                case MenuStates.Settings:
                    break;
                case MenuStates.QuitGame:
                    break;
                case MenuStates.NewGame:
                    break;
            }
        }
        
        protected override async UniTask ExitState(MenuStates oldState)
        {
            await base.ExitState(oldState);
            
            switch (oldState)
            {
                case MenuStates.Settings:
                    break;
                case MenuStates.QuitGame:
                    break;
                case MenuStates.NewGame:
                    break;
            }
        }
    }
}