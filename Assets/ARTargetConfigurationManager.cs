using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARTargetConfigurationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _targetPrefab = null;
    private ARPerson _target = null;

    private ARTrackedImageManager _imageManager = null;
    private string _currentTrackedObjectName = null;

    void Start()
    {
        _imageManager = GetComponentInParent<ARTrackedImageManager>();
        _imageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs changedImages)
    {
        if (changedImages.added.Count > 0)
        {
            InstantiatePrefab(changedImages);
        }
        else if (changedImages.removed.Count > 0)
        {
            DestroyPrefab();
        }

        if (_currentTrackedObjectName != null)
        {
            UpdatePosition(changedImages);
        }
    }

    private void InstantiatePrefab(ARTrackedImagesChangedEventArgs changedImages)
    {
        _currentTrackedObjectName = changedImages.added[0].referenceImage.name;
        _target = Instantiate(_targetPrefab, Vector3.zero, Quaternion.identity, transform)
        .GetComponent<ARPerson>();
        _target.SetTargetName(_currentTrackedObjectName);
    }

    private void DestroyPrefab()
    {
        Destroy(_target.gameObject);
        _target = null;
        _currentTrackedObjectName = null;
    }

    private void UpdatePosition(ARTrackedImagesChangedEventArgs changedImages)
    {
        _target.transform.position = changedImages.updated[0].transform.position;
        _target.transform.rotation = changedImages.updated[0].transform.rotation;
    }

    public void ThrowBall()
    {
        if (_target != null)
        {
            _target.ThrowBall();
        }
    }

}
