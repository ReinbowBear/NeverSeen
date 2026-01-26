using System;

public interface ISaveable<T> where T : struct
{
    T GetData();
    void SetData(T data);
}
