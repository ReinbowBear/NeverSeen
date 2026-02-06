using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MapDisplayer : MonoBehaviour
{
    private IEnumerator DisplayMap()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("Tile");
        yield return handle;
//
        //foreach (var tileData in tilesData.Values)
        //{
        //    Vector3 pos = CubeToWorld(tileData.CubeCoord, hexWidth);
        //    pos.y = (int)tileData.TileHeightType * HexY;
//
        //    GameObject obj = Instantiate(handle.Result);
        //    obj.transform.position = pos;
        //    obj.transform.SetParent(MapRoot);
//
        //    Tile component = obj.GetComponent<Tile>();
        //    component.tileData = tileData;
//
        //    mapData.Tiles.Add(tileData.CubeCoord, component);
        //}
        //tilesData = null;
        //freeTiles = null;
    }
}