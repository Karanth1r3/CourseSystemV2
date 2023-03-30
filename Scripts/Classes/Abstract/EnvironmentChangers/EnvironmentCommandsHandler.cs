//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace course
//{
//    public class EnvironmentCommandsHandler : MonoBehaviour
//    {
//       IEnvironmentChanger[] buttons;
//        Stack<IEnvironmentChanger> commandsHistory;

//        public MultiPult()
//        {
//            buttons = new ICommand[2];
//            for (int i = 0; i < buttons.Length; i++)
//            {
//                buttons[i] = new NoCommand();
//            }
//            commandsHistory = new Stack<ICommand>();
//        }

//        public void SetCommand(int number, ICommand com)
//        {
//            buttons[number] = com;
//        }

//        public void PressButton(int number)
//        {
//            buttons[number].Execute();
//            // добавляем выполненную команду в историю команд
//            commandsHistory.Push(buttons[number]);
//        }
//        public void PressUndoButton()
//        {
//            if (commandsHistory.Count > 0)
//            {
//                ICommand undoCommand = commandsHistory.Pop();
//                undoCommand.Undo();
//            }
//        }
//    }
//}

