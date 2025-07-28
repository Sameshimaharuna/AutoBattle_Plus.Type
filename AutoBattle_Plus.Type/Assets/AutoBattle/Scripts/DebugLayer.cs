using System.Collections.Generic;
using System.Linq;
using AutoBattle.Lc;
using AutoBattle.Vc;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattle
{
    public class DebugLayer : MonoBehaviour
    {
        [SerializeField] private Text logicCommandText;
        [SerializeField] private Text viewCommandText;
        
        private static void UpdateCommandText<T>(IReadOnlyCollection<T> logicCommands, Text commandText) where T : class
        {
            var commandsCount = logicCommands.Count;
            var reversedCommands = logicCommands.Reverse();
            commandText.text = string.Join("\n", reversedCommands.Select((command, index) =>
            {
                var line = command.ToString();
                if (index == commandsCount - 1)
                {
                    line = $"<color=yellow>{line}</color>";
                }
                return line;
            }));
        }
        
        public void UpdateLogicCommandText(Queue<LogicCommand> logicCommands)
        {
            UpdateCommandText(logicCommands, logicCommandText);
        }
        
        public void UpdateViewCommandText(Queue<ViewCommand> viewCommands)
        {
            UpdateCommandText(viewCommands, viewCommandText);
        }
    }
}