using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlayerScripts
{
    public class rocketBullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        [SerializeField] private int _damage;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Tilemap _rocks;
        [SerializeField] private Tile _ashes;
        private CombatController _combatController;
        private Vector2 _target;

        private void Awake()
        {
           

        }

        private void Start()
        {
            _combatController = FindObjectOfType<CombatController>();
            _target = _combatController._target;
        
   
                //StartCoroutine(KillBulletByLifeTime());
        

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
            if (col.gameObject.CompareTag("TestTag"))
            {
                print(col.contacts[0].point);

                var point = _rocks.WorldToCell(col.contacts[0].point);
                _rocks.SetTile(point,_ashes);
                
                print(point);


                //_rocks.SetTile(point,_ashes);
            }
            // Destroy(col.gameObject);
            // Destroy(gameObject);
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
