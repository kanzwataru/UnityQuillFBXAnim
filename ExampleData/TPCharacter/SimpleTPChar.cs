using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTPChar : MonoBehaviour {
	public float speed;
	private CharacterController _controller;
	private Transform _rot_root;
	private Animator _animator;

	void Start () {
		_controller = GetComponent<CharacterController>();
        _rot_root = transform.Find("rotation_root");
		_animator = _rot_root.GetComponent<Animator>();
	}
	
	void Update () {
		var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		_controller.Move(dir * Time.deltaTime * speed);

		if(dir != Vector3.zero) {
            var target_rot = Quaternion.LookRotation(dir, Vector3.up);

            _rot_root.rotation = Quaternion.Lerp(_rot_root.rotation, target_rot, 0.7f);
			_animator.SetBool("is_walking", true);
        }
		else {
			_animator.SetBool("is_walking", false);
		}

		_animator.SetBool("is_happy", Input.GetKey(KeyCode.Space));
	}
}
