using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlayerScripts
{
    public class MoveGrid : MonoBehaviour
    {
        [SerializeField] private Tilemap _highLightMap;
        [SerializeField] private Tile _highLightTile;
        [SerializeField] private MoveController _controller;
        [SerializeField] private LayerMask _layerMask;
        private Player _playerPosition;
        public List<Vector2> _moveList = new List<Vector2>();
        public PathFinder _Finder;
        private UIController _uiController;
        


        private void Start()
        {
            _uiController = FindObjectOfType<UIController>();
            _controller.OnSelected += GetPositionPlayer;
            _controller.OffSelected += ClearHighLight;
            _controller.Disable += ClearHighLight;
            _controller.MovePointChanged += UpdateMoveGrid;
            _uiController.EndTurnePress += EndTurne;
            _uiController.CombatPress += GetCombat;
        }

        private void GetCombat()
        {
        }
        private void EndTurne()
        {
           ClearHighLight();
        }

        private void Update()
        {
            
        }

        public void SetHighLight(List<Vector2> positions)  // zakrit
        {
            foreach (Vector2 vector2 in positions)
            {
                Vector3Int tilePosition = _highLightMap.WorldToCell(vector2);

                _highLightMap.SetTile(tilePosition, _highLightTile);
            }
        }

        private void CalculateMoveGrid()
        {
            Vector2Int player = Vector2Int.FloorToInt(_playerPosition.transform.position);
            for (int x = player.x - _playerPosition._movePoint; x <= player.x + _playerPosition._movePoint; x++)
            {
                for (int y = player.y - _playerPosition._movePoint; y <= player.y + _playerPosition._movePoint; y++)
                {
                    var walkble = !Physics2D.OverlapCircle(new Vector2(x + 0.5f, y + 0.5f), 0.05f, _layerMask);
                    if (walkble)
                    {
                        if (_Finder.GetPath(player, new Vector2(x, y)).Count <= _playerPosition._movePoint)
                        {
                            _moveList.Add(new Vector2(x + 0.5f, y + 0.5f));
                        }
                    }
                }
            }
        }

        private void ClearHighLight()
        {
            _highLightMap.ClearAllTiles();
            _moveList.Clear();
        }

        private void GetPositionPlayer(Player obj)
        {
            _playerPosition = obj;
            CalculateMoveGrid();
            SetHighLight(_moveList);
        }

        private void UpdateMoveGrid()
        {
            ClearHighLight();
            CalculateMoveGrid();
            SetHighLight(_moveList);
            
        }

        private void OnDisable()
        {
            _controller.OnSelected -= GetPositionPlayer;
            _controller.OffSelected -= ClearHighLight;
            _controller.MovePointChanged -= UpdateMoveGrid;
            _controller.Disable -= ClearHighLight;
            _uiController.EndTurnePress -= EndTurne;
            _uiController.CombatPress -= GetCombat;
            
        }
    }
}