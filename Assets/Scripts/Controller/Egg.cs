using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Egg : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _eggSprite;
    private EggData _data;
    private Tiles tile;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(RandomJump());
    }

    private IEnumerator RandomJump()
    {
        while (true)
        {
            float waitTime = UnityEngine.Random.Range(1f, 5f);
            yield return new WaitForSeconds(waitTime);

            if (gameObject.activeInHierarchy)
            {
                _animator.SetTrigger("jump");
                Debug.Log("egg jump");
            }
        }
    }

    public void Setup(EggData data, Tiles tile)
    {
        _data = data;
        this.tile = tile;
        _eggSprite.sprite = data.eggSprite;
    }
}