using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTestScript : MonoBehaviour, ISubscriber, INotifier
{
    [SerializeField] private GameEvent incoming;
    [SerializeField] private GameEvent outgoing;
    [SerializeField] public GameEvent Notifier { get { return outgoing; } }
    [SerializeField] string getName => incoming.ToString();
}

// Ignore this
class LinkedListStack<T>
{
    class Node
    {
        public T data;
        public Node previous;
    }

    Node node;

    LinkedListStack(T newData)
    {
        if (node == null)
        {
            node = new Node();
            node.data = newData;
        }
    }

    public void Push(T newData)
    {
        Node newNode = new Node();
        newNode.data = newData;
        node.previous = node;
        node = newNode;
    }

    public T Pop()
    {
        Node placeHolder = node;
        node = node.previous;
        return placeHolder.data;
    }
}