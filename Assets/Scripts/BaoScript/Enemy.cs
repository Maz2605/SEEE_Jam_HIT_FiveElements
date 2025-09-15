using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHits = 2;  
    private int currentHits;

    private void OnEnable()
    {
        currentHits = 0; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            currentHits++;

            if (currentHits >= maxHits)
            {
                gameObject.SetActive(false); 
            }
        }
    }
}
