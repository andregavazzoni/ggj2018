using UnityEngine;
using System.Collections;
using cakeslice;

public class CollectableInfoItem : MonoBehaviour, Actionable {

    public float OutlineTriggerRadious = 1.5f;
    private Outline OutlineEffect;

    void Start()
    {
        OutlineEffect = GetComponent<Outline>();

        OutlineEffect.enabled = false;
    }

    public IEnumerable doAction(Player p) {
        Config.getInstance().UpdateCollectedInfos();
        FeedbackMessage.getInstance().AddMessage("Voce pegou uma informação", 5);
        Destroy(gameObject);
        yield return null;
    }

    public void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, OutlineTriggerRadious, LayerMask.GetMask("Player"));

        if (colliders.Length > 0 && OutlineEffect.enabled == false)
        {
            OutlineEffect.enabled = true;
        } else if (OutlineEffect.enabled == true)
        {
            OutlineEffect.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, OutlineTriggerRadious);
    }
}