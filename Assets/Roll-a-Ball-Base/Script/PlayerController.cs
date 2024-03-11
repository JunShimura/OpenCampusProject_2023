using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float animationFrameRate = 10f;
    public float animationRate = 10.0f;

    [SerializeField] GameObject brokenParticle;
    [SerializeField] float brokenTime = 0.5f;

    [SerializeField] Transform cameraRoot;
    [SerializeField] ThirdPersonController thirdPersonController;

    private void Reset()
    {
        GameController.gameController.player = this;
    }
    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        var gamepad = Gamepad.current;
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if (gamepad != null)
        {
            if (gamepad.leftStick.ReadValue().sqrMagnitude > 0.5f)
                GameController.gameController.isStarted = true;
        }
        if (input.sqrMagnitude > 0)
        {
            GameController.gameController.isStarted = true;
        }
    }

    public void SetBroken()
    {
        //破壊された時の処理
        GameObject particleInstance = GameObject.Instantiate(brokenParticle, transform.position, Quaternion.identity);
        GameController.gameController.ResetScene(brokenTime);
        thirdPersonController.isEnd = true;
        Destroy(particleInstance, brokenTime);
        Destroy(gameObject, brokenTime);
    }
    public void SetClear()
    {
        //レベルクリア時の処理
        thirdPersonController.isEnd = true;
        //transform.parent.LookAt(GameObject.Find("MainCamera").transform);
        //StartCoroutine(ClearAnimationCoroutine());
    }

    private IEnumerator ClearAnimationCoroutine()
    {
        for (; ; )
        {
            transform.parent.Rotate(Vector3.forward * animationRate);
            yield return new WaitForSeconds(1 / animationFrameRate);
        }

    }
}
