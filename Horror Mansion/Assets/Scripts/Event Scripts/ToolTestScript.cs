using UnityEngine;

public class ToolTestScript : MonoBehaviour, ISubscriber, INotifier
{
    [SerializeField] GameEvent incoming;
    [SerializeField] GameEvent outgoing;
    public GameEvent Subscriber => incoming;
    public GameEvent Notifier => outgoing;
    string ISubscriber.GetName() => nameof(incoming);
    string INotifier.GetName() => nameof(outgoing);
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
        Node newNode = new();
        newNode.data = newData;
        newNode.previous = node;
        node = newNode;
    }

    public T Pop()
    {
        Node placeHolder = node;
        node = node.previous;
        return placeHolder.data;
    }
}