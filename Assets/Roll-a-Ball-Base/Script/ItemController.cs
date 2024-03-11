using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Burst.CompilerServices;

[RequireComponent(typeof(AudioSource))]
public class ItemController : MonoBehaviour
{
    [SerializeField] Transform rotateItem;
    [SerializeField] float rotateSpeed;
    [Header("プレイヤー接触時の入手演出に使う時間")]
    [SerializeField] float performanceTime = 0.25f;
    [Header("入手演出時にコインが飛び上がる距離")]
    [SerializeField] float jumpDistance = 1f;
    [Header("入手演出時にコインのサイズを通常時の何倍するか")]
    [SerializeField] float maxScale = 1.5f;
    private Vector3 normalScale;
    private AudioSource audioSource;
    private Collider col;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        col = GetComponent<Collider>();
        normalScale = rotateItem.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        rotateItem.eulerAngles += new Vector3(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 接触対象はPlayerタグですか？
        if (other.CompareTag("Player"))
        {
            // このコンポーネントを持つGameObjectを破棄する
            audioSource.Play();
            col.enabled = false;
            gameObject.transform.tag = "BrokenItem";

            Get();
        }
    }

    private void Get()
    {
        //DOTweenで演出を行う
        float currentY = rotateItem.localPosition.y;
        var moveY = DOTween.Sequence();
        var scaleChange = DOTween.Sequence();

        moveY.Append(rotateItem.DOLocalMoveY(currentY + jumpDistance, performanceTime).SetEase(Ease.OutCubic))
            .Append(rotateItem.DOLocalMoveY(currentY, performanceTime).SetEase(Ease.InCubic));

        scaleChange.Append(rotateItem.DOScale(normalScale * maxScale, performanceTime).SetEase(Ease.OutCubic))
            .Append(rotateItem.DOScale(Vector3.zero, performanceTime).SetEase(Ease.InCubic).OnComplete(Complete));
    }

    //Tween完了時にコールバックで呼び出される
    private void Complete()
    {
        gameObject.SetActive(false);
    }
}
