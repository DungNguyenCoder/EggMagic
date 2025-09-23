using UnityEngine;
using System.Collections.Generic;

public class EggPool : MonoBehaviour
{
    [SerializeField] private Egg _eggPrefab;
    [SerializeField] private int _amount = 20;
    private List<Egg> _eggPool = new List<Egg>();

    private void Awake()
    {
        for (int i = 0; i < _amount; i++)
        {
            Egg egg = Instantiate(_eggPrefab, this.transform);
            egg.gameObject.name = "Egg no: " + (i + 1);
            egg.gameObject.SetActive(false);
            _eggPool.Add(egg);
        }
    }

    public Egg GetEgg()
    {
        foreach (Egg egg in _eggPool)
        {
            if (!egg.gameObject.activeInHierarchy)
            {
                egg.gameObject.SetActive(true);
                return egg;
            }
        }
        Egg newEgg = Instantiate(_eggPrefab, this.transform);
        _eggPool.Add(newEgg);
        return newEgg;
    }

    public void ReturnEgg(Egg egg)
    {
        egg.gameObject.SetActive(false);
    }
}
