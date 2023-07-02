using System;
using UnityEngine;

namespace Core.Car.Experimental
{
    public class CarDamage : MonoBehaviour
    {
        private struct MeshData
        {
            public Vector3[] vertices;
        }

        private const string _ignoreTag = "ignore_car_collision";

        [SerializeField] private float _maxMoveDelta = 1.0f;
        [SerializeField] private float _maxCollisionStrength = 50.0f;
        [SerializeField] private float _verticalForceDamp = 0.1f;
        [SerializeField] private float _demolitionRange = 0.5f;
        [SerializeField] private float _impactDirectionManipulator = 0.0f;

        private MeshData[] _originalMeshData;
        private MeshFilter[] _meshfilters;
        private float _sqrDemolitionRange;
        private DateTime _lastCollisionTime;
        private TimeSpan _minCollisionTimeStep;

        private void Start()
        {
            _meshfilters = GetComponentsInChildren<MeshFilter>();
            _sqrDemolitionRange = _demolitionRange * _demolitionRange;
            _lastCollisionTime = DateTime.Now;
            _minCollisionTimeStep = new TimeSpan(0, 0, 0, 1, 0);

            LoadOriginalMeshData();
        }

        private void Update()
        {
            // For testing.
            if (Input.GetKeyDown(KeyCode.M))
            {
                Repair();
            }
        }

        private void LoadOriginalMeshData()
        {
            _originalMeshData = new MeshData[_meshfilters.Length];

            for (var i = 0; i < _meshfilters.Length; i++)
            {
                _originalMeshData[i].vertices = _meshfilters[i].mesh.vertices;
            }
        }

        private void Repair()
        {
            for (var i = 0; i < _meshfilters.Length; i++)
            {
                _meshfilters[i].mesh.vertices = _originalMeshData[i].vertices;
                _meshfilters[i].mesh.RecalculateNormals();
                _meshfilters[i].mesh.RecalculateBounds();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(_ignoreTag))
            {
                return;
            }

            if (DateTime.Now - _lastCollisionTime < _minCollisionTimeStep)
            {
                return;
            }

            _lastCollisionTime = DateTime.Now;

            var collisionRelativeVelocity = collision.relativeVelocity;
            var colliderPoint = transform.position - collision.contacts[0].point;

            collisionRelativeVelocity.y *= _verticalForceDamp;

            var collisionStrength = collisionRelativeVelocity.magnitude *
                Vector3.Dot(collision.contacts[0].normal,
                colliderPoint.normalized);

            OnMeshForce(collision.contacts[0].point,
                Mathf.Clamp01(collisionStrength / _maxCollisionStrength));
        }

        private void OnMeshForce(Vector3 originPos, float force)
        {
            force = Mathf.Clamp01(force);

            for (int j = 0; j < _meshfilters.Length; j++)
            {
                var vertices = _meshfilters[j].mesh.vertices;

                for (int i = 0; i < vertices.Length; i++)
                {
                    var scaledVert = Vector3.Scale(vertices[i], _meshfilters[j].transform.localScale);

                    var vertexWorldPosition = _meshfilters[j].transform.position +
                        (_meshfilters[j].transform.rotation * scaledVert);
                    var originRelativeDirection = vertexWorldPosition - originPos;
                    var flatVertexCenterDirection = transform.position -
                        vertexWorldPosition;
                    flatVertexCenterDirection.y = 0.0f;

                    if (originRelativeDirection.sqrMagnitude < _sqrDemolitionRange)
                    {
                        var distance = Mathf.Clamp01(
                            originRelativeDirection.sqrMagnitude / _sqrDemolitionRange);
                        var moveDelta = force * (1.0f - distance) * _maxMoveDelta;

                        var moveDirection = Vector3.Slerp(originRelativeDirection,
                            flatVertexCenterDirection, _impactDirectionManipulator).
                            normalized * moveDelta;

                        vertices[i] += Quaternion.Inverse(transform.rotation) *
                            moveDirection;
                    }

                }

                _meshfilters[j].mesh.vertices = vertices;
                _meshfilters[j].mesh.RecalculateBounds();
            }
        }
    }
}