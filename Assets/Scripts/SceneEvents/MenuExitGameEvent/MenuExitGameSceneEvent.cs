using UnityEngine;

namespace SceneEvents
{
    public class MenuExitGameSceneEvent : SceneEvent
    {
        public override void TriggerEvent()
        {
            base.TriggerEvent();

           
            Application.Quit();
        }
    }
}
