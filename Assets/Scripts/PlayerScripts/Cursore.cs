using System;
using UnityEngine;

namespace PlayerScripts
{
    public class Cursore : MonoBehaviour
    {
        [SerializeField] Texture2D cursorTexture;
        private CursorMode cursorMode = CursorMode.Auto;
        private Vector2 hotSpot = Vector2.zero;

        // private void Start()
        // {
        //     Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        // }

      
    }
}
