using Lean.Touch;
using UnityEngine;

namespace TinyBitTurtle
{
    public class InputCtrl : SingletonMonoBehaviour<InputCtrl>
    {
        public PlayerCtrl playerControl;
        public AICtrl AIControl;

        // delegate doesn't shpw up in the editor
        public delegate void PlayerAction(Character character);
        private PlayerAction playerAction;

        [SerializeField]
        private Cursor cursor;
        private Character character = null;

        private void OnEnable()
        {
            LeanTouch.OnFingerSwipe += OnSwipe;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerSwipe -= OnSwipe;
        }

        private void WhichAction()
        {
            PlayerAction playerAction = null;

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast(pos, Vector2.zero);
            if (hit2D.collider)
            {
                character = hit2D.collider.GetComponent<Character>();
                tag = hit2D.collider.gameObject.tag;

                // playable character
                if (tag != "" && tag == "Player")
                {
                    playerAction = GameCtrl.Instance.onTogleAltMode;
                    //playerAction = GameCtrl.Instance.onSelect;
                }
                // click on enemy (move/attack)
                else if (tag != "" && tag == "enemy")
                {
                    playerAction = GameCtrl.Instance.onAttack;
                }
                // open chest
                else if (tag != "" && tag == "Chest")
                {
                    playerAction = GameCtrl.Instance.onOpen;
                }
            }
            else
            {
                // cursor exists
                if (cursor)
                    cursor.UpdateCursor();

                // floor, move action
                playerAction = GameCtrl.Instance.onMove;
            }

            // perform the action
            if(playerAction != null)
                playerAction(character);
        }
        
        public void OnTap(LeanFinger finger)
        {
            // clicked on an object/tile
            WhichAction();

            // add sound FX
            AudioCtrl.Instance.playSoundEvent.Invoke("tap");
        }

        public void OnSwipe(LeanFinger finger)
        {
        }
    }
}