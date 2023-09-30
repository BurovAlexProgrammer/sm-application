using DG.Tweening;

namespace sm_application.Events
{
    public class RequireLoadSceneEvent : BaseEvent
    {
        public string NextSceneName;
        public bool ForceAppearance = false;
        public float Duration = 0.25f;
        public Ease Ease = Ease.InQuad;
        public string FrameColorHex = "000000";
    }
}