using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    public WeightedRandomList<Transform> lootTable;

    public Transform itemHolder;
    public bool slotFull;
    public ParticleSystem lootBoxEffect;
    public ParticleSystem lootBoxEffect2;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void HideItem()
    {
        //itemHolder.localScale = Vector3.zero;
        //itemHolder.gameObject.SetActive(false);
        slotFull = false;
        animator.SetBool("Open", false);

        foreach (Transform child in itemHolder)
        {
            Destroy(child.gameObject);
        }
    }

    public void ShowItem()
    {
        if (slotFull == false)
        {
            GameObject.FindGameObjectWithTag("Plakband").GetComponent<Plakband>().Playanimation();
            animator.SetBool("Open", true);
            Invoke("SpawnItem", 2);
            lootBoxEffect.Play();
            lootBoxEffect2.Play();
            slotFull = true;
        }
    }

    public void SpawnItem()
    {
        Transform item = lootTable.GetRandom();
        Instantiate(item, itemHolder);
        itemHolder.gameObject.SetActive(true);
        //slotFull = true;
        Invoke("HideItem", 10);
    }
}
