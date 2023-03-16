using UnityEngine;

public class HealBehaviour : GenericBehaviour
{
  public string healButton = "Heal";              // Default heal button.
	private int healBool;                          // Animator variable related to healing.
	private bool heal = false;                     // Boolean to determine whether or not the player activated heal mode.
    public bool heatTrigger = false;
	void Start()
	{
		// Set up the references.
		healBool = Animator.StringToHash("Heal");
		// Subscribe this behaviour on the manager.
		behaviourManager.SubscribeBehaviour(this);
	}

	// Update is used to set features regardless the active behaviour.
	void Update()
	{
		// Toggle heal by input, only if there is no overriding state or temporary transitions.
		if ((Input.GetButtonDown(healButton) || heatTrigger) && !behaviourManager.IsOverriding() 
			&& !behaviourManager.GetTempLockStatus(behaviourManager.GetDefaultBehaviour))
		{
			heal = !heal;
            heatTrigger = false;

			// Force end jump transition.
			behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);

			// Player is healing.
			if (heal)
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
		heal = heal && behaviourManager.IsCurrentBehaviour(this.behaviourCode);

		// Set heal related variables on the Animator Controller.
		behaviourManager.GetAnim.SetBool(healBool, heal);
	}

	// This function is called when another behaviour overrides the current one.
	public override void OnOverride()
	{
		
	}

	// LocalFixedUpdate overrides the virtual function of the base class.
      // CORRECTLY OVERRIDING AND GIVING IT BACK WHEN ITS OVER
	public override void LocalFixedUpdate()
	{
		// Call the heal manager.
		HealManagement(behaviourManager.GetH, behaviourManager.GetV);
	}
	// Deal with the player movement when healing.
	void HealManagement(float horizontal, float vertical)
	{       
		if(behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && 
           !behaviourManager.GetAnim.IsInTransition(0))
           {
             heal = false;
             behaviourManager.GetAnim.SetBool(healBool, heal);
             behaviourManager.UnregisterBehaviour(this.behaviourCode);
           }
	}
}