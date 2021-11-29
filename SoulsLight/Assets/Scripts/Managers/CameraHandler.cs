using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// Class <c>CameraHandler</c> is a Unity component script used to manage the camera behaviour.
/// </summary>
public class CameraHandler : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>inputHandler</c> is a Unity <c>InputHandler</c> component script representing the player input behaviour manager.
    /// </summary>
    private InputHandler _inputHandler;

    /// <summary>
    /// TODO: comments
    /// </summary>
    private PlayerManager _playerManager;
    
    /// <summary>
    /// Instance variable <c>myTransform</c> is a Unity <c>Transform</c> component representing the camera game object transform.
    /// </summary>
    private Transform _myTransform;
    
    /// <summary>
    /// Instance variable <c>cameraTransformPosition</c> represents the position vector of the camera.
    /// </summary>
    private Vector3 _cameraTransformPosition;
    
    /// <summary>
    /// Instance variable <c>cameraFollowVelocity</c> represents the velocity vector of the camera regarding the game object to follow.
    /// </summary>
    private Vector3 _cameraFollowVelocity;
    
    /// <summary>
    /// Instance variable <c>targetPosition</c> represents the distance value of the camera from the player.
    /// </summary>
    private float _targetPosition;

    /// <summary>
    /// Instance variable <c>defaultPosition</c> represents the default distance value of the camera from the player.
    /// </summary>
    private float _defaultPosition;
    
    /// <summary>
    /// Instance variable <c>lookAngle</c> represents the current look angle value of the camera.
    /// </summary>
    private float _lookAngle;
    
    /// <summary>
    /// Instance variable <c>pivotAngle</c> represents the current pivot angle value of the camera.
    /// </summary>
    private float _pivotAngle;
    
    /// <summary>
    /// Instance variable <c>availableTargets</c> is a list of Unity <c>CharacterManager</c> component scripts representing the different character managers.
    /// </summary>
    private readonly List<CharacterManager> _availableTargets = new List<CharacterManager>();

    /// <summary>
    /// Static variable <c>Singleton</c> represents the instance of the class. 
    /// </summary>
    public static CameraHandler Singleton;
    
    /// <summary>
    /// Instance variable <c>targetTransform</c> is a Unity <c>Transform</c> component representing the transform of the target game object to follow.
    /// </summary>
    public Transform targetTransform;
    
    /// <summary>
    /// Instance variable <c>cameraTransform</c> is a Unity <c>Transform</c> component representing the main camera position, rotation and scale.
    /// </summary>
    public Transform cameraTransform;
    
    /// <summary>
    /// Instance variable <c>cameraPivotTransform</c> is a Unity <c>Transform</c> component representing the camera pivot position, rotation and scale.
    /// </summary>
    public Transform cameraPivotTransform;

    /// <summary>
    /// Instance variable <c>lookSpeed</c> represents the look speed value of the camera.
    /// </summary>
    public float lookSpeed = 0.1f;
    
    /// <summary>
    /// Instance variable <c>followSpeed</c> represents the follow speed value of the camera.
    /// </summary>
    public float followSpeed = 0.1f;
    
    /// <summary>
    /// Instance variable <c>pivotSpeed</c> represents the pivot speed value of the camera.
    /// </summary>
    public float pivotSpeed = 0.03f;
    
    /// <summary>
    /// Instance variable <c>minimumPivot</c> represents the minimum pivot angle of the camera pivot angle.
    /// </summary>
    public float minimumPivot = -35;
    
    /// <summary>
    /// Instance variable <c>maximumPivot</c> represents the maximum pivot angle of the camera pivot angle.
    /// </summary>
    public float maximumPivot = 35;
    
    /// <summary>
    /// Instance variable <c>cameraSphereRadius</c> represents the sphere radius value of the camera collision detection distance.
    /// </summary>
    public float cameraSphereRadius = 0.2f;
    
    /// <summary>
    /// Instance variable <c>cameraCollisionOffset</c> represents the offset distance value assigned to the camera from the object with which it collides
    /// </summary>
    public float cameraCollisionOffset = 0.2f;
    
    /// <summary>
    /// Instance variable <c>minimumCollisionOffset</c> represents the minimum offset distance value assigned to the camera in case of collision with an object.
    /// </summary>
    public float minimumCollisionOffset = 0.2f;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public float lockedPivotPosition = 2.25f;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public float unlockedPivotPosition = 1.65f;

    /// <summary>
    /// Instance variable <c>maximumLockOnDistance</c> represents the maximum distance value the player can lock on a target.
    /// </summary>
    public float maximumLockOnDistance = 30.0f;

    /// <summary>
    /// Instance variable <c>currentLockOnTarget</c> is a Unity <c>Transform</c> component representing the current locked on target position, rotation and scale.
    /// </summary>
    public Transform currentLockOnTarget;
    
    /// <summary>
    /// Instance variable <c>nearestLockOnTarget</c> is a Unity <c>Transform</c> component representing the nearest lock on target position, rotation and scale.
    /// </summary>
    public Transform nearestLockOnTarget;

    /// <summary>
    /// Instance variable <c>leftLockTarget</c> is a Unity <c>Transform</c> component representing the nearest left lock target position, rotation and scale.
    /// </summary>
    public Transform leftLockTarget;

    /// <summary>
    /// Instance variable <c>rightLockTarget</c> is a Unity <c>Transform</c> component representing the nearest right lock target position, rotation and scale.
    /// </summary>
    public Transform rightLockTarget;

    /// <summary>
    /// Instance variable <c>_ignoreLayers</c> is a Unity <c>LayerMask</c> struct representing the layer mask of the camera game object.
    /// </summary>
    public LayerMask ignoreLayers;

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// This function is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Singleton = this;
        _myTransform = transform;
        _defaultPosition = cameraTransform.localPosition.z;
        ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        _inputHandler = FindObjectOfType<InputHandler>();
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    #endregion

    #region Public
    
    /// <summary>
    /// This function is responsible for moving the game object to follow another targeted game object.
    /// </summary>
    /// <param name="delta">A float value representing the elapsed time since the last game time tick.</param>
    public void FollowTarget(float delta)
    {
        Vector3 targetPosition = Vector3.SmoothDamp(_myTransform.position, targetTransform.position, 
                                                    ref _cameraFollowVelocity, delta / followSpeed);
        _myTransform.position = targetPosition;
        
        HandleCameraCollisions(delta);
    }

    /// <summary>
    /// This function is responsible for rotating the camera.
    /// </summary>
    /// <param name="delta">A float value representing the elapsed time since the last game time tick.</param>
    /// <param name="mouseXInput">A float value representing the mouse input movement value along the x axis.</param>
    /// <param name="mouseYInput">A float value representing the mouse input movement value along the y axis.</param>
    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
    {
        if (!_inputHandler.lockOnFlag && !currentLockOnTarget)
        {
            _lookAngle += (mouseXInput * lookSpeed) / delta;
            _pivotAngle -= (mouseYInput * pivotSpeed) / delta;

            _pivotAngle = Mathf.Clamp(_pivotAngle, minimumPivot, maximumPivot);
        
            Vector3 rotation = Vector3.zero;
            rotation.y = _lookAngle;
        
            Quaternion targetRotation = Quaternion.Euler(rotation);
            _myTransform.rotation = targetRotation;
        
            rotation = Vector3.zero;
            rotation.x = _pivotAngle;
        
            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
        }
        else
        {
            float velocity = 0;

            Vector3 direction = currentLockOnTarget.position - transform.position;
            direction.Normalize();
            direction.y = 0;
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;

            direction = currentLockOnTarget.position - cameraPivotTransform.position;
            direction.Normalize();
            
            targetRotation = Quaternion.LookRotation(direction);
            Vector3 eulerAngle = targetRotation.eulerAngles;
            eulerAngle.y = 0;
            cameraPivotTransform.localEulerAngles = eulerAngle;
        }
    }

    /// <summary>
    /// This function is responsible for handling the lock on target system.
    /// </summary>
    public void HandleLockOn()
    {
        float shortestDistance = Mathf.Infinity;
        float shortestDistanceOfLeftTarget = Mathf.Infinity;
        float shortestDistanceOfRightTarget = Mathf.Infinity;

        Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26.0f);

        foreach (Collider other in colliders)
        {
            CharacterManager character = other.GetComponent<CharacterManager>();

            if (character)
            {
                Vector3 lockTargetDirection = character.transform.position - targetTransform.position;
                float distanceFromTarget = Vector3.Distance(targetTransform.position, character.transform.position);

                float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);
                RaycastHit hit;
                
                if (character.transform.root != targetTransform.transform.root 
                    && viewableAngle > -50 && viewableAngle < 50 
                    && distanceFromTarget <= maximumLockOnDistance)
                {
                    if (Physics.Linecast(_playerManager.lockOnTransform.position, character.lockOnTransform.position,
                        out hit))
                    {
                        Debug.DrawLine(_playerManager.lockOnTransform.position, character.lockOnTransform.position);

                        if (!hit.transform.gameObject.CompareTag("Enemy"))
                        {
                            // Cannot lock on target, object in the way
                        }
                        else
                        {
                            _availableTargets.Add(character);
                        }
                    }
                }
            }
        }

        foreach (CharacterManager target in _availableTargets)
        {
            float distanceFromTarget = Vector3.Distance(targetTransform.position, target.transform.position);

            if (distanceFromTarget < shortestDistance)
            {
                shortestDistance = distanceFromTarget;
                nearestLockOnTarget = target.lockOnTransform;
            }

            if (_inputHandler.lockOnFlag)
            {
                Vector3 relativeEnemyPosition = currentLockOnTarget.InverseTransformPoint(target.transform.position);
                var distanceFromLeftTarget = currentLockOnTarget.transform.position.x - target.transform.position.x;
                var distanceFromRightTarget = currentLockOnTarget.transform.position.x + target.transform.position.x;

                if (relativeEnemyPosition.x > 0.0f && distanceFromLeftTarget < shortestDistanceOfLeftTarget)
                {
                    shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                    leftLockTarget = target.lockOnTransform;
                }

                if (relativeEnemyPosition.x < 0.0f && distanceFromRightTarget < shortestDistanceOfRightTarget)
                {
                    shortestDistanceOfRightTarget = distanceFromRightTarget;
                    rightLockTarget = target.lockOnTransform;
                }
            }
        }
    }
    
    /// <summary>
    /// This function is responsible for clearing the lock on targets list.
    /// </summary>
    public void ClearLockOnTargets()
    {
        _availableTargets.Clear();
        nearestLockOnTarget = null;
        currentLockOnTarget = null;
        rightLockTarget = null;
        leftLockTarget = null;
    }

    #endregion

    #region Private

    /// <summary>
    /// This function is responsible for camera collisions computation.
    /// </summary>
    /// <param name="delta">A float value representing the elapsed time since the last game time tick.</param>
    private void HandleCameraCollisions(float delta)
    {
        _targetPosition = _defaultPosition;

        // Compute camera orientation direction relative to the camera pivot
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();
        
        // Compute sphere raycast around the camera pivot
        RaycastHit hit;
        if (Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit,
            Mathf.Abs(_targetPosition), ignoreLayers))
        {
            float distance = Vector3.Distance(cameraPivotTransform.position, hit.point);
            _targetPosition = -(distance - cameraCollisionOffset);
        }
        
        if (Mathf.Abs(_targetPosition) < minimumCollisionOffset)
        {
            _targetPosition = -minimumCollisionOffset;
        }

        _cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, _targetPosition, delta / 0.2f);
        cameraTransform.localPosition = _cameraTransformPosition;
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    public void SetCameraHeight()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 newLockedPosition = new Vector3(0, lockedPivotPosition);
        Vector3 newUnlockedPosition = new Vector3(0, unlockedPivotPosition);

        if (currentLockOnTarget)
        {
            cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(
                cameraPivotTransform.transform.localPosition, newLockedPosition, ref velocity, Time.deltaTime);
        }
        else
        {
            cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newUnlockedPosition, ref velocity, Time.deltaTime);
        }
    }

    #endregion
}
