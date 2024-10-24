using foursoulsauto.core;
using Unity.VisualScripting;
using UnityEngine;

namespace foursoulsauto.ui
{
    public static class CardAnimate
    {
        //values, fresh outta my ass
        // TODO: make this depend on game settings (game speed?)
        public const float AnimTime = 1f; 
        private static readonly Vector3 FallPadding = new(-10f, 0, -1);
        private const float FallScale = 2f;
        
        public enum Style {Slide, Fall}
        public static void MoveTo(this Card card, Vector3 pos, Quaternion rot, 
            float time = AnimTime, bool local = false, Style style = Style.Slide)
        {
            var obj = card.GameObject();
            // cancel all previous tweens and return scale to normal
            LeanTween.cancel(obj);
            obj.transform.localScale = Vector3.one; 
            
            switch (style)
            {
                case Style.Slide:
                    MoveSlide(obj, pos, rot, time, local);
                    break;
                case Style.Fall:
                    MoveFall(obj, pos, rot, time, local);
                    break;
            }
        }

        private static void MoveFall(GameObject obj, Vector3 pos, Quaternion rot, float time, bool local)
        {
            if (local)
            {
                LeanTween.rotateLocal(obj, rot.eulerAngles, time);
                obj.transform.localPosition = pos + FallPadding;
                LeanTween.moveLocal(obj, pos, time).setEaseInExpo();
            }
            else
            {
                LeanTween.rotate(obj, rot.eulerAngles, time);
                obj.transform.position = pos + FallPadding;
                LeanTween.move(obj, pos, time).setEaseInExpo();
            }

            var stretchedScale = obj.transform.localScale;
            var originalScale = stretchedScale;
            stretchedScale *= FallScale;
            obj.transform.localScale = stretchedScale;
            LeanTween.scale(obj, originalScale, time).setEaseInExpo();
        }
        
        private static void MoveSlide(GameObject obj, Vector3 pos, Quaternion rot, float time, bool local)
        {
            if (local)
            {
                LeanTween.moveLocal(obj, pos, time).setEaseOutExpo();
                LeanTween.rotateLocal(obj, rot.eulerAngles, time).setEaseOutQuad();
            }
            else
            {
                LeanTween.move(obj, pos, time).setEaseOutExpo();
                LeanTween.rotate(obj, rot.eulerAngles, time).setEaseOutQuad();
            }
        }

        public static void MoveTo(this Card card, Transform t, float time = AnimTime, Style style = Style.Slide)
        {
            MoveTo(card, t.position, t.rotation, time, false, style);
        }


        public static void TapAnim(this Card card, bool isCharged)
        {
            // get the centered transform to rotate around center instead of around topleft
            var child = card.transform.Find("Centered Transform");
            var turn = child.rotation.eulerAngles + 
                       new Vector3(0, 0, 90 * (isCharged ? 1 : -1)); // rotate 90 either left or right
            LeanTween.rotate(child.gameObject, turn, AnimTime).setEaseOutExpo();
        }
    }
}