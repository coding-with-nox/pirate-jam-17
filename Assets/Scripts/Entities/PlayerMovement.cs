using UnityEngine;

public class PlayerMovement : EntityMovement
{
	//Aggiunta manuale del Singleton
	public static PlayerMovement self;
	public static PlayerMovement I
	{
		get
		{
#if UNITY_EDITOR
			if (self == null)
				if (Application.isPlaying == false)
					self = (PlayerMovement)FindAnyObjectByType(typeof(PlayerMovement));
#endif
			return self;
		}
		protected set { self = value; }
	}

	protected virtual void Awake()
	{
		// If there is an instance, and it's not me, delete myself.
		if (I != null && I != this)
		{
			Debug.LogWarning($"There is already an instance of type {typeof(PlayerMovement)}");
			Destroy(this);
		}
		else
		{
			I = this as PlayerMovement;
		}
	}
	//ILFS
	bool running;
	Controls inputScheme;
	[SerializeField]float stopSpeed, speedDampening;
	[SerializeField]int dashCost;
	
	bool openingChest;
	
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
		if (openingChest){
			CloseChest();
			return;
		}
		player.Interact();
	}
	public void RegisterMessages(){
		inputScheme = new Controls();
        inputScheme.Player.Enable();
		inputScheme.Player.Interact.performed += InteractTrigger;
		inputScheme.Player.Sprint.performed += Dash;
		inputScheme.Player.Attack.performed += Attack;
	}
	public void DeRegisterMessages(){
        inputScheme.Player.Disable();
		inputScheme.Player.Interact.performed -= InteractTrigger;
		inputScheme.Player.Sprint.performed -= Dash;
		inputScheme.Player.Attack.performed -= Attack;
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
	void Attack (UnityEngine.InputSystem.InputAction.CallbackContext action){
		if (inputScheme.Player.Mouse.ReadValue<Vector2>().y >= BottomUI.I.GetUIHeight() && !IsInteracting()){
			Vector2 attackVector = (Vector2)Camera.main.ScreenToWorldPoint(inputScheme.Player.Mouse.ReadValue<Vector2>());
			attackVector -= (Vector2)transform.position;
			player.GenerateBaseAttack(attackVector.normalized,attackVector.magnitude);
		}
	}
	public void OpenChest(){
		openingChest = true;
	}
	public void CloseChest(){
		openingChest = false;
		ContainerInventory.I.Hide();
		BottomUI.I.ReleaseForced();
		BottomUI.I.Raise(false);
	}
	public bool IsInteracting (){//in caso ce ne siano altri
		return (openingChest);
	}
	
}
