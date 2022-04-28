using System;
using System.Collections.Generic;
using UnityEngine;

namespace Finark.AI
{
    public abstract class StateMachine : MonoBehaviour
    {

        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> _currentTransitions = new List<Transition>();
        private List<Transition> _anyTransitions = new List<Transition>();

        private State _currentState;

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

            if (_currentState == null) return;

            var transition = GetTransition();
            if (transition != null)
                SwitchState(transition.To);

            //Run state update
            _currentState?.RunState(this);

        }

        public virtual void FixedUpdate()
        {

            if (_currentState == null) return;

            var transition = GetTransition();
            if (transition != null)
                SwitchState(transition.To);

            //Run state fixed update
            _currentState?.PhysicsRunState(this);
        }

        public void SwitchState(State nextState)
        {

            //If currentState is nextState just continue the current state
            if (_currentState == nextState) return;

            //Exit The Current State Before Changing It
            _currentState?.ExitState(this);

            //Change _currentState to the next state
            _currentState = nextState;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);

            if (_currentTransitions == null)
            {
                _currentTransitions = new List<Transition>();
            }

            //Enter The Current State
            _currentState.EnterState(this);

        }

        public void AddAnyTransition(State state, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(state, predicate));
        }

        public void AddTransition(State from, State to, Func<bool> predicate)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, predicate));
        }

        protected class Transition
        {
            public Func<bool> Condition { get; }
            public State To { get; }

            public Transition(State to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }
        //List<Func<bool>>
        private Transition GetTransition()
        {

            foreach (var transition in _anyTransitions)
                if (transition.Condition())
                    return transition;

            foreach (var transition in _currentTransitions)
                if (transition.Condition())
                    return transition;

            return null;
        }

    }
}