using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class MoveController : MonoBehaviour
    {
        public enum State
        {
            None = 1,
            Select =2,
            Combat =3,
        }
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _selectedMask;
        [SerializeField] private LayerMask _highlight;
        [SerializeField] private PathFinder _pathFinder;
        [SerializeField] private CombatController _combatController;
        private Vector2 _offset = new Vector2(0.5f, 0.5f);
        public State _currentState = State.Select;
        public GameObject _selectedGameObject;
        private UIController _uiController;
        private List<Vector2> path1 = new List<Vector2>(); 
        private float speed = 2f;
        public bool _isMoving;
        public event Action<Player> OnSelected;
        public event Action OffSelected;
        public event Action MovePointChanged;
        public event Action Disable;

        private void OnEnable()
        {
            _combatController.gameObject.SetActive(false);
        }

        private void Start()
        {
            _uiController = FindObjectOfType<UIController>();
            _uiController.EndTurnePress += EndTurne; //попробовать сделать через интерфейсы
            _uiController.CombatPress += GetCombat;
        }


        private void Update()
        {
           
            if(_isMoving)
                return;
            if (Input.GetMouseButtonDown(0)&&_currentState == State.Select)
            {
                SelectPlayer();
 
            }

            if (Input.GetMouseButtonDown(1)&&_currentState == State.Select)
            {
            }

        
        }

        private void SelectPlayer()
        {

            Vector3 mouseInput = _camera.ScreenToWorldPoint(Input.mousePosition);
            mouseInput.z = 0f;
            Collider2D colider = Physics2D.OverlapPoint(mouseInput, _selectedMask);
            Collider2D collider = Physics2D.OverlapPoint(mouseInput, _highlight);
            if (collider != null)
            {
                var start =Vector2Int.FloorToInt(_selectedGameObject.transform.position);
                var finish = Vector2Int.FloorToInt(mouseInput);
                MoveTo(start,finish);

            }
            if (colider != null && _selectedGameObject != null&&_currentState == State.Select)
            {
                if(colider.gameObject ==_selectedGameObject )
                    return;
                Player setPlayer = _selectedGameObject.GetComponent<Player>();
                setPlayer.EndSelectred();
                OffSelected?.Invoke();
                _selectedGameObject = colider.gameObject;
                Player player = _selectedGameObject.GetComponent<Player>();
                player.GetSelected();
                OnSelected?.Invoke(player);
                _currentState = State.Select;
            }
            else if(colider != null)
            {
                _selectedGameObject = colider.gameObject;
                Player player = _selectedGameObject.GetComponent<Player>();
                player.GetSelected();
                OnSelected?.Invoke(player);
                _currentState = State.Select;
            }

       
        }

      

        private void MoveTo(Vector2 start,Vector2 finish)
        {
            var path = _pathFinder.GetPath(start, finish);
            path1 = path;
            StartCoroutine(Move());

        }

        private IEnumerator Move()
        {
            _isMoving = true;
            var player = _selectedGameObject.GetComponent<Player>();
            for (int i = path1.Count-1; i >=0; i--)
            {

                while (_selectedGameObject.transform.position != new Vector3(path1[i].x, path1[i].y, 0f))
                {
                    var step =  speed * Time.deltaTime;
                    _selectedGameObject.transform.position=Vector2.MoveTowards(_selectedGameObject.transform.position, path1[i],step);
                    yield return null;
                }

                player._movePoint--;

            }
            MovePointChanged?.Invoke();
            _isMoving = false;


        }
        private void EndTurne()
        {
            _selectedGameObject = null;
        }
        private void GetCombat()
        {
            _combatController.gameObject.SetActive(true);
        
        }
        private void OnDestroy()
        {
            _uiController.EndTurnePress -= EndTurne;
            _uiController.CombatPress -= GetCombat;
        }

        private void OnDisable()
        {
            Disable?.Invoke();
        }
    }
}