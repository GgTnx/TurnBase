using System.Collections;
using UnityEngine;

namespace PlayerScripts
{
    public class rocketBullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        [SerializeField] private int _damage;
        [SerializeField] private LayerMask _layerMask;
        private CombatController _combatController;
        private Vector2 _target;

        private void Awake()
        {
           

        }

        private void Start()
        {
            _combatController = FindObjectOfType<CombatController>();
            _target = _combatController._target;
        
   
            StartCoroutine(KillBulletByLifeTime());
        

        }
        private void Update() =>
            Move();
        private IEnumerator KillBulletByLifeTime()
        {
            yield return new WaitForSeconds(_lifeTime);

            Kill();
        }

        //  private void OnCollisionEnter2D(Collision2D col)
        // {
        //     
        //     Destroy(col.gameObject);
        //     Destroy(gameObject);
        // }
      

        private void Move()
        {
            var step = _speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _target,step);
        }
    
        private void Kill() =>
            Destroy(gameObject);
    }
}
