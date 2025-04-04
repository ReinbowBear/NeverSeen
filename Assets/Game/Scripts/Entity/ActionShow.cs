using UnityEngine;

public class ActionShow : MonoBehaviour
{
    [SerializeField] private MeshRenderer model;
    [Space]
    [SerializeField] private Material[] mat;

    public void SetMat(byte index)
    {
        model.material = mat[index];
    }
}
