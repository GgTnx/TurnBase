using System;
using UnityEngine;
using System.Linq;
using PlayerScripts;
using UnityEngine.Tilemaps;


namespace Pathfinding
{
    [HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_target_mover.php")]
    public class TargetMover : MonoBehaviour
    {
        public LayerMask mask;
        public Vector3Int _positionOnTileMap;
        public Tilemap _Tilemap;
        public TileBase _hightLight;
        public Transform target;
        public bool onlyOnDoubleClick;
        public bool use2D;
        public MoveController _MoveController;
      
        Camera cam;
        public event Action<Vector3Int> onComplete;
        private Player _selectedPlayer;

        public void Start()
        {
            // cam = Camera.main;
            // ais = FindObjectsOfType<MonoBehaviour>().OfType<IAstarAI>().ToArray();
            // useGUILayout = false;
            // _MoveController.OnSelected += GetSelectedPlayer;
        }

        public void OnGUI()
        {
            if (onlyOnDoubleClick && cam != null && Event.current.type == EventType.MouseDown &&
                Event.current.clickCount == 2)
            {
                UpdateTargetPosition();
            }
        }


        void Update()
        {
            if (!onlyOnDoubleClick && cam != null)
            {
                UpdateTargetPosition();
            }
        }

        public void UpdateTargetPosition()
        {
            Vector3 newPosition = Vector3.zero;
            bool positionFound = false;

            if (use2D)
            {
                newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                newPosition.z = 0;
                positionFound = true;
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, mask))
                {
                    newPosition = hit.point;
                    positionFound = true;
                }
            }

            // if (positionFound && newPosition != target.position)
            // {
            //     target.position = newPosition;
            //
            //
            //     if (onlyOnDoubleClick)
            //     {
            //         _positionOnTileMap = _Tilemap.WorldToCell(newPosition);
            //         if (_Tilemap.GetTile(_positionOnTileMap) == _hightLight&&_selectedPlayer._movePoint>0) // продолжить тут
            //         {
            //             onComplete?.Invoke(_positionOnTileMap);
            //             for (int i = 0; i < ais.Length; i++)
            //             {
            //                 if (ais[i] != null) ais[i].SearchPath();
            //             }
            //         }
            //     }
            // }
            
        }

        private void GetSelectedPlayer(Player player)
        {
            _selectedPlayer = player;
        }

        private void OnDestroy()
        {
            _MoveController.OnSelected -= GetSelectedPlayer;
        }
    }

    
}