using System;
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
			public float RunMultiplier = 1.5f;  // Speed when sprinting
			public KeyCode RunKey = KeyCode.LeftShift;
			public float stepHeight = 1;
			public float JumpForce = 60f;
			public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
			[HideInInspector] public float CurrentTargetSpeed = 8f;

			private bool m_Running;

			public void UpdateDesiredTargetSpeed(Vector2 input)
			{
				if (input == Vector2.zero) return;
				if (input.x > 0 || input.x < 0)
				{
					//strafe
					CurrentTargetSpeed = StrafeSpeed;
				}
				if (input.y < 0)
				{
					//backwards
					CurrentTargetSpeed = BackwardSpeed;
				}
				if (input.y > 0)
				{
					//forwards
					//handled last as if strafing and moving forward at the same time forwards speed should take precedence
					CurrentTargetSpeed = ForwardSpeed;
				}
				if (Input.GetKey(RunKey))
				{

				}
				else
				{
					m_Running = false;
				}
			}

			public bool Running
			{
				get { return m_Running; }
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

		private Rigidbody m_RigidBody;
		private float m_YRotation;
		private Vector3 m_GroundContactNormal;
		private bool m_Jump, m_PreviouslyGrounded, m_Jumping, m_IsGrounded;


		public Vector3 Velocity
		{
			get { return m_RigidBody.velocity; }
		}

		public bool Grounded
		{
			get { return m_IsGrounded; }
		}

		public bool Jumping
		{
			get { return m_Jumping; }
		}

		public bool Running
		{
			get
			{
				return movementSettings.Running;
			}
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

		//used to lock PlayerMovement
		public bool freezePlayerCamera = true;
		public bool freezePlayerMovement = true;

		private void Start()
		{
			m_RigidBody = GetComponent<Rigidbody>();
			mouseLook.Init (transform, cam.transform);

			// Speed init values
			defaultForwardSpeed = movementSettings.ForwardSpeed;
			defaultBackwardSpeed = movementSettings.BackwardSpeed;
			defaultStrafeSpeed = movementSettings.StrafeSpeed;

			playerHealth = GetComponent<PlayerHealth>();
			footstepSound = GetComponent<Sound>();
		}

		private void Update()
		{
			if(!freezePlayerCamera)
				RotateView();

			if (CrossPlatformInputManager.GetButtonDown("Jump") && !m_Jump && m_IsGrounded)
			{
				m_Jump = true;
			}

			if (m_Jump == false && CrossPlatformInputManager.GetButtonDown("Crouch") && isCrouching == false)
			{
				isCrouching = true;
				crouchCollider.enabled = true;
				standCollider.enabled = false;
				cam.transform.localPosition = cameraCrouchPositionOffset;
			}

			if (m_Jump == false && CrossPlatformInputManager.GetButtonUp("Crouch") && isCrouching)
			{
				isCrouching = false;
				standCollider.enabled = true;
				crouchCollider.enabled = false;
				cam.transform.localPosition = Vector3.zero;
			}

			CheckMovability();
		}

		private void FixedUpdate()
		{
			GroundCheck();

			Vector2 input = GetInput();

			if(!freezePlayerMovement)
			{
				if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || m_IsGrounded))
				{
					// always move along the camera forward as it is the direction that it being aimed at
					Vector3 desiredMove = cam.transform.forward * input.y + cam.transform.right * input.x;
					desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

					desiredMove.x = desiredMove.x * movementSettings.CurrentTargetSpeed;
					desiredMove.z = desiredMove.z * movementSettings.CurrentTargetSpeed;
					desiredMove.y = /*desiredMove.y*movementSettings.CurrentTargetSpeed;*/0f;
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
			

			if ( m_IsGrounded)
			{
				m_RigidBody.drag = 5f;

				//if (false)//m_Jump
				//{
				//	m_RigidBody.drag = 0f;
				//	m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
				//	m_RigidBody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
				//	m_Jumping = true;
				//}

				if (!m_Jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && m_RigidBody.velocity.magnitude < 1f)
				{
					m_RigidBody.Sleep();
				}
			}
			else
			{
				m_RigidBody.drag = 0f;
				if (m_PreviouslyGrounded && !m_Jumping)
				{
					StickToGroundHelper();
				}
			}
			m_Jump = false;
		}


		private float SlopeMultiplier()
		{
			float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
			return movementSettings.SlopeCurveModifier.Evaluate(angle);
		}


		private void StickToGroundHelper()
		{
			RaycastHit hitInfo;
			if (Physics.SphereCast(transform.position + Vector3.up / 2f, standCollider.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
								   ((standCollider.height/2f) - standCollider.radius) +
								   advancedSettings.stickToGroundHelperDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
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

			if (m_IsGrounded || advancedSettings.airControl)
			{
				// Rotate the rigidbody velocity to match the new direction that the character is looking
				Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
				m_RigidBody.velocity = velRotation*m_RigidBody.velocity;
			}
		}

		/// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
		private void GroundCheck()
		{
			m_PreviouslyGrounded = m_IsGrounded;
			RaycastHit hitInfo;
			if (Physics.SphereCast(transform.position + Vector3.up / 2f, standCollider.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
								   ((standCollider.height/2f) - standCollider.radius) + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
			{
				m_IsGrounded = true;
				m_GroundContactNormal = hitInfo.normal;

				//FootSprainedCheck
				if (m_RigidBody.velocity.y < -10f && hitInfo.collider.material.bounciness < 0.6f)
				{
					playerHealth.ChangeCondition(Conditions.LowerBodyCondition, 0.5f);
					//playerHealth.PlaySound(); //ToDo
				} 
			}
			else
			{
				m_IsGrounded = false;
				m_GroundContactNormal = Vector3.up;
			}

			// Landing
			if (!m_PreviouslyGrounded && m_IsGrounded && m_Jumping)
			{
				//AudioManager.audioManager.Play("snd_landingjump");
				m_Jumping = false;
			}
		}

		private void CheckMovability()
		{
			if(playerHealth.GetCondition(Conditions.LowerBodyCondition) > 1.5f)
			{
				movementSettings.ForwardSpeed = defaultForwardSpeed;
				movementSettings.BackwardSpeed = defaultBackwardSpeed;
				movementSettings.StrafeSpeed = defaultStrafeSpeed;
			}
			else if (playerHealth.GetCondition(Conditions.LowerBodyCondition) > 1f)
			{
				movementSettings.ForwardSpeed = defaultForwardSpeed / 2f;
				movementSettings.BackwardSpeed = defaultBackwardSpeed / 2f;
				movementSettings.StrafeSpeed = defaultStrafeSpeed / 2f;
			}
			else if(playerHealth.GetCondition(Conditions.LowerBodyCondition) > 0)
			{
				movementSettings.ForwardSpeed = defaultForwardSpeed / 3f;
				movementSettings.BackwardSpeed = defaultBackwardSpeed / 3f;
				movementSettings.StrafeSpeed = defaultStrafeSpeed / 3f;
			}
			else
			{
				movementSettings.ForwardSpeed = 0f;
				movementSettings.BackwardSpeed = 0f;
				movementSettings.StrafeSpeed = 0f;
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
