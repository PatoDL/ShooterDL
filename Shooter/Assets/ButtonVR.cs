using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonVR : EventTrigger
{
    float timer = 0.0f;

    float maxTime = 1.5f;

    bool inButton = false;

    private void Update()
    {
        if(inButton)
            timer += Time.unscaledDeltaTime;

        if(timer>maxTime)
        {
            ExecuteEvents.Execute(this.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
            timer = 0.0f;
            inButton = false;
        }
    }

    public override void OnPointerEnter(PointerEventData data)
    {
        inButton = true;
    }

    public override void OnPointerExit(PointerEventData data)
    {
        timer = 0.0f;
        inButton = false;
    }
}
