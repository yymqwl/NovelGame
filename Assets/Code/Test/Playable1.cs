using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// A behaviour that is attached to a playable
public class Playable1 : BasicPlayableBehaviour
{
	// Called when the owning graph starts playing
	public override void OnGraphStart(Playable playable) {
		
	}

	// Called when the owning graph stops playing
	public override void OnGraphStop(Playable playable) {
		
	}

	// Called when the state of the playable is set to Play
	public override void OnBehaviourPlay(Playable playable, FrameData info) {
        Debug.Log("OnBehaviourPlay" + info.deltaTime);
    }

	// Called when the state of the playable is set to Paused
	public override void OnBehaviourPause(Playable playable, FrameData info) {
        Debug.Log("OnBehaviourPause" + info.deltaTime);
    }

	// Called each frame while the state is set to Play
	public override void PrepareFrame(Playable playable, FrameData info)
    {
       
        Debug.Log("PrepareFrame" + info.deltaTime);
	}
}
