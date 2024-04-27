using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Move")]
public class ActionMove : Action
{
    public enum Mode { Click, Auto };
    public enum AnimMode { None, Bounce, Animator };
    public enum WaypointOrder { Sequential, Random };

    [HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private Mode        mode = Mode.Click;
    [SerializeField, ShowIf("isAuto"), MinMaxSlider(0.0f, 10.0f)]
    private Vector2     pauseBetweenWaypoints = Vector2.zero;
    [SerializeField]
    private float       speed = 1.0f;
    [SerializeField]
    private AnimMode    animationMode = AnimMode.None;
    [SerializeField, ShowIf("animationIsBounce")]
    private float       animationSpeed = 0.25f;
    [SerializeField, ShowIf("animationIsBounce")]
    private float       animationAmplitude = 0.1f;
    [SerializeField, ShowIf("animationIsAnimator")]
    private Animator    animator;
    [SerializeField, AnimatorParam("animator"), ShowIf("animationIsAnimator")]
    private int         animatorSpeedParameter;
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
    [SerializeField, ShowIf("loop")]
    private WaypointOrder waypointOrder = WaypointOrder.Sequential;
    [SerializeField]
    private AudioClip   footstepSound;
    [SerializeField, ShowIf("hasFootstepSound"), MinMaxSlider(0.5f, 2.0f)]
    private Vector2     volume = Vector2.one;
    [SerializeField, ShowIf("hasFootstepSound"), MinMaxSlider(0.5f, 2.0f)]
    private Vector2     pitch = Vector2.one;
    [SerializeField, ShowIf("hasFootstepSoundAndAnimNotBounce")]
    private float       unitsPerStep = 0.25f;

    Transform           target;
    int                 waypointIndex = -1;
    float               movementAngle = 0.0f;
    float               lastOffsetY;
    float               stepDistance;
    float               pauseTimer;

    bool isAuto => mode == Mode.Auto;
    bool hasFootstepSound => footstepSound != null;
    bool animationIsBounce => animationMode == AnimMode.Bounce;
    bool animationIsAnimator => animationMode == AnimMode.Animator;
    bool hasFootstepSoundAndAnimNotBounce => hasFootstepSound && (animationMode != AnimMode.Bounce);

    private void Start()
    {
        target = transform;

        lastOffsetY = 0.0f;

        if (mode == Mode.Auto)
        {
            canRetrigger = true;
        }
    }

    public void Update()
    {
        if (!Level.isActive) return;

        float distanceMoved = 0.0f;

        Vector3 pos = transform.position;
        pos.y -= lastOffsetY;

        Vector3 targetPos = target.position;
        targetPos.y = GetGroundY(target.position);
        targetPos.y = GetGroundY(target.position);

        float distance = GetDistance(targetPos, pos);

        if (distance > 1e-3)
        {
            Vector3 newPos = Vector3.MoveTowards(pos, targetPos, speed * Time.deltaTime);
            Vector3 dir = (newPos - pos); dir.y = 0.0f;
            distanceMoved = dir.magnitude;
            if (distanceMoved > 1e-6)
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
            pos = target.position;
            transform.rotation = target.rotation;

            lastOffsetY = 0.0f;

            if (followGround)
            {
                pos.y = GetGroundY(pos);
            }

            if (mode == Mode.Auto)
            {
                pauseTimer -= Time.deltaTime;
                if (pauseTimer < 0.0f)
                {
                    // Go to next waypoint
                    if (CheckConditions())
                    {
                        NextWaypoint();
                    }
                    pauseTimer = Random.Range(pauseBetweenWaypoints.x, pauseBetweenWaypoints.y);
                }
            }
        }

        if (distanceMoved > 0.0f)
        {
            if (animationMode == AnimMode.Bounce)
            {
                float prevAngle = movementAngle;
                movementAngle += Time.deltaTime * Mathf.PI / animationSpeed;
                if (movementAngle >= Mathf.PI * 2.0f)
                {
                    movementAngle -= Mathf.PI * 2.0f;
                    if (footstepSound)
                    {
                        SoundManager.PlaySound(SoundManager.Type.Fx, footstepSound, Random.Range(volume.x, volume.y), Random.Range(pitch.x, pitch.y));
                    }
                }

                if (footstepSound)
                {
                    if ((prevAngle < Mathf.PI) && (movementAngle >= Mathf.PI))
                    {
                        SoundManager.PlaySound(SoundManager.Type.Fx, footstepSound, Random.Range(volume.x, volume.y), Random.Range(pitch.x, pitch.y));
                    }
                }
            }
            else
            {
                stepDistance += distanceMoved;
                if (stepDistance > unitsPerStep)
                {
                    SoundManager.PlaySound(SoundManager.Type.Fx, footstepSound, Random.Range(volume.x, volume.y), Random.Range(pitch.x, pitch.y));
                    stepDistance = 0.0f;
                }
            }

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

        if ((animationMode == AnimMode.Animator) && (animator))
        {
            // Set velocity to normalized speed
            animator.SetFloat(animatorSpeedParameter, (distanceMoved / Time.deltaTime) / speed);
        }

        transform.position = pos;
    }

    float GetGroundY(Vector3 p)
    {
        if (!followGround) return p.y;

        // Get ground
        if (Physics.Raycast(p + Vector3.up * 1.0f * animationAmplitude, Vector3.down, out RaycastHit hit, 2.0f, groundMask, QueryTriggerInteraction.Collide))
        {
            return hit.point.y;
        }

        return p.y;
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
        if (mode == Mode.Click)
        {
            NextWaypoint();
        }
        return true;
    }

    void NextWaypoint()
    {
        // Still moving, can't move again before we're done
        Vector3 targetPos = target.position;
        targetPos.y = GetGroundY(targetPos);

        float distance = GetDistance(targetPos, transform.position);
        if (distance > 1e-3) return;

        Transform   prevTarget = target;
        int         prevWaypointIndex = waypointIndex;
        bool        end = false;

        if (waypointOrder == WaypointOrder.Sequential)
        {
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
        }
        else if (waypointOrder == WaypointOrder.Random)
        {
            waypointIndex = Random.Range(0, waypoints.Length);
            target = waypoints[waypointIndex];
        }

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
                return;
            }
        }

        return;
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
