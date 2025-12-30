using System;
using Unity.Cinemachine;
using UnityEngine;

namespace foursoulsauto.ui.player
{
    public class PlayzoneCameraTarget : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D boundingBox;
        [SerializeField] private CinemachineCamera followCamera;
        [SerializeField] private float slideSpeed = 0.1f;
        [SerializeField] private float lensMin;
        [SerializeField] private float lensMax;
        [SerializeField] private float lensSpeed;
        
        private float _boundWidth;
        private float _boundHeight;

        private void Start()
        {
            boundingBox.enabled = false;
            _boundWidth = boundingBox.size.x / 2;
            _boundHeight = boundingBox.size.y / 2;
        }

        private void LateUpdate()
        {
            updatePosition();
            updateLens();
        }

        private void updateLens()
        {
            var scrollDelta = Input.GetAxis("Mouse ScrollWheel");
            if (!(Mathf.Abs(scrollDelta) > 0)) return;
            followCamera.Lens.OrthographicSize -= scrollDelta * lensSpeed * Time.deltaTime;
            followCamera.Lens.OrthographicSize = Mathf.Clamp(followCamera.Lens.OrthographicSize, lensMin, lensMax);
        }

        private void updatePosition()
        {
            var moveDelta = Vector2.zero;
            if (Input.GetKey(KeyCode.A)) moveDelta.x += -slideSpeed;
            if (Input.GetKey(KeyCode.D)) moveDelta.x += slideSpeed;
            if (Input.GetKey(KeyCode.W)) moveDelta.y += slideSpeed;
            if (Input.GetKey(KeyCode.S)) moveDelta.y += -slideSpeed;
            moveDelta *= Time.deltaTime;
            
            transform.Translate(moveDelta);
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -_boundWidth, _boundWidth),
                Mathf.Clamp(transform.position.y, -_boundHeight, _boundHeight),
                transform.position.z
            );
        }
        
    }
}