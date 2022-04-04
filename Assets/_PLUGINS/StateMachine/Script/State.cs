namespace Finark.AI
{
    public abstract class State
    {

        public abstract void EnterState(StateMachine machine);

        public abstract void ExitState(StateMachine machine);

        public abstract void RunState(StateMachine machine);

        public abstract void PhysicsRunState(StateMachine machine);

    }
}
