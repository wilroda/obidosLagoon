using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using static UnityEditor.PlayerSettings;

[AddComponentMenu("Actions/Move")]
public class ActionMove : Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private float       speed = 1.0f;
    [SerializeField]
    private float       animationSpeed = 0.25f;
    [SerializeField]
    private float       animationAmplitude = 0.1f;
    [SerializeField]
    private bool        followGround = false;
    [SerializeField, ShowIf("followGround")]
    private LayerMask   groundMask;
    [SerializeField]
    private bool        checkCollisions = false;
    [SerializeField, ShowIf("checkCollisions"), TextArea]
    private string      displayTextOnCollision;
    [SerializeField, ShowIf("checkCollisions")]
    private float       textOffsetY = 0.1f;
    [SerializeField, ShowIf("checkCollisions")]
    private float       textDuration = 3.0f;
    [SerializeField, ShowIf("checkCollisions")]
    private Color       textBackground = Color.white;
    [SerializeField, ShowIf("checkCollisions")]
    private Color       textColor = Color.black;
    [SerializeField]
    private bool        loop = false;

    Vector3             target;
    Quaternion          targetRotation;
    int                 waypointIndex = -1;
    float               movementAngle = 0.0f;
    List<Vector3>       actualPositions;
    List<Quaternion>    actualRotations;
    float               lastOffsetY;
    Vector3             originalPosition;

    private void Start()
    {
        target = transform.position;

        originalPosition = target;

        actualPositions = new List<Vector3>();
        actualRotations = new List<Quaternion>();
        foreach (var waypoint in waypoints)
        {
            Vector3 pos = waypoint.position;
            pos.y = GetGroundY(pos);
            actualPositions.Add(pos);
            actualRotations.Add(waypoint.rotation);
        }
        lastOffsetY = 0.0f;
    }

    public void Update()
    {
        bool isMoving = false;

        Vector3 pos = transform.position;
        pos.y -= lastOffsetY;

        float distance = GetDistance(target, pos);
        if (distance > 1e-3)
        {
            isMoving = true;

            Vector3 newPos = Vector3.MoveTowards(pos, target, speed * Time.deltaTime);
            Vector3 dir = (newPos - pos);
            if (dir.sqrMagnitude > 1e-3)
            {
                dir.Normalize();
                transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
            }
            else
            {
                transform.rotation = targetRotation;
            }

            pos = newPos;
        }
        else
        {
            transform.rotation = targetRotation;

            lastOffsetY = 0.0f;

            if (followGround)
            {
                pos.y = GetGroundY(transform.position);
            }
        }

        if (isMoving)
        {
            movementAngle += Time.deltaTime * Mathf.PI / animationSpeed;

            if (followGround)
            {
                // Get ground
                pos.y = GetGroundY(transform.position) + Mathf.Abs(Mathf.Sin(movementAngle)) * animationAmplitude;
            }
            else
            {
                lastOffsetY = Mathf.Abs(Mathf.Sin(movementAngle)) * animationAmplitude;

                pos.y += lastOffsetY;
            }
        }

        transform.position = pos;
    }

    float GetGroundY(Vector3 p)
    {
        // Get ground
        if (Physics.Raycast(p + Vector3.up * 1.0f * animationAmplitude, Vector3.down, out RaycastHit hit, 2.0f, groundMask, QueryTriggerInteraction.Collide))
        {
            return hit.point.y;
        }

        return transform.position.y;
    }

    float GetDistance(Vector3 p1, Vector3 p2)
    {
        float deltaY = Mathf.Abs(p1.y - p2.y);
        if (deltaY < 1e-1)
        {
            p1.y = 0.0f;
            p2.y = 0.0f;
        }
        return Vector3.Distance(p1, p2);
    }

    protected override bool OnRun()
    {
        // Still moving, can't move again before we're done
        float distance = GetDistance(target, transform.position);
        if (distance > 1e-3) return true;

        Vector3     prevPosition = target;
        Quaternion  prevRotation = targetRotation;
        int         prevWaypointIndex = waypointIndex;
        bool        end = false;

        waypointIndex++;
        if (waypointIndex >= actualPositions.Count)
        {
            if (loop) waypointIndex = 0;
            else
            {
                waypointIndex = actualPositions.Count - 1;
                end = true;
            }
        }
        target = actualPositions[waypointIndex];
        targetRotation = actualRotations[waypointIndex];

        if ((checkCollisions) && (!end))
        {
            // Check if there's a collision between the current position and the next one
            var pos = transform.position + Vector3.up * 0.1f;
            var dest = target + Vector3.up * 0.1f;
            var dir = dest - pos;
            var maxDist = dir.magnitude;
            dir /= maxDist;
            
            var hits = Physics.RaycastAll(pos, dir, maxDist, InteractionManager.GetLayerMask(), QueryTriggerInteraction.Collide);
            foreach (var h in hits)
            {
                if (IsChild(h.collider.gameObject, gameObject)) continue;

                // Found an intersection not with himself, can't move for now!
                target = prevPosition;
                targetRotation = prevRotation;
                waypointIndex = prevWaypointIndex;

                if (displayTextOnCollision != "")
                {
                    SpeechManager.Say(transform, displayTextOnCollision, textBackground, textColor, textDuration, textOffsetY, true);
                }
                return true;
            }
        }

        return true;
    }

    bool IsChild(GameObject check, GameObject obj)
    {
        if (check == obj) return true;

        foreach (Transform child in obj.transform)
        {
            if (IsChild(check, child.gameObject)) return true;
        }

        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (waypoints == null) return;
        if (waypoints.Length == 0) return;

        Gizmos.color = Color.yellow;

        if (UnityEditor.EditorApplication.isPlaying)
        {
            Vector3 oldPos = originalPosition;
            foreach (var t in actualPositions)
            {
                Gizmos.DrawLine(oldPos, t);

                oldPos = t;
            }

            if (loop)
            {
                Gizmos.DrawLine(oldPos, actualPositions[0]);
            }
        }
        else
        {
            Vector3 oldPos = transform.position;
            foreach (var t in waypoints)
            {
                Gizmos.DrawLine(oldPos, t.position);

                oldPos = t.position;
            }

            if (loop)
            {
                Gizmos.DrawLine(oldPos, waypoints[0].position);
            }
        }
    }
#endif
}
