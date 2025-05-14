using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Range : Ability
{
    [SerializeField] protected byte damage;
    [Space]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private AssetReference projectile;
    [SerializeField] private float projectileSpeed;

    public override IEnumerator Use(Character owner)
    {
        owner.state = State.attack;
        var handle = Addressables.LoadAssetAsync<GameObject>(projectile);

        yield return new WaitForSeconds(prepare);
        yield return handle;

        GameObject newObject = Instantiate(handle.Result, shootPoint.position, shootPoint.rotation);
        var release = newObject.AddComponent<ReleaseOnDestroy>();
        release.handle = handle;

        Projectile newProjectile = newObject.GetComponent<Projectile>();
        newProjectile.ownerTag = transform.root.gameObject.tag;
        newProjectile.direction = shootPoint.forward;

        newProjectile.damage = damage;
        newProjectile.speed = projectileSpeed;

        yield return new WaitForSeconds(ending);
        owner.state = State.None;
    }
}
