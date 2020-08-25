using System.Collections;
using UnityEngine;

namespace TinyBitTurtle
{
    public class Cursor : UICursor
    {
        [SerializeField]
        private float clickDuration = 2f;

        void Start()
        {
            gameObject.SetActive(false);
        }

        void Update()
        {
        }

        public void UpdateCursor()
        {
            gameObject.SetActive(true);

            StartCoroutine(UpdatePosition());
        }

        IEnumerator UpdatePosition()
        {
            Vector3 pos = Input.mousePosition;

            if (uiCamera != null)
            {
                // Since the screen can be of different than expected size, we want to convert
                // mouse coordinates to view space, then convert that to world position.
                pos.x = Mathf.Clamp01(pos.x / Screen.width);
                pos.y = Mathf.Clamp01(pos.y / Screen.height);
                transform.position = uiCamera.ViewportToWorldPoint(pos);

                // For pixel-perfect results
#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7
			if (uiCamera.isOrthoGraphic)
#else
                if (uiCamera.orthographic)
#endif
                {
                    Vector3 lp = transform.localPosition;
                    lp.x = Mathf.Round(lp.x);
                    lp.y = Mathf.Round(lp.y);
                    transform.localPosition = lp;
                }
            }
            else
            {
                // Simple calculation that assumes that the camera is of fixed size
                pos.x -= Screen.width * 0.5f;
                pos.y -= Screen.height * 0.5f;
                pos.x = Mathf.Round(pos.x);
                pos.y = Mathf.Round(pos.y);
                transform.localPosition = pos;
            }

            yield return new WaitForSeconds(clickDuration);

            gameObject.SetActive(false);
        }
    }
}