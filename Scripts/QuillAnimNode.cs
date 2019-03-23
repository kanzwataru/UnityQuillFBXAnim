using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuillAnim {
public class QuillAnimNode : StateMachineBehaviour {
	public int frameRate = 12;
	public GameObject animationPrefab;
	public bool resetFrameOnEnter;

	private GameObject _animInstance = null;

	private GameObject[][] _layersFrames;
    private int _overallFrameCount;
    private int _currentFrame = 0;

	private void CreateAnimInstance(GameObject this_object) {
		QuillAnimSystem.instance.RequestFPSCounter(frameRate);

		_animInstance = Instantiate(animationPrefab, this_object.transform.position, this_object.transform.rotation);
		var xform = _animInstance.transform;
		xform.parent = this_object.transform;

		var root = xform.Find("BakedMesh");
		if(root) {
			root = root.Find("Root");
		}
		else {
			root = xform.Find("Root");
		}

		int layer_count = root.childCount;

		_layersFrames = new GameObject[layer_count][];  // create arrays for each layer
		for(int i = 0; i < layer_count; ++i) {          // for each layer, add the frames
            var layer = root.GetChild(i);
            
            var frame_count = layer.childCount;                 // get the number of frames
            _layersFrames[i] = new GameObject[frame_count];     // create an array for the frames
            for(int f = 0; f < frame_count; ++f) {              // add all the frames
                _layersFrames[i][f] = layer.GetChild(f).gameObject;
                _layersFrames[i][f].SetActive(false);
            }
        }

		// take the longest frame as the length of the animation
        _overallFrameCount = 0;
        foreach(var layer in _layersFrames) {
            if(layer.Length > _overallFrameCount)
                _overallFrameCount = layer.Length;
        }
	}

	private void SetFrame(int next_frame) {
		foreach(var layer in _layersFrames) {
			if(_currentFrame < layer.Length)
				layer[_currentFrame].SetActive(false);

			if(next_frame < layer.Length) {
				layer[next_frame].SetActive(true);
			}
		}

		_currentFrame = next_frame;
	}

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(!_animInstance) {
			CreateAnimInstance(animator.gameObject);
			SetFrame(0);
		}

		_animInstance.SetActive(true);
		
		if(resetFrameOnEnter) {
			SetFrame(0);
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		_animInstance.SetActive(false);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(QuillAnimSystem.instance.ShouldUpdate(frameRate)) {
            int next_frame = _currentFrame + 1;
            if(next_frame == _overallFrameCount) {
                next_frame = 0;
            }

			SetFrame(next_frame);
		}
	}
}
}
