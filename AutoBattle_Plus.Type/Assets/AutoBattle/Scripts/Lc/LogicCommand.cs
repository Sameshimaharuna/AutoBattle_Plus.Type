using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace AutoBattle.Lc
{
    public abstract class LogicCommand
    {
        /** 番号 */
        private static int TotalNum;
        private int _num;
        
        /** 命令キュー */
        private static readonly Queue<LogicCommand> Queue = new();
        
        /** バトル */
        public static BattleManager BattleManager;
        /** デバッグ */
        public static DebugLayer DebugLayer;
        
        /** 現在の命令 */
        private static LogicCommand _currentCommand;
        
        /** キューの実行中か否か */
        private static bool _playingQueue;
        
        /** 命令の完了 */
        private static async UniTask Complete()
        {
            Queue.Dequeue();
            DebugLayer.UpdateLogicCommandText(Queue);
            
            if (Queue.Count == 0)
            {
                _playingQueue = false;
                return;
            }
            await UpdateQueue();
        }
        
        /** キューの更新 */
        private static async UniTask UpdateQueue()
        {
            if (Queue.Count > 0 && _playingQueue)
            {
                _currentCommand = Queue.Peek();
                await _currentCommand.Execute();
                await Complete();
            }
        }
        
        /** 命令の実行 */
        protected abstract UniTask Execute();
        
        
        /** キューの開始 */
        public static async UniTask StartQueue()
        {
            _playingQueue = true;
            await UpdateQueue();
        }
        
        /** 命令追加 */
        public void AddToQueue()
        {
            TotalNum += 1;
            _num = TotalNum;
            Queue.Enqueue(this);
            DebugLayer.UpdateLogicCommandText(Queue);
        }
        
        public override string ToString()
        {
            return $"{GetType().Name}({_num})";
        }
        
    }
}