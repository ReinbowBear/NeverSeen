using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute
{ 
    // [SerializeField, ReadOnly] private int debugValue; // будет отображатся в инспекторе но будет нередактируемым
}
