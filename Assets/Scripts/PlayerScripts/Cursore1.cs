using System;
using UnityEngine;

namespace PlayerScripts
{
    public class Cursore1 : MonoBehaviour
    {
        [SerializeField] Texture2D[] cursorTexture;
        [SerializeField] private UIController _uiController;
        private CursorMode cursorMode = CursorMode.Auto;
        private Vector2 hotSpot = Vector2.zero;

        private void Start()
        {
            Cursor.SetCursor(cursorTexture[0], hotSpot, cursorMode);
            _uiController.EndTurnePress += EndTurne;
            _uiController.CombatPress += GetCombat;
        }

        private void GetCombat()
        {
            Cursor.SetCursor(cursorTexture[1], hotSpot, cursorMode);
        }

        private void OnDestroy()
        {
            _uiController.CombatPress -= GetCombat;
            _uiController.EndTurnePress -= EndTurne;
        }

        private void EndTurne()
        {
            Cursor.SetCursor(cursorTexture[0], hotSpot, cursorMode);
        }

        public void ChangeCursore(int _index)
        {
            Cursor.SetCursor(cursorTexture[_index], hotSpot, cursorMode);
        }
    }
}
