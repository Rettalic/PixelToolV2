using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private Stack<ICommand> historyStack     = new Stack<ICommand>();
    private Stack<ICommand> redoHistoryStack = new Stack<ICommand>();

    public void Execute(ICommand _command)
    {
        _command.Execute();
        historyStack.Push(_command);
    }

    public void Undo()
    {
        if(historyStack.Count > 0)
        {
            redoHistoryStack.Push(historyStack.Peek());
            historyStack.Pop().Undo();
        }
    }

    public void Redo()
    {
        if(redoHistoryStack.Count > 0)
        {
            historyStack.Push(redoHistoryStack.Peek());
            redoHistoryStack.Pop().Execute();
        }
    }
}
