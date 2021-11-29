using UnityEngine;
using UnityEngine.UI;

public class InBattleEnemySystem : MonoBehaviour
{
	public EnemyData data;

	[HideInInspector]
	public float hp, thisdef;
	[HideInInspector]
	public string state;
	[HideInInspector]
	public bool changeData;

	[SerializeField][Header("Image Prefab")]
	private GameObject neutral, breaks, rage, defensive;


    ///GuilleUCM/ObjectShake.cs from github
    private Vector3 originPosition;
    private Quaternion originRotation;
	private Image img;

	public float minCooldownTimer;
    public float shake_decay = 0.002f;
    public float shake_intensity = .3f;

    private float temp_shake_intensity = 0, defCooldownTimer;

	float randEv, thisdmg, defcooldown, invoketime, maxinvoketime;
	bool attacking;
	string currState;

	void Start()
	{
		hp = data.health;
		maxinvoketime = TurnSystem.instance.maxturntimer*6;
		attacking = true;
		randEv = Random.Range(minCooldownTimer, minCooldownTimer*2);
		defCooldownTimer = minCooldownTimer;
		state = EnemyState.NEUTRAL;
		thisdef = data.defense;
		thisdmg = data.damage;
		defcooldown = minCooldownTimer;
		invoketime = maxinvoketime;

		img = GetComponent<Image>();
		OnNeutral();
	}

    void Update ()
	{
		ShakeDecay();

		if (CommandLineSystem.instance.showHelp) 
			{ minCooldownTimer *= 2; } else { minCooldownTimer = defCooldownTimer; }

		if (HealthSystem.health > 0)
		{
			if (!attacking)
			{
				OptionsSystem.instance.PlayerHurt(data, thisdmg);
				attacking = true;
			} else if (attacking)
			{ randEv -= Time.deltaTime; }

			if (state != EnemyState.NEUTRAL)
				Invoke("ResetState", invoketime);

			if (randEv <= 0) { attacking = false;randEv = Random.Range(minCooldownTimer, minCooldownTimer*2); }
			if (hp <= 0f) { ScoreCounter.bugexecuted++; Destroy(this.gameObject); }
		}
	}

	public void OnNeutral()
	{
		if (state != EnemyState.NEUTRAL) { Instantiate(neutral, this.transform); }
		state = EnemyState.NEUTRAL;
		thisdmg = data.damage;
		thisdef = data.defense;
		img.color = Color.white; 
		minCooldownTimer = defcooldown; 
		changeData = true;
	}

	public void OnBreak()
	{
		if (state != EnemyState.BREAK) { Instantiate(breaks, this.transform); }
		state = EnemyState.BREAK;
		thisdmg = data.damage;
		thisdef = data.defense/2;
		img.color = Color.magenta; 
		minCooldownTimer = defcooldown/2; 
		changeData = true;
	}

	public void OnRage()
	{
		if (state != EnemyState.RAGE) { Instantiate(rage, this.transform); }
		state = EnemyState.RAGE;
		thisdmg = data.damage*2;
		thisdef = data.defense*0;
		img.color = Color.red; 
		minCooldownTimer = defcooldown/2; 
		changeData = true;
	}

	public void OnDefensive()
	{
		if (state != EnemyState.DEFENSIVE) { Instantiate(defensive, this.transform); }
		state = EnemyState.DEFENSIVE;
		thisdmg = data.damage/2;
		thisdef = data.defense*2;
		img.color = Color.cyan; 
		minCooldownTimer = defcooldown*2; 
		changeData = true;
	}

	void ResetState()
		=> state = EnemyState.NEUTRAL;

	public void ResetInvokeTimer()
		=> invoketime = maxinvoketime;

	void ShakeDecay()
	{
		if (temp_shake_intensity > 0){
			transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
			transform.rotation = new Quaternion(
				originRotation.x ,
				originRotation.y + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.z + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .02f,
				originRotation.w );
			temp_shake_intensity -= shake_decay;
		}
	}
	
	public void Shake()
	{
		originPosition = transform.position;
		originRotation = transform.rotation;
		temp_shake_intensity = shake_intensity;
	}
}
