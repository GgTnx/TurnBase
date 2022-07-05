using System;
using System.Collections;
using UnityEngine;

namespace PlayerScripts
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        [SerializeField] private int _damage;
        private EnemyController _enemyController;
        private Vector2 _target;

        private void Awake()
        {
           

        }

        private void Start()
        {
            _enemyController = FindObjectOfType<EnemyController>();
            _target = _enemyController._target.transform.position;
        
   
            StartCoroutine(KillBulletByLifeTime());
        

        }
        private void Update() =>
            Move();
        private IEnumerator KillBulletByLifeTime()
        {
            yield return new WaitForSeconds(_lifeTime);

            Kill();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }

        private void Move()
        {
            var step = _speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _target,step);
        }
    
        private void Kill() =>
            Destroy(gameObject);
    }

}
