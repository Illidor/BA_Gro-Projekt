using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
	[RequireComponent(typeof (Rigidbody))]
	public class RigidbodyFirstPersonController : MonoBehaviour
	{
		[Serializable]
		public class MovementSettings
		{
			public float ForwardSpeed = 8.0f;   // Speed when walking forward
			public float BackwardSpeed = 4.0f;  // Speed when walking backwards
			public float StrafeSpeed = 4.0f;    // Speed when walking sideways
			public float CrouchSpeed = 2f;    // Speed when crouching
			public float RunMultiplier = 1.5f;  // Speed when sprinting
			public KeyCode RunKey = KeyCode.LeftShift;
			public float stepHeight = 1;
			public float JumpForce = 60f;
			public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
			[HideInInspector] public float CurrentTargetSpeed = 8f;

			public bool IsCrouching = false;
			private bool m_Running;

			public void UpdateDesiredTargetSpeed(Vector2 input)
			{
				if (input == Vector2.zero) return;

				if (IsCrouching) {
					CurrentTargetSpeed = CrouchSpeed;
				}
				else if (input.x > 0 || input.x < 0)
				{
					//strafe
					CurrentTargetSpeed = StrafeSpeed;
				}
				else if (input.y < 0)
				{
					//backwards
					CurrentTargetSpeed = BackwardSpeed;
				}
				else if (input.y > 0)
				{
					//forwards
					//handled last as if strafing and moving forward at the same time forwards speed should take precedence
					CurrentTargetSpeed = ForwardSpeed;
				}
			}
		}


		[Serializable]
		public class AdvancedSettings
		{
			public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
			public float stickToGroundHelperDistance = 0.5f; // stops the character
			public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input
			public bool airControl; // can the user control the direction that is being moved in the air
			[Tooltip("set it to 0.1 or more if you get stuck in wall")]
			public float shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
		}

		public Camera cam;
		public MovementSettings movementSettings = new MovementSettings();
		public MouseLook mouseLook = new MouseLook();
		public AdvancedSettings advancedSettings = new AdvancedSettings();
        private Animator playerAnimator;

        private Rigidbody m_RigidBody;
		private float m_YRotation;
		private Vector3 m_GroundContactNormal;
		//private bool m_Jump, m_PreviouslyGrounded, m_Jumping, m_IsGrounded;

		[SerializeField] GroundCheck groundCheck;
		private bool wasPreviouslyGrounded = true;

		public Vector3 Velocity
		{
			get { return m_RigidBody.velocity; }
		}

		// Custom
		private bool isCrouching = false;
		[SerializeField] private Vector3 cameraCrouchPositionOffset = new Vector3(0f, -0.5f, 0);
		[SerializeField] private CapsuleCollider standCollider;
		[SerializeField] private CapsuleCollider crouchCollider;
		private PlayerHealth playerHealth;
		private Sound footstepSound;
		[SerializeField] private float footstepSoundTicker = 1f;
		[SerializeField] private float footstepSoundThreshold = 0.65f;
		private int footstepSoundCount = 0;

		// used for saving before reducing the speed with health conditions
		private float defaultForwardSpeed;
		private float defaultBackwardSpeed;
		private float defaultStrafeSpeed;
		private float defaultCrouchSpeed;

		//used to lock PlayerMovement
		public bool freezePlayerCamera = true;
		public bool freezePlayerMovement = true;

        private Quaternion targetRotation;
        public Quaternion TargetRotation { get { return targetRotation; } set { targetRotation = value; } }

		private void Start()
		{
			m_RigidBody = GetComponent<Rigidbody>();
			mouseLook.Init (transform, cam.transform);

			// Speed init values
			defaultForwardSpeed = movementSettings.ForwardSpeed;
			defaultBackwardSpeed = movementSettings.BackwardSpeed;
			defaultStrafeSpeed = movementSettings.StrafeSpeed;
			defaultCrouchSpeed = movementSettings.CrouchSpeed;

			playerHealth = GetComponent<PlayerHealth>();
			footstepSound = GetComponent<Sound>();

            Invoke("defreezeMovement", 7f);

            playerAnimator = GetComponentInChildren<Animator>();
        }

		private void Update()
		{
            if (!freezePlayerCamera)
                RotateView();
            else
            {
                cam.transform.rotation = new Quaternion(0, 0, 0, 0);
                transform.rotation = targetRotation;
            }

            if (CrossPlatformInputManager.GetButtonDown("Crouch") && isCrouching == false)
			{
				movementSettings.IsCrouching = true;
				isCrouching = true;
				crouchCollider.enabled = true;
				standCollider.enabled = false;
			}

			if (CrossPlatformInputManager.GetButtonUp("Crouch") && isCrouching)
			{
				movementSettings.IsCrouching = false;
				isCrouching = false;
				standCollider.enabled = true;
				crouchCollider.enabled = false;
			}

			CheckMovability();
		}

        private void defreezeMovement()
        {
            freezePlayerCamera = false;
            freezePlayerMovement = false;
        }


        private void FixedUpdate()
		{
			Vector2 input = GetInput();

			if(wasPreviouslyGrounded == true && !groundCheck.IsGrounded)
			{
				wasPreviouslyGrounded = false;
			}

			if(wasPreviouslyGrounded == false && groundCheck.IsGrounded)
			{
				GroundCheck();
			}

			if(!freezePlayerMovement)
			{
                if (Mathf.Abs(input.x) > 0 || Mathf.Abs(input.y) > 0)
                {
                    playerAnimator.SetBool("IsMoving", true);
                }
                else
                {
                    playerAnimator.SetBool("IsMoving", false);
                }
                if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) /*&& (advancedSettings.airControl || m_IsGrounded)*/)
				{
                    // always move along the camera forward as it is the direction that it being aimed at
                    Vector3 desiredMove = cam.transform.forward * input.y + cam.transform.right * input.x;
					desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

					desiredMove.x = desiredMove.x * movementSettings.CurrentTargetSpeed;
					desiredMove.z = desiredMove.z * movementSettings.CurrentTargetSpeed;
					desiredMove.y = 0f;

					if (m_RigidBody.velocity.sqrMagnitude <
						(movementSettings.CurrentTargetSpeed * movementSettings.CurrentTargetSpeed))
					{
						m_RigidBody.AddForce(desiredMove * SlopeMultiplier() / 2f, ForceMode.VelocityChange);
					}

					// Footstep audio logic with increasing ticker and threshold
					footstepSoundTicker += Time.deltaTime;
					if (footstepSoundTicker > footstepSoundThreshold)
					{
						footstepSoundTicker = 0f;
						footstepSoundCount++;
						// Play sounds at different audio sources so they don't get killed before fully played
						if (footstepSoundCount % 2 == 0)
						{
							footstepSound.PlaySound(UnityEngine.Random.Range(0, footstepSound.clips.Count), 1);
						}
						else
						{
							footstepSound.PlaySound(UnityEngine.Random.Range(0, footstepSound.clips.Count), 2);
						}
					}
				}
			}
		}


		private float SlopeMultiplier()
		{
			return 0.78f;
		}


		private void StickToGroundHelper()
		{
			RaycastHit hitInfo;
			if (Physics.SphereCast(transform.position + Vector3.up / 2f, standCollider.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
								   ((standCollider.height/2f) - standCollider.radius) +
								   advancedSettings.stickToGroundHelperDistance, /* Physics.AllLayers */ 0b_111001111, QueryTriggerInteraction.Ignore))
			{
				if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
				{
					m_RigidBody.velocity = Vector3.ProjectOnPlane(m_RigidBody.velocity, hitInfo.normal);
				}
			}
		}


		private Vector2 GetInput()
		{
			
			Vector2 input = new Vector2
				{
					x = CrossPlatformInputManager.GetAxis("Horizontal"),
					y = CrossPlatformInputManager.GetAxis("Vertical")
				};
			movementSettings.UpdateDesiredTargetSpeed(input);
			return input;
		}


		private void RotateView()
		{
			//avoids the mouse looking if the game is effectively paused
			if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

			// get the rotation before it's changed
			float oldYRotation = transform.eulerAngles.y;

			mouseLook.LookRotation (transform, cam.transform);
		}

		/// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
		private void GroundCheck()
		{
			RaycastHit hitInfo;
			if (Physics.SphereCast(transform.position + Vector3.up / 2f, standCollider.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
								   ((standCollider.height/2f) - standCollider.radius) + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
			{
				//m_IsGrounded = true;
				m_GroundContactNormal = hitInfo.normal;
			}
			else
			{
				//m_IsGrounded = false;
				m_GroundContactNormal = Vector3.up;
			}
		}

		private void CheckMovability()
		{
			if(playerHealth.GetCondition(Conditions.LowerBodyCondition) > 1.5f)
			{
				movementSettings.ForwardSpeed = defaultForwardSpeed;
				movementSettings.BackwardSpeed = defaultBackwardSpeed;
				movementSettings.StrafeSpeed = defaultStrafeSpeed;
				movementSettings.CrouchSpeed = defaultCrouchSpeed;
			}
			else if (playerHealth.GetCondition(Conditions.LowerBodyCondition) > 1f)
			{
				movementSettings.ForwardSpeed = defaultForwardSpeed / 2f;
				movementSettings.BackwardSpeed = defaultBackwardSpeed / 2f;
				movementSettings.StrafeSpeed = defaultStrafeSpeed / 2f;
				movementSettings.CrouchSpeed = defaultCrouchSpeed / 2f;
			}
			else if(playerHealth.GetCondition(Conditions.LowerBodyCondition) > 0)
			{
				movementSettings.ForwardSpeed = defaultForwardSpeed / 3f;
				movementSettings.BackwardSpeed = defaultBackwardSpeed / 3f;
				movementSettings.StrafeSpeed = defaultStrafeSpeed / 3f;
				movementSettings.CrouchSpeed = defaultCrouchSpeed / 3f;
			}
			else
			{
				movementSettings.ForwardSpeed = 0f;
				movementSettings.BackwardSpeed = 0f;
				movementSettings.StrafeSpeed = 0f;
				movementSettings.CrouchSpeed = 0f;
			}
		}
		public void OnCollisionEnter(Collision col)
		{
			if (col.gameObject.name == "pre_mattress")
			{
				foreach (ContactPoint cp in col.contacts)
				{
					if (cp.point.y < movementSettings.stepHeight && cp.point.y > col.collider.bounds.min.y)
					{
						Debug.Log("stepheight");
						transform.position = Vector3.MoveTowards(transform.position, cp.point, Time.deltaTime * movementSettings.JumpForce);
						m_RigidBody.velocity = transform.up; //ensure your character will step up 
					}
				}
			}
		}

						//private void ChangePlayerValuesAccordingToCondition(PlayerInjuries playerInjuries, bool addCondition)
						//{
						//    switch ()
						//    {
						//        case PlayerInjuries.LeftArmDislocated:
						//            if(addCondition)
						//            {
						//                // Cannot pick up items
						//            }
						//            else
						//            {
						//                // Can pick up items again
						//            }
						//            break;

						//        case PlayerInjuries.RightArmDislocated:
						//            if (addCondition)
						//            {
						//                // Cannot pick up items
						//            }
						//            else
						//            {
						//                // Can pick up items again
						//            }
						//            break;

						//        case PlayerInjuries.RightFootSprained:
						//            if (addCondition)
						//            {
						//                movementSettings.ForwardSpeed = defaultForwardSpeed / 2f;
						//                movementSettings.BackwardSpeed = defaultBackwardSpeed / 2f;
						//                movementSettings.ForwardSpeed = defaultForwardSpeed / 2f;

						//            }
						//            else
						//            {
						//                movementSettings.ForwardSpeed = defaultForwardSpeed;
						//                movementSettings.BackwardSpeed = defaultBackwardSpeed;
						//                movementSettings.ForwardSpeed = defaultForwardSpeed;
						//            }
						//            break;

						//        case PlayerInjuries.RightFootBroken:
						//            if (addCondition)
						//            {
						//                movementSettings.ForwardSpeed = 0f;
						//                movementSettings.BackwardSpeed = 0f;
						//                movementSettings.ForwardSpeed = 0f;
						//            }
						//            else
						//            {
						//                movementSettings.ForwardSpeed = defaultForwardSpeed;
						//                movementSettings.BackwardSpeed = defaultBackwardSpeed;
						//                movementSettings.ForwardSpeed = defaultForwardSpeed;
						//            }
						//            break;
						//    }
						//}

						//private void OnEnable()
						//{
						//    HealthConditions.ChangeCondition += ChangePlayerValuesAccordingToCondition;
						//}

						//private void OnDisable()
						//{
						//    HealthConditions.ChangeCondition -= ChangePlayerValuesAccordingToCondition;

						//}
	}
}
