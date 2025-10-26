using UnityEngine;

public class MeshButtonView : MonoBehaviour
{
    public Transform modelPos;
    [Space]
    public Vector3 modelRotation = new Vector3(0, 90, 0);
    public float targetSize = 1.0f;

    private GameObject currentModel;


    public void SetModel(GameObject modelPrefab) // моделька появляется все дела. но она крошечная и поза камерой что может их вообще рендерить
    {
        if (currentModel != null) Destroy(currentModel);

        currentModel = Instantiate(modelPrefab, modelPos);
        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.Euler(modelRotation);

        NormalizeModelSize(currentModel);
    }

    private void NormalizeModelSize(GameObject model) // по идеи модельки в юай должны отображатся отдельной камерой а ещё есть некая RawImage которая может отображать в виде текстуры то что видит камера(вроде как)
    {
        var renderers = model.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return;

        Bounds bounds = renderers[0].bounds;
        foreach (var rend in renderers)
        {
            bounds.Encapsulate(rend.bounds);
        }

        float maxDimension = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
        if (maxDimension == 0) maxDimension = 1f;

        float scaleFactor = targetSize / maxDimension;
        model.transform.localScale = Vector3.one * scaleFactor;

        Vector3 offset = bounds.center - model.transform.position;
        model.transform.localPosition = -offset * scaleFactor;
    }
}
