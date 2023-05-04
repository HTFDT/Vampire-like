using System;
using System.Collections.Generic;


public delegate void ActionDelegate<T>(T value, ChainNode<T> next);

public class ChainNode<T>
{
    public ActionDelegate<T> Action;
    public ChainNode<T> Next;
    public bool IsLast => Next == null;
}

public class DelegateChain<T>
{
    public ChainNode<T> First;
    public ChainNode<T> Last;

    public void AddLast(ActionDelegate<T> action)
    {
        if (Last == First && Last == null)
            Last = First = new ChainNode<T> { Action = action };
        else
        {
            Last.Next = new ChainNode<T> { Action = action };
            Last = Last.Next;
        }
    }
}