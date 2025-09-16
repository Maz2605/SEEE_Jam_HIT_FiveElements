using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyReference : MonoBehaviour
{
    public EnemyHealth Health { get; private set; }

    private void Awake()
    {
        Health = GetComponent<EnemyHealth>(); 
    }
}
