using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public int Health;
    public int Number_Of_Lifes;

    public Image[] Lifes;

    public Sprite Full_Life;
    public Sprite Empty_Life;

    private Renderer Player_Sprite;

    private bool isHitted;
    private bool isDead;
    private float minColInt = 0.4f, maxColInt = 1f;
    private float colInt;
    private Color c;

    public Rigidbody RB;

    private UnitInput Player_Script;
    public Time_Controller Time_Contr;

    public event System.Action OnDeath;

    private void Start()
    {
        Player_Sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        Player_Script = gameObject.GetComponentInChildren<UnitInput>();
        RB = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Health_Update();

        if (isHitted)
        {
            Player_Blinking();
        }
    }

    void Player_Blinking()
    {
        colInt = Random.Range(minColInt, maxColInt);

        c = Player_Sprite.material.color;
        c.a = colInt;
        Player_Sprite.material.color = c;
    }

    void Health_Update()
    {
        if (Health > Number_Of_Lifes)
        {
            Health = Number_Of_Lifes;
        }

        for (int i = 0; i < Lifes.Length; i++)
        {
            if (i < Health)
            {
                Lifes[i].sprite = Full_Life;
            }
            else
            {
                Lifes[i].sprite = Empty_Life;
            }

            if (i < Number_Of_Lifes)
            {
                Lifes[i].enabled = true;
            }
            else
            {
                Lifes[i].enabled = false;
            }
        }
        if (Health < 1)
        {
            OnDeath?.Invoke();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isHitted || isDead)
            return;
        
        if(other.gameObject.tag == "Enemy")
        {
            SessionEntity.Current.SFX.Play("Player_Hurt");
            isHitted = true;
            Health -= 1;
            
            if (Health >= 1)
            {
                StartCoroutine(Turn_On_HP(other.gameObject.transform.position));
            }
            else
            {
                Time_Contr.ShowDeadMenu();
                c.a = 1;
                Player_Sprite.material.color = c;
                isHitted = false;
            }
            

        }

    }

    IEnumerator Turn_On_HP(Vector3 other)
    {

        for (float t = 0f; t < 1; t += Time.deltaTime / F.Settings.CubeAttackTime)
        {
            var magnitude = 500;

            var force = new Vector3(transform.position.x - other.x, 0 , transform.position.z - other.z);
           // magnitude -= 100;
            
            force.Normalize();
            
            RB.AddForce(force * magnitude);
            yield return null;
        }

       // yield return new WaitForSeconds(3f);
        c.a = 1;
        Player_Sprite.material.color = c;
        isHitted = false;
    }
}
