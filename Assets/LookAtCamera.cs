using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool invert;
    private Transform _cameraTransform;
    
    private void Awake()
    {
        if (Camera.main != null) 
            _cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        LookAtTest();
    }

    
    private void LookAt()
    {
        if (invert)
        {
            var dirToCamera = (_cameraTransform.position - transform.position).normalized;
            transform.LookAt(transform.position + dirToCamera * -1);
        }
        else
        {
            transform.LookAt(_cameraTransform);
        }
    }
    
    private void LookAtTest()
    {
        transform.LookAt(transform.position + _cameraTransform.rotation * Vector3.forward, _cameraTransform.rotation * Vector3.up);
    }
}
