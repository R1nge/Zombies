using Data;
using Units.Humans;
using UnityEngine;
using Zenject;

namespace Units.Zombies
{
    public class ZombieUnit : Unit
    {
        [SerializeField] private GameObject selectedMark;
        [SerializeField] private ZombieConfig zombieConfig;
        private ZombieUnitStateMachine _zombieUnitStateMachine;
        private UnitRTSController _unitRtsController;

        [Inject]
        private void Inject(UnitRTSController unitRtsController) => _unitRtsController = unitRtsController;

        protected override void Awake()
        {
            base.Awake();
            _zombieUnitStateMachine = new ZombieUnitStateMachine(CoroutineRunner,  UnitMovement, UnitAnimator);
            _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Walking);
        }

        private void OnEnable()
        {
            _unitRtsController.Add(this);
            skinnedMeshRenderer.sharedMesh = zombieConfig.ZombieMesh;
            NavMeshAgent.speed = zombieConfig.Speed;
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            if (collision.transform.TryGetComponent(out HumanUnit humanUnit))
            {
                if (humanUnit.CurrentState is HumanUnitStateMachine.HumanUnitStates.Dead or HumanUnitStateMachine.HumanUnitStates.TurningIntoZombie)
                {
                    Debug.LogWarning("Human unit is already dead");
                    return;
                }

                Attack();
                
                humanUnit.Die();
            }
        }
        
        public void OnSelected() => selectedMark.SetActive(true);

        public void OnDeselected() => selectedMark.SetActive(false);
        
        public void MoveTo(Vector3 position)
        {
            UnitMovement.SetDestination(position);
            
            if (_zombieUnitStateMachine.CurrentStateType == ZombieUnitStateMachine.ZombieUnitStates.Infecting)
            {
                return;
            }

            if (_zombieUnitStateMachine.CurrentStateType == ZombieUnitStateMachine.ZombieUnitStates.Dead)
            {
                return;
            }

            if (_zombieUnitStateMachine.CurrentStateType == ZombieUnitStateMachine.ZombieUnitStates.StandUp)
            {
                return;
            }
            
            _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Walking);
        }

        public void ChangeMesh() => skinnedMeshRenderer.sharedMesh = zombieConfig.ZombieMesh;

        public override void StandUp() => _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.StandUp);

        public override void Attack() => _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Infecting);

        public override void Die() => _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Dead);

        protected override void Update() => _zombieUnitStateMachine.Update();
    }
}