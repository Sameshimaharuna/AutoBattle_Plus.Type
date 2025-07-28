using System.Collections.Generic;
using AutoBattle.Lc;
using AutoBattle.Vc;
using UnityEngine;

namespace AutoBattle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private BattleLayer battleLayer;
        [SerializeField] private DebugLayer debugLayer;
        
        /** 味方 */
        public readonly List<Unit> AllyList = new();
        /** 敵 */
        public readonly List<Unit> EnemyList = new();

        private async void Start()
        {
            LogicCommand.BattleManager = this;
            LogicCommand.DebugLayer = debugLayer;
            ViewCommand.BattleLayer = battleLayer;
            ViewCommand.DebugLayer = debugLayer;
            
            // バトル開始のコマンド生成
            new LcStartBattle().AddToQueue();
            // 最初のターン処理
            ExecuteTurn();
            
            Debug.Log("バトル開始");
            // キュー開始
            await LogicCommand.StartQueue();
            Debug.Log("バトル終了");
        }

        /** ユニット追加 */
        public void AddUnit(Unit unit, bool isAlly)
        {
            if (isAlly)
            {
                AllyList.Add(unit);
            }
            else
            {
                EnemyList.Add(unit);
            }
        }
        
        /** ターン実行 */
        public void ExecuteTurn()
        {
            new LcExecuteTurn().AddToQueue();
        }
        
    }
}