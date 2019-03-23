using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QuillAnim {
public class QuillAnimComponent : MonoBehaviour {
    public int frameRate = 12;
    private QuillAnimation _animation;

    void Start() {
        _animation = new QuillAnimation(this.transform, frameRate);
        _animation.SetFrame(0);
    }

    void Update() {
        _animation.Update();
    }
    
    public void Reset() {
        _animation.SetFrame(0);
    }
}
}
