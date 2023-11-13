using Game.Services;
using UnityEngine;
using Zenject;

namespace Units.Zombies
{
    public class ZombieUnit : Unit, IDamageable
    {
        private ZombieUnitStateMachine _zombieUnitStateMachine;
        private ZombieHealth _zombieHealth;
        private UnitRTSController _unitRtsController;
        private ZombieCounter _zombieCounter;
        private MarkerPositionService _markerPositionService;

        public ZombieHealth ZombieHealth
        {
            get
            {
                _zombieHealth ??= new ZombieHealth(this, unitConfig.MaxHealth);
                return _zombieHealth;
            }
        }

        [Inject]
        private void Inject(UnitRTSController unitRtsController, ZombieCounter zombieCounter, MarkerPositionService markerPositionService)
        {
            _unitRtsController = unitRtsController;
            _zombieCounter = zombieCounter;
            _markerPositionService = markerPositionService;
        }

        protected override void Awake()
        {
            base.Awake();
            var zombieDeath = new ZombieDeathController(UnitAnimator, this, UnitMovement);
            _zombieUnitStateMachine = new ZombieUnitStateMachine(CoroutineRunner, UnitMovement, UnitAnimator, zombieDeath);
            _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Idle);
        }

        private void Start()
        {
            _zombieCounter.AddZombie();
            _unitRtsController.Add(this);
        }

        protected override void OnCollisionStay(Collision collision)
        {
            base.OnCollisionStay(collision);
            if (collision.transform.TryGetComponent(out Unit unit))
            {
                if (!unit.CanBeAttackedBy(this))
                {
                    Debug.LogWarning($"{unit.gameObject.name} can't be attacked by {gameObject.name}");
                    return;
                }

                if (IsBehind(unit.transform))
                {
                    SetTargetForward(-DirectionFromPlayerTo(unit.transform));
                    unit.SetTargetForward(-DirectionFromPlayerTo(unit.transform));
                    Attack();
                    unit.Die();
                }
            }
        }


        private void SetPositionMarker(Vector3 position)
        {
            position += new Vector3(0, .1f, 0);
            _markerPositionService.SetPosition(position);
        }

        public void OnSelected() => SetPositionMarker(UnitMovement.TargetPosition);

        public void OnDeselected() => _markerPositionService.Hide();

        public void TakeDamage(int amount) => _zombieHealth.TakeDamage(amount);

        public void MoveTo(Vector3 position)
        {
            UnitMovement.SetDestination(position);

            SetPositionMarker(position);

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

        public override void Idle()
        {
           
        }

        public void StandUp() => _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.StandUp);

        public void Attack() => _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Infecting);

        public override void Die()
        {
            _zombieCounter.RemoveZombie();
            _unitRtsController.Remove(this);
            _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Dead);
        }

        public override bool CanBeAttackedBy(Unit unit)
        {
            if (unit is ZombieUnit)
            {
                return false;
            }

            return _zombieUnitStateMachine.CurrentStateType is not (ZombieUnitStateMachine.ZombieUnitStates.Dead
                or ZombieUnitStateMachine.ZombieUnitStates.Infecting);
        }

        protected override void Update()
        {
            base.Update();
            _zombieUnitStateMachine.Update();
            Debug.Log($"Current state: {_zombieUnitStateMachine.CurrentStateType}");
        }
    }
}