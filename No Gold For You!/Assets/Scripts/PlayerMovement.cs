using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {
	[Header("Speeds")]
	[SerializeField] float walkSpeed;

	[Header("Physics")]
	[SerializeField] float mass;
	[SerializeField] float groundCheckRadius;
	[SerializeField] LayerMask groundMask;

	CharacterController _controller;

	// Runtime
	Vector3 _input;
	Vector3 _direction;
	Vector3 _velocity;
	RaycastHit _groundCheckInfo;

	bool isGrounded;

	private void Awake() {
		GameManager.Instance.OnBeforeStateChange += Init;
	}

	void Init(GameState state) {
		if (state != GameState.Initializing) return;

		_controller = GetComponent<CharacterController>();
	}

	void Update() {
		if (GameManager.Instance.currState != GameState.Playing) return;

		isGrounded = Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out _groundCheckInfo, _controller.height / 2 + .1f - groundCheckRadius);

		_input = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
		_input = Vector3.ClampMagnitude(_input, 1);

		transform.GetChild(0).localScale = new Vector3(1, _controller.height / 2, 1);

		ApplyGravity(GameManager.Instance.gravity, -2f);

		_controller.Move((_velocity + _direction * walkSpeed) * Time.deltaTime);
	}

	void ApplyGravity(float gravity, float defaultValue) {
		if (!isGrounded) {
			_velocity.y -= gravity / mass;
		} else {
			_velocity.y = defaultValue;
		}
	}
}