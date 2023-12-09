using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {
	[Header("Values")]
	[SerializeField] float walkSpeed;
	[SerializeField] float sensitivity;
	[SerializeField] float lowBound;
	[SerializeField] float highBound;

	[Header("Physics")]
	[SerializeField] float mass;
	[SerializeField] float groundCheckRadius;
	[SerializeField] LayerMask groundMask;

	CharacterController _controller;

	// Runtime
	Vector3 _direction;
	Vector3 _velocity;
	RaycastHit _groundCheckInfo;

	Transform _cameraRef;

	bool _isGrounded;

	float _xInput, _yInput;
	float _xRotation, _yRotation;

	private void Awake() {
		GameManager.OnBeforeStateChange += Init;
	}

	private void OnDestroy() {
		GameManager.OnBeforeStateChange -= Init;
	}

	void Init(GameState state) {
		if (state != GameState.Initializing) return;

		_controller = GetComponent<CharacterController>();
		_cameraRef = Camera.main.transform;

		_xRotation = 90;
	}

	void Update() {
		if (GameManager.Instance.currState != GameState.Playing) return;

		_isGrounded = Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out _groundCheckInfo, _controller.height / 2 + .1f - groundCheckRadius);

		_direction = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
		_direction = Vector3.ClampMagnitude(_direction, 1);

		ApplyGravity(GameManager.Instance.gravity, -2f);

		_controller.Move((_velocity + _direction * walkSpeed) * Time.deltaTime);

		_xInput = Input.GetAxis("Mouse X") * sensitivity;
		_yInput = -Input.GetAxis("Mouse Y") * sensitivity;

		_xRotation += _xInput;
		_yRotation = Mathf.Clamp(_yRotation + _yInput, lowBound, highBound);

		transform.rotation = Quaternion.Euler(0f, _xRotation, 0);
		_cameraRef.localRotation = Quaternion.Euler(_yRotation, 0, 0);
	}

	void ApplyGravity(float gravity, float defaultValue) {
		if (!_isGrounded) {
			_velocity.y -= gravity / mass;
		} else {
			_velocity.y = defaultValue;
		}
	}
}