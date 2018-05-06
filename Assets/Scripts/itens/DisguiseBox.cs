using System;
using UnityEngine;
using System.Collections;
using cakeslice;

public class DisguiseBox : MonoBehaviour, Actionable {
    public float OutlineTriggerRadious = 1.5f;
    private Outline OutlineEffect;

    void Start()
    {
        OutlineEffect = GetComponent<Outline>();

        OutlineEffect.enabled = false;
    }

    public IEnumerable doAction(Player p) {

        if (!p.disguised)
        {
            FeedbackMessage.getInstance().AddMessage("Voce pegou um disfarce", 5);

            p.disguised = true;
        }

        yield return null;
    }

    public void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, OutlineTriggerRadious, LayerMask.GetMask("Player"));

        if (colliders.Length > 0 && OutlineEffect.enabled == false)
        {
            OutlineEffect.enabled = true;
        }
        else if (OutlineEffect.enabled == true)
        {
            OutlineEffect.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, OutlineTriggerRadious);
    }
}
