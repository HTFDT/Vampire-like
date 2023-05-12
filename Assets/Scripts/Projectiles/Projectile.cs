using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData data;
    public Rigidbody2D rb;
    public DelegateChain<Collision2D> OnCollisionActions;
    public DelegateChain<Rigidbody2D> StartActions;
    public DelegateChain<Rigidbody2D> UpdateActions;
    public DelegateChain OnDestroyActions;
    public HashSet<ModifierCount> AdditionalModifiers;

    public void Init(ProjectileData pdata, IEnumerable<ModifierCount> additionalMods)
    {
        OnCollisionActions = new DelegateChain<Collision2D>();
        StartActions = new DelegateChain<Rigidbody2D>();
        UpdateActions = new DelegateChain<Rigidbody2D>();
        OnDestroyActions = new DelegateChain();

        data = pdata;
        OnCollisionActions.AddLast(data.OnCollisionActions);
        StartActions.AddLast(data.StartActions);
        UpdateActions.AddLast(data.UpdateActions);
        OnDestroyActions.AddLast(data.OnDestroyActions);
        AdditionalModifiers = additionalMods.ToHashSet();

        foreach (var mod in AdditionalModifiers.Concat(data.BaseModifiers)
                     .OrderBy(mod => (mod.modifier.Tag, mod.modifier.weight)))
            mod.modifier.ApplyTo(gameObject, mod.count);
    }

    private void Start()
    {
        StartActions.First?.Action(rb, StartActions.First?.Next);
    }

    private void FixedUpdate()
    {
        UpdateActions.First?.Action(rb, UpdateActions.First?.Next);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        OnCollisionActions.First?.Action(col, OnCollisionActions.First?.Next);
    }

    private void OnDestroy()
    {
        OnDestroyActions?.First?.Action(OnDestroyActions?.First?.Next);
    }
}