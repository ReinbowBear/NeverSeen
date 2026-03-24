using System.Collections.Generic;

public class Pool<T> where T : new()
{
    private Queue<T> freeObjects  = new();

    public T Get()
    {
        T obj;

        if (freeObjects.Count > 0)
        {
            obj = freeObjects.Dequeue();
        }
        else
        {
            obj = new T();
        }

        return obj;
    }


    public void Return(T obj)
    {
        freeObjects.Enqueue(obj);
    }
}
