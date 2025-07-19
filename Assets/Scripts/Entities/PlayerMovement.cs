using UnityEngine;

public class PlayerMovement : EntityMovement
{
	//ILFS
	bool running;
	Controls inputScheme;
	[SerializeField]float stopSpeed, speedDampening;
	[SerializeField]int dashCost;
	
	Player player;
    protected override void Move(){
		Vector2 input = inputScheme.Player.Move.ReadValue<Vector2>();
		if (dashCooldownRemaining>0){
			dashCooldownRemaining -= TimeManager.time;
		}
		if (dashTimeRemaining > 0){
			dashTimeRemaining -= TimeManager.time;
		}
		else if (input != Vector2.zero){
			Accellerate(input);
		}
		else if (GetSpeedMagnitude()>stopSpeed){
			MultiplySpeed(speedDampening);
		}
		else {
			MultiplySpeed(0);
		}
	}
	public override void Setup(){
		base.Setup();
		RegisterMessages();
		player = GetComponent<Player>();
		if (player == null){
			print (""+this+" non ha una classe Player");
		}
	}
	void InteractTrigger (UnityEngine.InputSystem.InputAction.CallbackContext action){
		//print ("1");
		player.Interact();
	}
	public void RegisterMessages(){
		inputScheme = new Controls();
        inputScheme.Player.Enable();
		inputScheme.Player.Interact.performed += InteractTrigger;
		inputScheme.Player.Sprint.performed += Dash;
	}
	public void DeRegisterMessages(){
        inputScheme.Player.Disable();
		inputScheme.Player.Interact.performed -= InteractTrigger;
		inputScheme.Player.Sprint.performed -= Dash;
	}
	void Dash (UnityEngine.InputSystem.InputAction.CallbackContext action){
		
		Vector2 input = inputScheme.Player.Move.ReadValue<Vector2>();
		if (dashCooldownRemaining<= 0 && input != Vector2.zero && player.ChangeValueIfPossible(Entity.Resource.stamina,-dashCost)){
			dashTimeRemaining = dashTime;
			RotateSpeed(input);
			AccellerateToMax();
			MultiplySpeed(dashSpeedMultiplier);
		}
	}
	
}
