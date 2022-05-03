using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShoot : MonoBehaviour
{
    public float time=3f;
    private float seconds;
    private Animator anim;

    private bool isShooting=false;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        //F.Prefabs.Fireball
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;

        if (seconds > time && !isShooting)
        {
            isShooting = true;
            StartCoroutine(FireBall_Shoot());
        }
    }

    IEnumerator FireBall_Shoot()
    {
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.25f);

        Instantiate(F.Prefabs.Fireball, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);

        yield return new WaitForSeconds(3f);
        isShooting = false;
        seconds = 0;
    }
}
