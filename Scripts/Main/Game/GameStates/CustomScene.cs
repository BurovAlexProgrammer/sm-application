using smApplication.Scripts.Main.Services;
using Cysharp.Threading.Tasks;

namespace smApplication.Scripts.Main.Game.GameStates
{
    public static partial class GameState
    {
        public class CustomScene : GameStateBase
        {
            public override async UniTask EnterState()
            {
                await UniTask.Yield();
                Services.Services.Get<GameStateService>().PrepareToPlay();
            }
        }
    }
}