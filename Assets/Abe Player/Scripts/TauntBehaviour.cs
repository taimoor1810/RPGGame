using UnityEngine;

public class TauntBehaviour : GenericBehaviour
{
  public string tauntButton = "Taunt";              // Default taunt button.

	private int tauntBool;                          // Animator variable related to taunting.
	private bool taunt = false;                     // Boolean to determine whether or not the player activated taunt mode.

	void Start()
	{
		// Set up the references.
		tauntBool = Animator.StringToHash("Taunt");
		// Subscribe this behaviour on the manager.
		behaviourManager.SubscribeBehaviour(this);
	}

	// Update is used to set features regardless the active behaviour.
	void Update()
	{
		// Toggle taunt by input, only if there is no overriding state or temporary transitions.
		if (Input.GetButtonDown(tauntButton) && !behaviourManager.IsOverriding() 
			&& !behaviourManager.GetTempLockStatus(behaviourManager.GetDefaultBehaviour))
		{
			taunt = !taunt;

			// Force end jump transition.
			behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);

			// Player is taunting.
			if (taunt)
			{
				// Register this behaviour.
				behaviourManager.RegisterBehaviour(this.behaviourCode);
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
		taunt = taunt && behaviourManager.IsCurrentBehaviour(this.behaviourCode);

		// Set taunt related variables on the Animator Controller.
		behaviourManager.GetAnim.SetBool(tauntBool, taunt);
	}

	// This function is called when another behaviour overrides the current one.
	public override void OnOverride()
	{
		
	}

	// LocalFixedUpdate overrides the virtual function of the base class.
      // CORRECTLY OVERRIDING AND GIVING IT BACK WHEN ITS OVER
	public override void LocalFixedUpdate()
	{
		// Call the taunt manager.
		TauntManagement(behaviourManager.GetH, behaviourManager.GetV);
	}
	// Deal with the player movement when taunting.
	void TauntManagement(float horizontal, float vertical)
	{
		if(behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && 
           !behaviourManager.GetAnim.IsInTransition(0))
           {
             taunt = false;
             behaviourManager.GetAnim.SetBool(tauntBool, taunt);
             behaviourManager.UnregisterBehaviour(this.behaviourCode);
           }
	}
}