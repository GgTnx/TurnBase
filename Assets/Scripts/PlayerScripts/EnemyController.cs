using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerScripts
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private CombatController _combatController;
        [SerializeField] private CameraMove _cameraMove;
        [SerializeField] private MoveController _moveController;
        [SerializeField] private UIController _uiController;
        [SerializeField] private PathFinder _pathFinder;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private GameObject _bulletPrefab;
        public List<Vector2> path1 = new List<Vector2>(); //potom zakrit
        [SerializeField] private Camera _camera;
        private float speed = 3f;
        public List<Enemy> _enemies; // zakrit
        public List<Player> _players; // zakrit
        public Player _target;

        private void OnEnable()
        {
            _uiController.ProverkaPress += SetActive;
        }

        private void SetActive()
        {
            _cameraMove.gameObject.SetActive(false);
            _combatController.gameObject.SetActive(false);
            _moveController.gameObject.SetActive(false);
            _enemies = FindEnemys();
            _players = FindPlayers();
            StartCoroutine(StartMove(_enemies));
        }

        private List<Enemy> FindEnemys()
        {
            var enemyList = new List<Enemy>();
            var enemy = FindObjectsOfType<Enemy>();
            foreach (var variableEnemy in enemy)
            {
                enemyList.Add(variableEnemy);
            }

            return enemyList;
        }

        private List<Player> FindPlayers()
        {
            var playerList = new List<Player>();
            var player = FindObjectsOfType<Player>();
            foreach (var variablePlayer in player)
            {
                playerList.Add(variablePlayer);
            }

            return playerList;
        }
        

       

        private void EnemyAttack(Enemy enemy)
        {
            
            Instantiate(_bulletPrefab, enemy.transform.position, Quaternion.identity); //dobavit' to4ku plevka
        }

        private void CalculateMoveGrid(Enemy enemy)
        {
            Vector2Int _enemy = Vector2Int.FloorToInt(enemy.transform.position);
            for (int x = _enemy.x - enemy._movePoint; x <= _enemy.x + enemy._movePoint; x++)
            {
                for (int y = _enemy.y - enemy._movePoint; y <= _enemy.y + enemy._movePoint; y++)
                {
                    var walkble = !Physics2D.OverlapCircle(new Vector2(x + 0.5f, y + 0.5f), 0.05f, _layerMask);
                    if (walkble)
                    {
                        if (_pathFinder.GetPath(_enemy, new Vector2(x, y)).Count <= enemy._movePoint)
                        {
                            path1.Add(new Vector2(x + 0.5f, y + 0.5f));
                        }
                    }
                }
            }
        }

        private IEnumerator StartMove(List<Enemy> enemies)
        {
            foreach (var enem in enemies)
            {
                SetTargetPlayer(_players, enem.transform.position);
                if (_target.transform.position.x <= enem.transform.position.x)
                {
                    enem._Renderer.flipX = true;
                }
                else
                {
                    enem._Renderer.flipX = false;
                }
                var finish =Vector2Int.FloorToInt(_target.transform.position);
                var start =Vector2Int.FloorToInt(enem.transform.position);
                var path =_pathFinder.GetPath(start, finish);
                _camera.transform.position = enem.transform.position + new Vector3(0,0,-1);
                yield return StartCoroutine(Move(enem, path));
               // EnemyAttack(enem);
                path1.Clear();
            }
        }
        private IEnumerator Move(Enemy start,List<Vector2> path)
        {
            path.RemoveAt(0);
            Vector2 pos = start.transform.position;
            for (int i = path.Count-1; i >=path.Count-5; i--) //tut meniau
            {
                if(i<0)
                    break;
                if (pos == path[0])
                    break;
                while (start.transform.position != new Vector3(path[i].x, path[i].y, 0f))
                {
                    var step =  speed * Time.deltaTime;
                    start.transform.position=Vector2.MoveTowards(start.transform.position, path[i],step);
                    yield return null;
                }
            }

        }

        private void SetTargetPlayer(List<Player> players, Vector2 enemy) // vrode nahodit
        {
            var distance = Vector2.Distance(enemy, players[0].transform.position);
            var tagret = players[0];
            foreach (var pl in players)
            {
                var dis = Vector2.Distance(enemy, pl.transform.position);
                if (dis < distance)
                {
                    distance = dis;
                    tagret = pl;
                }
            }
            _target = tagret;
        }


        private void OnDisable()
        {
            _uiController.ProverkaPress -= SetActive;
        }
    }
}