using System;
using System.Collections;
using UnityEngine;

namespace PlayerScripts
{
    public class CombatController : MonoBehaviour  // povernut' raketu v storonu vraga
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private MoveController _moveController;
        [SerializeField] private Cursore1 _cursore1;
        public Vector2 _spawnPoint;
        public Vector2 _target;
        private Player _player;
        


        private void OnEnable()
        {
            if (_moveController._selectedGameObject != null)
            {
                _spawnPoint = _moveController._selectedGameObject.gameObject.transform.Find("BulletPoint").position;
                _player = _moveController._selectedGameObject.gameObject.GetComponent<Player>();
            }
            else
            {
                print("viberi igroka"); // na bolshoi ekran.
            }

            _moveController.gameObject.SetActive(false);
        }

        private void Start()
        {
           
  
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _target = _camera.ScreenToWorldPoint(Input.mousePosition);
                if (_player!=null&&_player._canShoot)
                {
                    CreateBullet(); /// вот тут через свитч вид стрельбы
                    _player._canShoot = false;
                    _cursore1.ChangeCursore(0);
                }
                
   
            }
            if (Input.GetMouseButtonDown(1))
            {
                _moveController.gameObject.SetActive(true);
            }
            
        }



        private void CreateBullet()
        {
            Instantiate(_bulletPrefab, _spawnPoint, Quaternion.identity);

        }
       

        
          
    }
}
