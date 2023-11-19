using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    public AK.Wwise.Event MyEvent;

    // Post a Wwise event each time the player jumps.
    void PlayJumpSound()
    {
        MyEvent.Post(gameObject);
    }
}
