using System;
using System.Collections;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerAtack : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _bulletPrefab;
         private Camera _camera;
        public Vector3 _target;
        private Player _player;


        private void Start()
        {
            _camera = FindObjectOfType<Camera>();
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            if (_player._currentState == Player.State.Combat&&Input.GetMouseButtonDown(0))
            {
                _target = _camera.ScreenToWorldPoint(Input.mousePosition);
                _target.z = 0f;
                CreateBullet();
            }
        }


        private IEnumerator Shoot()
        {

            yield return null;
        }
        
        
        
        
        private void CreateBullet() =>
            Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);

    }
}
