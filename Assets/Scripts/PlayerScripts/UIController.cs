using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class UIController : MonoBehaviour
    {
        public Button _combat;
        public Button _endTurn;
        public Button _proverka;
        public event Action CombatPress;
        public event Action EndTurnePress;
        public event Action ProverkaPress;


        private void EndTurn()
        {
            print("konec hoda");
            EndTurnePress?.Invoke();
        }

        private void Combat()
        {
            print("Se4a na4alas");
            CombatPress?.Invoke();
        }

        private void Proverka()
        {
            print("proverka");
            ProverkaPress?.Invoke();
            
        }

    }
}
