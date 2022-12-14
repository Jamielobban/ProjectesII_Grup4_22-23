using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RecoilScript : MonoBehaviour
{

    public float _maximumOffsetDistance;
    public float _recoilAcceleration;
    public float _weaponRecoilStartSpeed;

    public bool _recoilInEffect;
    public bool _weaponHeadedBackToStartPosition;

    public Vector3 _offsetPosition;
    public Vector3 _recoilSpeed;
    private bool hasReachedPoint;


    public void AddRecoil()
    {
        //_weaponHeadedBackToStartPosition = false;
        //_recoilSpeed = -transform.up * _weaponRecoilStartSpeed;
        transform.DOLocalMove((new Vector3(-0.04f, -0.5f, 0.0f)), 0.05f, false);
        StartCoroutine(waitForPosition(0.06f));
    }
    private void Start()
    {
        _recoilSpeed = Vector3.zero;
        _offsetPosition = Vector3.zero;

        _recoilInEffect = false;
        _weaponHeadedBackToStartPosition = false;
    }

    private void Update()
    {
        UpdateRecoil();
    }

    private void UpdateRecoil()
    {
        if (!_recoilInEffect)
        {
            return;
        }
        else
        {
            transform.DOLocalMove((new Vector3(-0.04f, -1f, 0.0f)), 0.1f, false);
            _recoilInEffect = false;
        }
        if (hasReachedPoint)
        {
            _recoilSpeed += (-_offsetPosition.normalized) * _recoilAcceleration * Time.deltaTime;
            Vector3 newOffsetPosition = _offsetPosition + _recoilSpeed * Time.deltaTime;

            Vector3 newTransformPosition = transform.position - _offsetPosition;

            if (newOffsetPosition.magnitude > _maximumOffsetDistance)
            {
                _recoilSpeed = Vector3.zero;
                _weaponHeadedBackToStartPosition = true;
                newOffsetPosition = _offsetPosition.normalized * _maximumOffsetDistance;
            }
            else if (_weaponHeadedBackToStartPosition == true && newOffsetPosition.magnitude > _offsetPosition.magnitude)
            {
                transform.position -= _offsetPosition;
                _offsetPosition = Vector3.zero;

                _recoilInEffect = false;
                _weaponHeadedBackToStartPosition = false;
                return;
            }
            transform.position = newTransformPosition + newOffsetPosition;
            _offsetPosition = newOffsetPosition;
        }
    }
    private IEnumerator waitForPosition(float time)
    {
        yield return new WaitForSeconds(time);
        _recoilInEffect = true;
    }
}
