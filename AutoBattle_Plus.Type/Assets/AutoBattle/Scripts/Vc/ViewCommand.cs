using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace AutoBattle.Vc
{
    public abstract class ViewCommand
    {
        /** 番号 */
        private static int TotalNum;
        private int _num;
        
        /** 命令キュー */
        private static readonly Queue<ViewCommand> Queue = new();
        
        /** 現在の命令 */
        private static ViewCommand _currentCommand;
        
        /** キューの実行中か否か */
        private static bool _playingQueue;
        
        /** バトル */
        public static BattleLayer BattleLayer;
        /** デバッグ */
        public static DebugLayer DebugLayer;
        
        /** 命令の完了 */
        private static async UniTask Complete()
        {
            Queue.Dequeue();
            DebugLayer.UpdateViewCommandText(Queue);
            
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
        
        /** 命令の非同期実行 */
        protected abstract UniTask Execute();
        
        /** 命令追加 & 開始*/
        public void AddToQueue()
        {
            TotalNum += 1;
            _num = TotalNum;
            Queue.Enqueue(this);
            DebugLayer.UpdateViewCommandText(Queue);
            
            // 自動でキューを開始
            if (!_playingQueue)
            {
                _playingQueue = true;
                UpdateQueue().Forget();
            }
        }

        /** キューの完了待機 */
        public static async UniTask WaitQueueComplete()
        {
            await UniTask.WaitUntil(() => Queue.Count == 0);
        }
        
        public override string ToString()
        {
            return $"{GetType().Name}({_num})";
        }
        
    }
}