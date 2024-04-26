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

    Transform           target;
    int                 waypointIndex = -1;
    float               movementAngle = 0.0f;
    float               lastOffsetY;
    Vector3             originalPosition;

    private void Start()
    {
        target = transform;

        lastOffsetY = 0.0f;
    }

    public void Update()
    {
        bool isMoving = false;

        Vector3 pos = transform.position;
        pos.y -= lastOffsetY;

        Vector3 targetPos = target.position;
        targetPos.y = GetGroundY(targetPos);

        float distance = GetDistance(targetPos, pos);
        if (distance > 1e-3)
        {
            isMoving = true;

            Vector3 newPos = Vector3.MoveTowards(pos, targetPos, speed * Time.deltaTime);
            Vector3 dir = (newPos - pos);
            if (dir.sqrMagnitude > 1e-3)
            {
                dir.Normalize();
                transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
            }
            else
            {
                transform.rotation = target.rotation;
            }

            pos = newPos;
        }
        else
        {
            transform.rotation = target.rotation;

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
        Vector3 targetPos = target.position;
        targetPos.y = GetGroundY(targetPos);

        float distance = GetDistance(targetPos, transform.position);
        if (distance > 1e-3) return true;

        Transform   prevTarget = target;
        int         prevWaypointIndex = waypointIndex;
        bool        end = false;

        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            if (loop) waypointIndex = 0;
            else
            {
                waypointIndex = waypoints.Length - 1;
                end = true;
            }
        }
        target = waypoints[waypointIndex];

        if ((checkCollisions) && (!end))
        {
            // Check if there's a collision between the current position and the next one
            var pos = transform.position + Vector3.up * 0.1f;
            var dest = target.position;
            dest.y = GetGroundY(dest) + 0.1f;
            var dir = dest - pos;
            var maxDist = dir.magnitude;
            dir /= maxDist;
            
            var hits = Physics.RaycastAll(pos, dir, maxDist, InteractionManager.GetLayerMask(), QueryTriggerInteraction.Collide);
            foreach (var h in hits)
            {
                if (IsChild(h.collider.gameObject, gameObject)) continue;

                // Found an intersection not with himself, can't move for now!
                target = prevTarget;
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
#endif
}
