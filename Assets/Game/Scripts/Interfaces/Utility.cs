using System;

public interface ISaveable<T> where T : struct
{
    T GetData();
    void SetData(T data);
}


public interface IHaveBar
{
    event Action<int, int> OnChangeValue;
}

public interface IHaveNumber
{
    event Action<int> OnChangeValue;
}