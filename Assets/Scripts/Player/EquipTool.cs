using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;

    [Header("Resource Gathering")]
    public bool canGatherWood;
    public bool canGatherStone;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;

    // components
    private Animator anim;
    private Camera cam;

    void Awake ()
    {
        // get our components
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }
    private AudioSource audioSource;
    
    // called when we press the attack input
    public override void OnAttackInput ()
    {
        if(!attacking)
        {
            attacking = true;
            anim.SetTrigger("Attack");
            Invoke("OnCanAttack", attackRate);

        }
    }

    // called when we're able to attack again
    void OnCanAttack ()
    {
        attacking = false;
    }
    
    public void OnHit()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Wizualizacja raycastu w oknie Scene (2 razy dłuższy)
        Debug.DrawRay(ray.origin, ray.direction * (attackDistance * 3), Color.red, 1.0f);

        if (Physics.Raycast(ray, out hit, attackDistance * 3))
        {
            // did we hit a resource?
            if (canGatherStone && hit.collider.GetComponent<Resource>() && hit.collider.CompareTag("Stone"))
            {
                hit.collider.GetComponent<Resource>().Gather(hit.point, hit.normal);
                Debug.Log("Stone");
                AudioManager.instance.PlaySound(AudioManager.instance.pickaxeSound);
            }
            if (canGatherWood && hit.collider.GetComponent<Resource>() && hit.collider.CompareTag("Wood"))
            {
                hit.collider.GetComponent<Resource>().Gather(hit.point, hit.normal);
                Debug.Log("Wood");
                AudioManager.instance.PlaySound(AudioManager.instance.axeSound);
            }
            if (hit.collider.GetComponent<Resource>() && hit.collider.CompareTag("Teleport"))
            {
                Debug.Log("Teleport");
            }
                // did we hit a damagable?
            if (doesDealDamage && hit.collider.GetComponent<IDamagable>() != null)
            {
                hit.collider.GetComponent<IDamagable>().TakePhysicalDamage(damage);
            }
        }
    }

}