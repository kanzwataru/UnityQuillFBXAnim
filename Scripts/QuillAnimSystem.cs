using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuillAnim {

/*
	Global counter singleton
 */
public class QuillAnimSystem : SingletonObject<QuillAnimSystem> {
	// fps -> millisecond timer
	List<int> _frameRates = new List<int>(5);
	List<float> _frameRateCounters = new List<float>(5);

	public void RequestFPSCounter(int fps) {
		if(_frameRates.Contains(fps))
			return; // only keep one counter for each frame rate

		_frameRates.Add(fps);
		_frameRateCounters.Add(0.0f);
	}

	public bool ShouldUpdate(int fps) {
		return _frameRateCounters[_frameRates.FindIndex(id => id == fps)] == 0.0f;
	}

	void Update () {
		for(int i = 0; i < _frameRates.Count; ++i) {
			_frameRateCounters[i] += Time.deltaTime * 1000;
			if(_frameRateCounters[i] >= 1000 / _frameRates[i]) {
				_frameRateCounters[i] = 0.0f;
			}
		}
	}
}

/*
	Class that manages Quill FBX animation logic
	(i.e. switching frame GameObjects on and off and counting frames)

	This is in its own class to allow multiple ways of playing back the animation.
	(like QuillAnimNode for state machines and QuillAnimComponent for standalone use)
*/
public class QuillAnimation {
	private GameObject[][] _layersFrames;
    private int _overallFrameCount;
    private int _currentFrame = 0;

	private int frameRate;

	public QuillAnimation(Transform animation_root, int frameRate) {
		this.frameRate = frameRate;
		QuillAnimSystem.instance.RequestFPSCounter(frameRate);

		var root = animation_root.Find("BakedMesh");
		if(root) {
			root = root.Find("Root");
		}
		else {
			root = animation_root.Find("Root");
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

	public void Update() {
		if(QuillAnimSystem.instance.ShouldUpdate(frameRate)) {
            int next_frame = _currentFrame + 1;
            if(next_frame == _overallFrameCount) {
                next_frame = 0;
            }

			SetFrame(next_frame);
		}
	}

	public void SetFrame(int next_frame) {
		foreach(var layer in _layersFrames) {
			if(_currentFrame < layer.Length)
				layer[_currentFrame].SetActive(false);

			if(next_frame < layer.Length) {
				layer[next_frame].SetActive(true);
			}
		}

		_currentFrame = next_frame;
	}
}

}
