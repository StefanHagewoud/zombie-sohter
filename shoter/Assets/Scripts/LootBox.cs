using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    public WeightedRandomList<Transform> lootTable;

    public Transform itemHolder;

    Animator animator;

    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    void HideItem()
    {
        itemHolder.localScale = Vector3.zero;
        itemHolder.gameObject.SetActive(false);

        foreach (Transform child in itemHolder)
        {
            Destroy(child.gameObject);
        }
    }

    public void ShowItem()
    {
        Transform item = lootTable.GetRandom();
        Instantiate(item, itemHolder.position, itemHolder.rotation);
        itemHolder.gameObject.SetActive(true);
    }
}
