using UnityEngine;

public class ExplosionAutoDespawn : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;
    public void DespawnSelf()
    {
        PoolingManager.Despawn(gameObject);
    }
}
