using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolumeRTPC : MonoBehaviour
{

    public AK.Wwise.RTPC VolumeRTPC;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Slider>().onValueChanged.AddListener(delegate {UpdateAK();});
    }

    // UpdateAK is called when the slider value is changed
    public void UpdateAK() {
        VolumeRTPC.SetGlobalValue(GetComponent<Slider>().value);
    }
}
