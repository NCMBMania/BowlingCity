using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class PlaySERandomByCollider : MonoBehaviour {

    public AudioClip[] selist;

    void Start()
    {
        if (selist.Length == 0)
        {
            Debug.Log("SE Audioclips are not found");
        }
    }

    void OnCollisionEnter()
    {
        if (selist.Length != 0)
        {
            //int型は最大値maxを含まない
            int rndindex = Random.Range(1, selist.Length);

            SoundManager.Instance.PlaySE(selist[rndindex], 1f);
        }
    }
}
