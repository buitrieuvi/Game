﻿using DG.Tweening;
using Game.Service;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.Presenter
{
    public class PlayerMovement : MonoBehaviour
    {
        [Inject] public InputService Input { get; private set; }
        [Inject] public LayerService Layer { get; private set; }

        [Inject] public readonly Player player;

        public CharacterController CharCtrl { get; private set; }
        private PlayerState _movement;
        public float CrouchTime;
        public Vector2 Move;
        public Vector2 LastMove;
        public float SpeedWalking;
        public float Speed;
        public bool IsGrounded;
        public bool IsIdling;
        public bool IsWalking;
        public bool IsMoving;
        public bool IsStoping;
        public float DistanceToGround;
        public bool IsAirbone;
        public bool IsFalling;

        private void Awake()
        {
            CharCtrl = GetComponent<CharacterController>();

            _movement = new PlayerState(this);
            _movement.ChangeState(_movement.idling);

            //Input.CursorOff();
        }

        public void Start()
        {
            //UiState.UiState.ChangeState(UiState.UiState.MenuPlaying);
        }

        public void Update()
        {
            _movement.HandleInput();
            _movement.Update();
        }

        public void FixedUpdate()
        {
            _movement.PhysicsUpdate();
        }

        public void OnTriggerEnter(Collider other)
        {
            _movement.OntriggerEnter(other);
        }

        public void OnTriggerExit(Collider other)
        {
            _movement.OntriggerExit(other);
        }

        public void IsFreezing(bool status)
        {
            if (status)
            {
                _movement.ChangeState(_movement.freezing);
                return;
            }

            DOTween.Sequence().SetDelay(0.15f).AppendCallback(() => 
            {
                _movement.ChangeState(_movement.idling);
            });
        }
    }

    #region Player Movement system
    public interface IState
    {
        public void Enter();
        public void Update();
        public void PhysicsUpdate();
        public void HandleInput();
        public void Exit();

        public void OntriggerEnter(Collider coll);
        public void OntriggerExit(Collider coll);
    }
    public abstract class StateMachine
    {
        protected IState currentState;

        public void ChangeState(IState newState)
        {
            currentState?.Exit();

            currentState = newState;

            currentState.Enter();
        }

        public void HandleInput()
        {
            currentState?.HandleInput();
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void PhysicsUpdate()
        {
            currentState?.PhysicsUpdate();
        }

        public void OntriggerEnter(Collider coll) { currentState?.OntriggerEnter(coll); }
        public void OntriggerExit(Collider coll) { currentState?.OntriggerExit(coll); }
    }
    public class PlayerState : StateMachine
    {
        public PlayerMovement player { get; }
        public PlayerFreezing freezing { get; }
        public PlayerStoping stoping { get; }
        public PlayerIdling idling { get; }
        public PlayerWalking walking { get; }
        public PlayerFall falling { get; }
        public PlayerState(PlayerMovement player)
        {
            this.player = player;

            freezing = new PlayerFreezing(this);
            idling = new PlayerIdling(this);
            walking = new PlayerWalking(this);
            stoping = new PlayerStoping(this);

            falling = new PlayerFall(this);
        }
    }
    public class PlayerMovementState : IState
    {
        protected PlayerState playerState;

        protected float targetAngle;
        protected float angle;
        protected Vector3 moveDir;

        protected float turnSmoothTime = 0.1f;
        protected float turnSmoothVelocity;

        protected float speed;

        public PlayerMovementState(PlayerState playerState)
        {
            this.playerState = playerState;
            playerState.player.Input.CursorOff();
        }

        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {

        }

        public virtual void HandleInput()
        {
            playerState.player.Move = playerState.player.Input.Player.Move.ReadValue<Vector2>();

            if (playerState.player.Move != Vector2.zero)
            {
                playerState.player.LastMove = playerState.player.Move;
            }
        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Update()
        {
            playerState.player.DistanceToGround = CheckDistanceToGrounded();

            if (playerState.player.DistanceToGround > 0.1)
            {
                ApplyGravity();
            }
        }

        protected void Move()
        {
            playerState.player.Speed = Mathf.Lerp(playerState.player.Speed, speed, Time.deltaTime * playerState.player.CrouchTime);

            targetAngle = Mathf.Atan2(playerState.player.LastMove.x, playerState.player.LastMove.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(playerState.player.CharCtrl.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            playerState.player.CharCtrl.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerState.player.CharCtrl.Move(moveDir.normalized * playerState.player.Speed * Time.deltaTime);
        }

        protected float CheckDistanceToGrounded()
        {
            Vector3 origin = playerState.player.CharCtrl.bounds.center;
            origin.y = playerState.player.CharCtrl.bounds.center.y - 0.45f;

            RaycastHit hit;

            float radius = playerState.player.CharCtrl.radius;
            if (Physics.SphereCast(origin, radius, Vector3.down, out hit, Mathf.Infinity, playerState.player.Layer.GroundLayer.value))
            {
                //if (hit.distance <= 0.001f) { return 0f; }
                return hit.distance;
            }

            return 0;
        }

        protected void ApplyGravity()
        {
            playerState.player.CharCtrl.Move(Physics.gravity * Time.deltaTime * 2f);
        }

        protected float FindTimeFromValue(AnimationCurve curve, float precision = 0.01f)
        {
            float valueAtTime = curve.keys.Last().time;
            float val = 0;

            for (float i = 0; i < valueAtTime; i += precision)
            {
                val = curve.Evaluate(i);
                if (val < playerState.player.Speed)
                {
                    return i;
                }
            }
            return -1;
        }

        public virtual void OntriggerEnter(Collider coll)
        {
            Controller c = coll.GetComponent<Controller>();
            if (c != null)
            {
                c.TriggerEnter();
                playerState.player.transform.parent = c.transform;
            }

        }

        public virtual void OntriggerExit(Collider coll)
        {
            Controller c = coll.GetComponent<Controller>();
            if (c != null)
            {
                c.TriggerExit();
                playerState.player.transform.parent = null;
            }
        }
    }
    public class PlayerGrounded : PlayerMovementState
    {
        public PlayerGrounded(PlayerState playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            playerState.player.IsGrounded = true;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsGrounded = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();
        }
    }
    public class PlayerAirborne : PlayerMovementState
    {
        public PlayerAirborne(PlayerState playerState) : base(playerState)
        {
        }

        public override void Enter()
        {
            base.Enter();
            playerState.player.IsAirbone = true;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsAirbone = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();
        }
    }
    public class PlayerMoving : PlayerGrounded
    {
        public PlayerMoving(PlayerState playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            playerState.player.IsMoving = true;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsMoving = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }

        public override void Update()
        {
            base.Update();
            Move();
        }
    }
    public class PlayerWalking : PlayerMoving
    {

        public PlayerWalking(PlayerState playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            playerState.player.IsWalking = true;

            speed = playerState.player.SpeedWalking;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsWalking = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();

            if (playerState.player.Move != Vector2.zero)
            {
                return;
            }

            if (playerState.player.Move == Vector2.zero)
            {
                playerState.ChangeState(playerState.stoping);
            }
        }
    }
    public class PlayerStoping : PlayerMoving
    {
        public PlayerStoping(PlayerState playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            playerState.player.IsStoping = true;
            speed = 0;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsStoping = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();

            if (playerState.player.Speed < 0.050f)
            {
                playerState.ChangeState(playerState.idling);
            }

            if (playerState.player.Move != Vector2.zero)
            {
                playerState.ChangeState(playerState.walking);
            }
        }
    }
    public class PlayerIdling : PlayerGrounded
    {
        public PlayerIdling(PlayerState playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            playerState.player.IsIdling = true;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsIdling = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();

            if (playerState.player.Move == Vector2.zero)
            {
                return;
            }

            playerState.ChangeState(playerState.walking);
        }
    }
    public class PlayerFreezing : PlayerMovementState
    {
        public PlayerFreezing(PlayerState playerState) : base(playerState)
        {

        }

        public override void Enter()
        {
            base.Enter();
            playerState.player.Input.Player.Disable();
            playerState.player.player.playerCamera.SetLook(false);
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.Input.Player.Enable();
            playerState.player.player.playerCamera.SetLook(true);
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();
            if (playerState.player.Speed > 0.1f)
            {
                Move();
            }
        }
    }
    public class PlayerFall : PlayerAirborne
    {
        public PlayerFall(PlayerState playerState) : base(playerState)
        {
        }

        public override void Enter()
        {
            base.Enter();
            playerState.player.IsFalling = true;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsFalling = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();
        }
    }

    #endregion Player Movement system
}

