using UnityEngine;
using System.Collections;

public class AttackBehaviour : GenericBehaviour
{
  public string attackButton = "Attack";              // Default attack button.

	private int attackBool;                          // Animator variable related to attacking.
	private bool attack = false;                     // Boolean to determine whether or not the player activated attack mode.
    private bool isAttacking = false;

	void Start()
	{
		// Set up the references.
		attackBool = Animator.StringToHash("Attack");
		// Subscribe this behaviour on the manager.
		behaviourManager.SubscribeBehaviour(this);
	}

	// Update is used to set features regardless the active behaviour.
	void Update()
	{
		// Toggle attack by input, only if there is no overriding state or temporary transitions.
		if (Input.GetButtonDown(attackButton) && !behaviourManager.IsOverriding() 
			&& !behaviourManager.GetTempLockStatus(behaviourManager.GetDefaultBehaviour))
		{
			attack = !attack;

			// Force end jump transition.
			behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);

			// Player is attacking.
			if (attack)
			{
				// Register this behaviour.
				behaviourManager.RegisterBehaviour(this.behaviourCode);
                StartCoroutine ( ParticleEffects() );
			}
			else
			{
				// Set camera default offset.
				behaviourManager.GetCamScript.ResetTargetOffsets();

				// Unregister this behaviour and set current behaviour to the default one.
				behaviourManager.UnregisterBehaviour(this.behaviourCode);
			}
		}

		// Assert this is the active behaviour
		attack = attack && behaviourManager.IsCurrentBehaviour(this.behaviourCode);

		// Set attack related variables on the Animator Controller.
		behaviourManager.GetAnim.SetBool(attackBool, attack);
	}

	// This function is called when another behaviour overrides the current one.
	public override void OnOverride()
	{
		
	}

	// LocalFixedUpdate overrides the virtual function of the base class.
      // CORRECTLY OVERRIDING AND GIVING IT BACK WHEN ITS OVER
	public override void LocalFixedUpdate()
	{
		// Call the attack manager.
		AttackManagement(behaviourManager.GetH, behaviourManager.GetV);
	}
	// Deal with the player movement when attacking.
	void AttackManagement(float horizontal, float vertical)
	{
		if(behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && 
           !behaviourManager.GetAnim.IsInTransition(0))
           {
             attack = false;
             behaviourManager.GetAnim.SetBool(attackBool, attack);
             behaviourManager.UnregisterBehaviour(this.behaviourCode);
           }
	}

    public GameObject particleAttack = null;
    public GameObject socket = null;
    public float createTime1 = 0.1f;
    public float createTime2 = 0.1f;
    public float createTime3 = 0.1f;
    public float attackTime1 = 0.8f;
    public float attackTime2 = 0.5f;
    public float attackTime3 = 1.0f;
    
    IEnumerator ParticleEffects ()
    {
        if(!isAttacking)
        {
            isAttacking = true;

        yield return new WaitForSeconds (createTime1);
        GameObject p1 = CreateParticleEffect();
        yield return new WaitForSeconds (attackTime1);
        MoveParticleEffect(p1);

        yield return new WaitForSeconds (createTime2);
        GameObject p2 = CreateParticleEffect();
        yield return new WaitForSeconds (attackTime2);
        MoveParticleEffect(p2);

        // yield return new WaitForSeconds (createTime3);
        // GameObject p3 = CreateParticleEffect();
        // yield return new WaitForSeconds (attackTime3);
        // MoveParticleEffect(p3);

        isAttacking =false;
        }
    }

    GameObject CreateParticleEffect()
    {
        GameObject obj = Instantiate (particleAttack);
        obj.transform.position = socket.transform.position;
        obj.transform.parent = socket.transform;
        return obj;
    }

    void MoveParticleEffect (GameObject obj)
    {
        obj.transform.parent = null;
        obj.transform.localRotation = this.transform.localRotation;
        obj.GetComponent<Mover>().enabled = true;
    }
}
