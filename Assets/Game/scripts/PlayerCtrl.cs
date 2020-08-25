using UnityEngine;

namespace TinyBitTurtle
{
    public class PlayerCtrl : CharacterCtrl
    {
        public GameObject template;
        private bool isMoving;
        private Vector2 lastMove;
        private bool goingRight;
        private GameObject newPuff;
        private GridPlayer2D gridPlayer2D;
        private GameObject playerObject;

        public override void Setup()
        {
            base.Setup();

            playerObject = GameObject.FindGameObjectWithTag("Player");
            

            if (gridPlayer2D == null)
            {
               gridPlayer2D = playerObject.GetComponent<GridPlayer2D>();
               gridPlayer2D.speed = playerObject.GetComponent<Character>().GetStat("SPD");
            }

            ProximityCtrl.Instance.Init();
            ProximityCtrl.Instance.UpdateProximity(playerObject.transform.position);
        }

        void Update()
        {
            // missing components
            if (gridPlayer2D == null)
                return;

            isMoving = false;

            float horizontalInput = gridPlayer2D.GetAxisRaw("Horizontal");
            float verticalInput = gridPlayer2D.GetAxisRaw("Vertical");
            Vector2 FinalSpeed;
            FinalSpeed.x = horizontalInput * gridPlayer2D.speed * Time.deltaTime;
            FinalSpeed.y = verticalInput * gridPlayer2D.speed * Time.deltaTime;

            if (horizontalInput > 0.5f || horizontalInput < -0.5f || verticalInput > 0.5f || verticalInput < -0.5f)
            {
                lastMove = new Vector2(FinalSpeed.x, FinalSpeed.y);
                isMoving = true;
                goingRight = horizontalInput > 0.5f ? true : false;
            }

            character.animator.SetFloat("moveX", horizontalInput);
            character.animator.SetFloat("moveY", verticalInput);
            character.animator.SetBool("isMoving", isMoving);
            character.animator.SetFloat("lastMoveX", lastMove.x);
            character.animator.SetFloat("lastMoveY", lastMove.y);

            if (isMoving)
                ProximityCtrl.Instance.UpdateProximity(playerObject.transform.position);
        }

        public override void onMove(Character character)
        {
            base.onMove(character);

            if (gridPlayer2D == null)
                return;

            gridPlayer2D.FindPath();

            // check proximity of pickables
            //ProximityCtrl.Instance.isInRange(characterCtrl.transform.position);
        }

        public override void EmitPuff()
        {
            puffFXSpawner.TryGetNextObject(playerObject.transform.position, Quaternion.identity, out newPuff);
            if (newPuff == null)
                return;

            if (goingRight)
                newPuff.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            else
                newPuff.transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
        }
    }
}