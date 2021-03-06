using System;
using UnityEngine;

namespace PlayerScripts
{
   public class CameraMove : MonoBehaviour
   {
      [SerializeField] private Camera _camera;
      private UIController _uiController;
      private Vector2 _currentPositionCamera;
      public float _up;
      public float _down;
      public float _left;
      public float _right ;

      private void Start()
      {
         _uiController = FindObjectOfType<UIController>();
         _uiController.EndTurnePress += EndTurne;
      }


      private void Update()
      {
         if (Input.GetMouseButton(1))
         {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - _camera.transform.position;
            _camera.transform.position += direction*Time.deltaTime;
         }
      
         _camera.transform.position = new Vector3(Mathf.Clamp(_camera.transform.position.x, _left, _right),Mathf.Clamp(_camera.transform.position.y, _down, _up),_camera.transform.position.z);
      }

      private void EndTurne()
      {
         
      }
   }
}
