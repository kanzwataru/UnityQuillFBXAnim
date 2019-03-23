using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuillAnim {
public class QuillAnimNode : StateMachineBehaviour {
	public int frameRate = 12;
	public bool persistFrames = false;
	public GameObject animationPrefab;
	public bool resetFrameOnEnter;

	private QuillAnimation _animInstance = null;
	private GameObject _animObject = null;


	private void CreateAnimInstance(GameObject this_object) {
		/* destroy placeholders */
		foreach(Transform child in this_object.transform) {
			if(child.tag == "EditorOnly")
				Destroy(child.gameObject);
		}

		var anim_object = Instantiate(animationPrefab, this_object.transform.position, this_object.transform.rotation);
		var anim_object_xform = anim_object.transform;
		anim_object_xform.parent = this_object.transform;

		_animInstance = new QuillAnimation(anim_object_xform, frameRate, persistFrames);
		_animObject = anim_object;
	}

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(_animInstance == null || _animObject == null) {
			CreateAnimInstance(animator.gameObject);
			_animInstance.SetFrame(0);
		}

		_animObject.SetActive(true);
		
		if(resetFrameOnEnter) {
			_animInstance.SetFrame(0);
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		_animObject.SetActive(false);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		_animInstance.Update();
	}
}
}
