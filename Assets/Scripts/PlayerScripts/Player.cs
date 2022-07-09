using System.Collections.Generic;
using DefaultNamespace.Interfaces;
using Interfaces;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour,Idmg,IEndTurn
    {
        public enum State
        {
            Idle = 1,
            Selected = 2,
            Combat = 3,
            Die = 4
        }

        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private int _hp;
        [SerializeField] private int _currentHp;
        private UIController _uiController;
        private List<Weapon> _inventory;
        
        
        private Weapon _weapon;
        public int _movePoint = 5;
        public bool _canShoot;
        public State _currentState = State.Idle;
       

        private void Start()
        {
            _uiController = FindObjectOfType<UIController>();
            _uiController.EndTurnePress += EndTurne;
            _uiController.CombatPress += GetCombat;
        }


        private void Update()
        {
           
      
        }

        public void GetSelected()
        {
            _currentState = State.Selected;
            
        }

        public void EndSelectred()
        {
            _currentState = State.Idle;
            
        }

        private void GetCombat()
        {
            _currentState = State.Combat;
        }

        private void EndCombat()
        {
            _currentState = State.Idle;
        }

        private void Die()
        {
            _currentState = State.Die;
        }

        private void EndTurne()
        {
            _movePoint = 5;
            _canShoot = true;
        }


        public void TakeDmg(int dmg)
        {
            
        }

        private void OnDestroy()
        {
            _uiController.EndTurnePress -= EndTurne;
            _uiController.CombatPress -= GetCombat;
        }
    }
}


    
