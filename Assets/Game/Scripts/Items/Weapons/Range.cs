using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Range : Weapon
{
    [Space]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private AssetReference projectile;
    [SerializeField] private float projectileSpeed;

    public override IEnumerator Attack()
    {
        isInAttack = true;
        var handle = Addressables.LoadAssetAsync<GameObject>(projectile);

        yield return new WaitForSeconds(prepare);
        yield return handle;

        GameObject newObject = Instantiate(handle.Result, shootPoint.position, shootPoint.rotation);
        var release = newObject.AddComponent<ReleaseOnDestroy>();
        release.handle = handle;


        Projectile newProjectile = handle.Result.GetComponent<Projectile>();
        newProjectile.damage = damage;
        newProjectile.speed = projectileSpeed;
        newProjectile.shootingEntity = transform.root.gameObject;


        yield return new WaitForSeconds(ending);
        isInAttack = false;
    }
}
