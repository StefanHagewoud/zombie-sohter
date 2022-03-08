using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RobotSummon : MonoBehaviour
{
    PhotonView pv;
    public ParticleSystem summon;

    [HideInInspector]public GameObject robotToSpawn;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        SpawnRobot();
    }

    public void SpawnRobot()
    {
        StartCoroutine(Spawn());
        IEnumerator Spawn()
        {
            summon.Play();
            yield return new WaitForSecondsRealtime(1.5f);
            PhotonNetwork.Instantiate(robotToSpawn.name, gameObject.transform.position, gameObject.transform.rotation);
            yield return new WaitForSecondsRealtime(1);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
