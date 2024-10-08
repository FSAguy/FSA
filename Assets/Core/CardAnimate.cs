using UnityEngine;

namespace Core
{
    public static class CardAnimate
    {
        public const float MoveTime = 1f; // TODO: make this depend on game settings (game speed?)
        public static void MoveTo(this Card card, Vector3 pos, Quaternion rot, float time = MoveTime, bool local = false)
        {
            LeanTween.cancel(card.gameObject);
            if (local)
            {
                LeanTween.moveLocal(card.gameObject, pos, time).setEaseInOutExpo();
                LeanTween.rotateLocal(card.gameObject, rot.eulerAngles, time).setEaseInQuint();
            }
            else
            {
                LeanTween.move(card.gameObject, pos, time).setEaseInOutExpo();
                LeanTween.rotate(card.gameObject, rot.eulerAngles, time).setEaseInQuint();
            }
        }

        public static void MoveTo(this Card card, Transform t, float time = MoveTime)
        {
            MoveTo(card, t.position, t.rotation, time);
        }
    }
}