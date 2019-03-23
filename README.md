# UnityQuillFBXAnim
Using Quill FBX animations in a seamless way with Unity

There are two scripts included:
## QuillAnimComponent
Just drag and drop it onto a Quill FBX scene and press Play.
It just works and loops infinitely. Enable/disable the component to play/pause.
The simplest one to use for simple situations. Useful for using Unity as a renderer.

## QuillAnimNode
This is for using Animator state-machines 
It is mostly aimed at those who use Unity for creating games with Quill assets.
Simply add it onto a state inside the Animator, drag-and-drop the desired animation into the slot,
and set up your transitions. (make sure to turn off exit time and transition time since there is no blending)
It will automatically spawn the animation mesh when playing the game. 

##### If you want to have a visual for seeing the character in the editor, you can attach any one of its animations to the 
GameObject that contains the Animator, and set its tag to EditorOnly. It will be deleted automatically by QuillAnimNode
(and Unity will automatically exclude it from builds, so there is no performance penalty).

Example art by Nick Ladd
