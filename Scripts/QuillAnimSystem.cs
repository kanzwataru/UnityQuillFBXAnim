using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuillAnim {
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
}
