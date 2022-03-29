using System;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public class EnemyTankAI : EntityAI
    {
        private Vector2 _moveVector;
        [SerializeField] private EnemyTankAIState _state;
        private EnemyTankAIState _lastState;

        [SerializeField] private IdleEnemyTankAIState _idleState;
        [SerializeField] private MovingToPointEnemyTankAIState _movingToPointState;
        [SerializeField] private ShootingEnemyTankAIState _shootingState;
        [SerializeField] private FollowPlayerState _followPlayer;
        public EnemyTankType TankType;
        public bool _isStateRunning;
        private int _stateSwitch;
        public bool followPlayer;

        public override void Init(Entity entity)
        {
            base.Init(entity);
            _stateSwitch = 0;

            _idleState.Init(Self);
            _movingToPointState.Init(Self);
            _shootingState.Init(Self);
            _followPlayer.Init(Self);
            
            setNewAiState();
        }
        public override void UpdateAI()
        {

            base.UpdateAI();
            if (GameObject.FindGameObjectWithTag("Game").GetComponent<PlayerPowerUps>().Timer || GameUtils.Game.Instance.IsGamePaused)
            {
                return;
            }


            if(!_isStateRunning) setNewAiState();



            _idleState.UpdateState();
            _movingToPointState.UpdateState();
            _shootingState.UpdateState();
            _followPlayer.UpdateState();
        }

        private void setNewAiState()
        {
            

            if (_isStateRunning) return;


            _stateSwitch++;

            Array values = Enum.GetValues(typeof(EnemyTankAIState));

            EnemyTankAIState randomBar = 
                (EnemyTankAIState)values.GetValue(UnityEngine.Random.Range(1, values.Length));

            if(_lastState == randomBar)
            {
                _state = _state = EnemyTankAIState.Shooting;
            }
            else
            {
                _state = randomBar;
            }

            if (randomBar == EnemyTankAIState.FollowPlayer && _stateSwitch < 10 &&  !followPlayer)
            {
                _state = _state = EnemyTankAIState.Idle;
            }

            _lastState = _state;

            controllTankStateSwitch();
        }

        private void controllTankStateSwitch()
        {
            _isStateRunning = true;

            switch (_state)
            {
                case EnemyTankAIState.Idle:
                    RunIdleState();
                    break;
                case EnemyTankAIState.MovingToPoint:
                    RunMovingToPointState();
                    break;
                case EnemyTankAIState.Shooting:
                    RunShootingState();
                    break;
                case EnemyTankAIState.FollowPlayer:
                    RunFollowPlayer();
                    break;
                default:
                    RunIdleState();
                    break;
            }
        }

        public virtual void RunIdleState()
        {
            _idleState.RunState(OnStateEnd);
        }
        public virtual void RunMovingToPointState()
        {
            
            _movingToPointState.RunState(OnStateEnd);
            
            
        }
        public virtual void RunShootingState()
        {
            _shootingState.RunState(OnStateEnd);
        }


        public virtual void RunFollowPlayer()
        {
            _followPlayer.RunState(OnStateEnd);
            _stateSwitch = 0;
        }

        public virtual void OnStateEnd()
        {
            _isStateRunning = false;
            //setNewAiState();
        }
    }

    public enum EnemyTankAIState
    {
        Idle,
        MovingToPoint,
        Shooting,
        FollowPlayer
    }
}
