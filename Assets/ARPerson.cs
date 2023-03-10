using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ARPerson : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _targetName = null;

    [SerializeField]
    private GameObject _ball = null;

    [SerializeField]
    private Transform _throwOrigin = null;

    [SerializeField]
    private Vector3 _size = Vector3.one;

    public void SetTargetName(string name)
    {
        _targetName.text = name;
    }

    // private void Start()
    // {
    //     ThrowBallWithExtraForce();
    // }

    public void ThrowBall()
    {
        GameObject ball = Instantiate(_ball, _throwOrigin.position, _throwOrigin.rotation, _throwOrigin);
        ball.transform.localScale = _size;
        Vector3 direction = _ball.transform.forward + _ball.transform.up;
        ball.GetComponent<Rigidbody>().AddForce(direction * 20);
    }

    public async void ThrowBallWithExtraForce()
    {
        await new WaitForSeconds(1f);

        GameObject ball = Instantiate(_ball, _throwOrigin.position, _throwOrigin.rotation, _throwOrigin);
        ball.transform.localScale = _size;
        Vector3 direction = _ball.transform.forward + _ball.transform.up;

        ball.GetComponent<Rigidbody>().AddForce(direction * 10);
        for (int i = 0; i < 10; i++)
        {
            ball.GetComponent<Rigidbody>().AddForce(direction * 1);
            await new WaitForSeconds(0.1f);
        }
    }
}
