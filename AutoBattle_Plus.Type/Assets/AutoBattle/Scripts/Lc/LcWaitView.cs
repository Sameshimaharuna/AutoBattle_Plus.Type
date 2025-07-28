using AutoBattle.Vc;
using Cysharp.Threading.Tasks;

namespace AutoBattle.Lc
{
    public class LcWaitView : LogicCommand
    {
        protected override async UniTask Execute()
        {
            await ViewCommand.WaitQueueComplete();
        }
    }
}