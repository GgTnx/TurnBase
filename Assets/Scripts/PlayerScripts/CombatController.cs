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
        public Vector2 _spawnPoint;
        public Vector2 _target;

        private void Awake()
        {
           
        }

        private void OnEnable()
        {
            if (_moveController._selectedGameObject != null)
            {
                _spawnPoint = _moveController._selectedGameObject.gameObject.transform.Find("BulletPoint").position;
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
                
                CreateBullet();
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
