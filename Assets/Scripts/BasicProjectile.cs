using UnityEngine;

public class BasicProjectile : MonoBehaviour
{

    private BasicEnemy target;
    private int damage;
    private float speed;

    public void Initialize(BasicEnemy target, int damage, float speed) {
        this.target = target;
        this.damage = damage;
        this.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            
            if (Vector3.Distance(transform.position, target.transform.position) < 0.2f)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        } else {
            Destroy(gameObject);
        }
    }
}
