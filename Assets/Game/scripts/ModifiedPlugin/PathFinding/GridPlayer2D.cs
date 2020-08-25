using UnityEngine;

namespace TinyBitTurtle
{
    public class GridPlayer2D : Pathfinding2D
    {
        public float minDist = 0.2f;
        private LineRenderer lineRenderer;
        private Vector2 segment = new Vector2();

        [HideInInspector]
        public float speed;
        
        private void Start()
        {
            lineRenderer = gameObject.GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (Path.Count > 0)
            {
                DrawPath();
                Move();
            }
        }

        public void FindPath()
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                FindPath(transform.position, hit.point);
            }
        }

        private void DrawPath()
        {
            if (Path.Count > 0 && lineRenderer)
            {
                lineRenderer.positionCount = Path.Count;

                for (int i = 0; i < Path.Count; i++)
                {
                    lineRenderer.SetPosition(i, Path[i]);
                }
            }
            else
            {
                lineRenderer.positionCount = 0;
            }
        }

        public float GetAxisRaw(string directionName)
        {
            if (Path.Count == 0)
                return 0;

            segment = Path[1] - Path[0];

            if (directionName == "Horizontal")
            {
                float angle = Vector2.SignedAngle(Vector2.right, segment);
                return Mathf.Cos(angle);
            }
            else
            {
                float angle = Vector2.SignedAngle(Vector2.up, segment);
                return Mathf.Cos(angle);
            }
        }

        public override void Move()
        {
            if (Path.Count > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, Path[0], Time.deltaTime * speed);
                if (Vector3.Distance(transform.position, Path[0]) < minDist)
                {
                    Path.RemoveAt(0);
                }
            }
        }
    }
}
