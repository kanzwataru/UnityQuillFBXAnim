using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QuillAnim {
public class QuillAnimation : MonoBehaviour {
    /*
    public int frameRate = 12;

    private GameObject[][] _layersFrames;
    private int _overallFrameCount;
    private int _currentFrame = 0;

    private UnityEvent onEndEvent = new UnityEvent();

	// Use this for initialization
	void Start () {
        QuillAnimSystem.instance.RequestFPSCounter(frameRate);
        EventBus.AddListener<PauseEvent>(HandleEvent);

        var root = transform.Find("BakedMesh");
        if(root) {
            root = root.Find("Root");
        }
        else {
            root = transform.Find("Root");
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

	// Update is called once per frame
	void Update () {
        if(QuillAnimSystem.instance.ShouldUpdate(frameRate)) {
            int next_frame = _currentFrame + 1;
            if(next_frame == _overallFrameCount) {
                next_frame = 0;
                onEndEvent.Invoke();
            }

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

    public void Reset() {
        if(_layersFrames == null)
            return;

        foreach(var layer in _layersFrames) {
            Debug.Log(_currentFrame);
            layer[_currentFrame].SetActive(false);
            layer[0].SetActive(true);
        }
        _currentFrame = 0;
    }

    public void AddOnEndHandler(UnityAction action) {
        onEndEvent.AddListener(action);
    }

    public void RemoveOnEndHandler(UnityAction action) {
        onEndEvent.RemoveListener(action);
    }

    private void HandleEvent(PauseEvent msg)
    {
        enabled = !enabled;
    }
    */
}
}
