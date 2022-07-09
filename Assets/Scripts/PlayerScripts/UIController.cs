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

            EndTurnePress?.Invoke();
        }

        private void Combat()
        {

            CombatPress?.Invoke();
        }

        private void Proverka()
        {

            ProverkaPress?.Invoke();
            
        }

    }
}
