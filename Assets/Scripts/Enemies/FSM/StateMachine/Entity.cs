using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;


public abstract class Entity : MonoBehaviour
{
	[HideInInspector]
	public FiniteStateMachine stateMachine;

	[HideInInspector]
	public Transform player;
	[HideInInspector]
	public Vector3 vectorToPlayer;

	[SerializeField]
	protected SpriteRenderer sr;	
	[SerializeField]
	protected GameObject burnPrefab;	
	protected GameObject burnFireShader;
	protected float enemyHealth;
	protected Transform firePoint;
	Texture2D text;
	public Rigidbody2D rb { get; private set; }
	public Animator anim { get; private set; }
	public D_Entity enemyData;

	protected bool isDead;
	protected HealthStateTypes myHealthState;
	//protected float timeHealthStateEntered;
	protected float timeHealthStateExit;	
	protected float lastTimeDamageHealthStateApplied;
	protected bool maxHitBlendReached = false;
	protected bool applyingHitEffect = false;
	protected float lastTimeTouchedFire;
	bool sequenceCompleted = true;

	float tHE = 0;

	static Sequence sequenceImpactShader;
	static Tween shaker;

	private void Awake()
    {
		firePoint = GetComponentsInChildren<Transform>().Where(t => t.tag == "FirePoint").ToArray()[0];
		myHealthState = HealthStateTypes.NORMAL;		
		timeHealthStateExit = 0;
		lastTimeDamageHealthStateApplied = 0;
		enemyHealth = enemyData.health;

		//DOTween.Init();
		sequenceImpactShader = DOTween.Sequence();
		

	}

	public virtual void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();		
		if(anim == null)
        {
			anim = GetComponentsInChildren<Animator>().ToArray()[0];
		}
		player = GameObject.FindGameObjectWithTag("Player").transform;
		stateMachine = new FiniteStateMachine();		
    }
	public virtual void Update()
	{
		//Debug.Log(enemyData.health);				

		isDead = enemyHealth <= 0;

		vectorToPlayer = player.position - transform.position;
				
		CheckHealthStateEnding();		

		CheckIfHealthStateIsDamaging();

		UpdateBurnHItDamageValues();

		stateMachine.currentState.LogicUpdate();

		//Debug.Log(sequence.IsPlaying());
	}

 //   void CreateText()
 //   {
 //       text = new Texture2D((int)sr.sprite.rect.width, (int)sr.sprite.rect.height);
 //       var pixels = sr.sprite.texture.GetPixels((int)sr.sprite.textureRect.x,
 //                                                (int)sr.sprite.textureRect.y,
 //                                                (int)sr.sprite.textureRect.width,
 //                                                (int)sr.sprite.textureRect.height);
 //       text.SetPixels(pixels);
 //       text.Apply();

	//	sr.material.SetTexture("_MainTex", text);

	//}

	void CheckHealthStateEnding()
    {
		if (Time.time >= timeHealthStateExit)
		{
			if (myHealthState == HealthStateTypes.BURNED)
			{
				tHE = 0;
				Destroy(burnFireShader);
			}
			myHealthState = HealthStateTypes.NORMAL;
		}
	}

	void CheckIfHealthStateIsDamaging()
    {
		if (myHealthState != HealthStateTypes.NORMAL)
		{
			switch (myHealthState)
			{
				case HealthStateTypes.BURNED:
					if (Time.time - lastTimeDamageHealthStateApplied >= 2)
					{
						applyingHitEffect = true;
						GetDamage(10);
						lastTimeDamageHealthStateApplied = Time.time;
						lastTimeTouchedFire = Time.time;
					}
					break;
				case HealthStateTypes.FREEZE:
					break;
				case HealthStateTypes.PARALYZED:
					break;
				default:
					break;
			}
		}
	}

	void UpdateBurnHItDamageValues()
    {
		if (applyingHitEffect)
		{
			float minimum;
			float maximum;
			sr.material.SetFloat("_HitEffectGlow", 10);

			if (!maxHitBlendReached)
			{
				minimum = 0;
				maximum = 1;

				sr.material.SetFloat("_HitEffectBlend", Mathf.Lerp(minimum, maximum, tHE));

				tHE += 5 * Time.deltaTime;

				if (sr.material.GetFloat("_HitEffectBlend") >= 1)
				{
					maxHitBlendReached = true;
					tHE = 0;
				}
			}
			else
			{
				minimum = 1;
				maximum = 0;

				sr.material.SetFloat("_HitEffectBlend", Mathf.Lerp(minimum, maximum, tHE));

				tHE += 5 * Time.deltaTime;

				if (sr.material.GetFloat("_HitEffectBlend") <= 0)
				{
					maxHitBlendReached = false;
					applyingHitEffect = false;
					tHE = 0;
				}
			}
		}
	}

	
	public virtual void FixedUpdate()
	{		

		stateMachine.currentState.PhysicsUpdate();
	}

	public virtual void GetDamage(float damageHit, HealthStateTypes damageType, float knockBackForce, Vector3 bulletPosition)
	{

		if (enemyHealth - damageHit >= 0)
		{
			enemyHealth -= damageHit;

		}
		else
		{
			enemyHealth = 0;
		}

		

        if (sequenceImpactShader.IsPlaying())
        {
			Debug.Log("isPlaying");
			sequenceImpactShader.Pause();
			sequenceImpactShader.Kill();
			sequenceImpactShader = null;						
		}

		sequenceImpactShader = DOTween.Sequence();

        
        sr.material.SetFloat("_ChromAberrAmount", 0);
        sr.material.SetFloat("_FishEyeUvAmount", 0);
        sr.material.SetFloat("_HitEffectBlend", 0);
        sr.material.SetColor("_HitEffectColor", new Color(1, 0.99806f, 0.93816f, 1));
        sr.material.SetFloat("_PinchUvAmount", 0);

		if(shaker == null || !shaker.IsPlaying())
        {
			shaker = transform.DOShakePosition(0.2f, 1.5f);
			shaker = transform.DOShakeRotation(0.2f, 0.7f);
			shaker = transform.DOShakeScale(0.2f, 0.5f);
			//shaker = transform.DOJump(this.transform.position, 1.5f, 1, 0.2f);
			//shaker = transform.DOShakeRotation(0.2f, 0.7f);
		}		

		sequenceImpactShader.Join(sr.material.DOColor(new Color(1, 0.99999f, 0.34615f, 1), "_HitEffectColor", 0.2f));

		sequenceImpactShader.Join(sr.material.DOFloat(1, "_ChromAberrAmount", 0.2f));
		sequenceImpactShader.Join(sr.material.DOFloat(0.245f, "_FishEyeUvAmount", 0.2f));
		sequenceImpactShader.Join(sr.material.DOFloat(0.2f, "_HitEffectBlend", 0.2f));
		sequenceImpactShader.Join(sr.material.DOColor(new Color(1, 1, 0.34434f, 0.7f), "_HitEffectColor", 0.2f));
		sequenceImpactShader.Join(sr.material.DOFloat(0, "_PinchUvAmount", 0.2f));
		//});

		sequenceImpactShader.OnComplete(() =>
		{
			sequenceImpactShader.Join(sr.material.DOFloat(0, "_FishEyeUvAmount", 0.2f));
			sequenceImpactShader.Join(sr.material.DOFloat(0.18963f, "_HitEffectBlend", 0.2f));
			sequenceImpactShader.Join(sr.material.DOFloat(0.078f, "_PinchUvAmount", 0.2f));
		});

		sequenceImpactShader.OnComplete(() =>
		{
			sequenceImpactShader.Join(sr.material.DOFloat(0, "_ChromAberrAmount", 0.2f));
			sequenceImpactShader.Join(sr.material.DOFloat(0, "_FishEyeUvAmount", 0.2f));
			sequenceImpactShader.Join(sr.material.DOFloat(0, "_HitEffectBlend", 0.2f));
			sequenceImpactShader.Join(sr.material.DOColor(new Color(1, 1, 0.34434f, 1), "_HitEffectColor", 0.2f));
			sequenceImpactShader.Join(sr.material.DOFloat(0, "_PinchUvAmount", 0.2f)); 
		});
		






		//	sequence.Insert(0, sr.material.DOColor(new Color(1, 0.99999f, 0.34615f, 1), "_HitEffectColor", 1));//0.3f / 0.6f
		//sequence.OnComplete(() =>
		//{
		//	sequence.Insert(0, sr.material.DOFloat(1, "_ChromAberrAmount", 1));//0.01f / 0.6f
		//	sequence.Insert(0, sr.material.DOFloat(0.245f, "_FishEyeUvAmount", 1));
		//	sequence.Insert(0, sr.material.DOFloat(0.2f, "_HitEffectBlend", 1));
		//	sequence.Insert(0, sr.material.DOColor(new Color(1, 1, 0.34434f, 1), "_HitEffectColor", 1));
		//	sequence.Insert(0, sr.material.DOFloat(0, "_PinchUvAmount", 1));
		//});
		//      sequence.OnComplete(() =>
		//      {
		//          sequence.Insert(0, sr.material.DOFloat(0, "_FishEyeUvAmount", 1));//0.04f / 0.6f
		//          sequence.Insert(0, sr.material.DOFloat(0.18963f, "_HitEffectBlend", 1));
		//          sequence.Insert(0, sr.material.DOFloat(0.078f, "_PinchUvAmount", 1));
		//      });
		//      sequence.OnComplete(() =>
		//      {
		//          Debug.Log("in2");
		//          sequence.Insert(0, sr.material.DOFloat(0, "_ChromAberrAmount", 1));//0.25f / 0.6f
		//          sequence.Insert(0, sr.material.DOFloat(0, "_FishEyeUvAmount", 1));
		//          sequence.Insert(0, sr.material.DOFloat(0, "_HitEffectBlend", 1));
		//          sequence.Insert(0, sr.material.DOColor(new Color(1, 1, 0.34434f, 1), "_HitEffectColor", 1));
		//          sequence.Insert(0, sr.material.DOFloat(0, "_PinchUvAmount", 1));
		//          sequenceCompleted = true;
		//      });


		//}
		//      else
		//      {
		//	sequence.Restart();
		//}

		//sequence.Insert(0, sr.material.DO)

		if (damageType == HealthStateTypes.NORMAL)
			AudioManager.Instance.PlaySound(enemyData.hitSound, this.transform);

		if (damageType != HealthStateTypes.BURNED)
		{
			GameObject hitDamageParticles = Instantiate(enemyData.hitParticles, bulletPosition, this.transform.rotation);

			FunctionTimer.Create(() => { Destroy(hitDamageParticles.gameObject); }, 0.5f);

		}

		UpdateNewHeealthState(damageType);

	}
	void UpdateNewHeealthState(HealthStateTypes damageType)
    {
			if (myHealthState != damageType)
			{
				myHealthState = damageType;
				//timeHealthStateEntered = Time.time;
				switch (damageType)
				{
					case HealthStateTypes.BURNED:						
						applyingHitEffect = true;
						timeHealthStateExit = Time.time + 5;
						GetDamage(10);
						lastTimeDamageHealthStateApplied = Time.time;
						lastTimeTouchedFire = Time.time;	
						burnFireShader = Instantiate(burnPrefab, sr.gameObject.transform);
						burnFireShader.transform.localPosition = GetBurnValues().position;
						burnFireShader.transform.rotation = GetBurnValues().rotation;
						burnFireShader.transform.localScale = GetBurnValues().localScale;
						//burnShader.GetComponent<BurnedAuraScript>().SetValues(this.GetBurnValues());
						break;
					case HealthStateTypes.FREEZE:
						break;
					case HealthStateTypes.PARALYZED:
						break;
					default:
						break;
				}
			}
			else
			{
				switch (damageType)
				{
					case HealthStateTypes.BURNED:
					lastTimeTouchedFire = Time.time;
					timeHealthStateExit = Time.time + 5;
						break;
					case HealthStateTypes.FREEZE:
						break;
					case HealthStateTypes.PARALYZED:
						break;
					default:
						break;
				}
			}


		
	}

	protected virtual void GetDamage(float damageHit)
    {
		Debug.Log("Damaged");
		if (enemyHealth - damageHit >= 0)
		{
			enemyHealth -= damageHit;

		}
		else
		{
			enemyHealth = 0;
		}
	}

	public Transform GetFirePointTransform()
    {
		return firePoint;
    }
	public abstract  /*KeyValuePair<*/Transform/*, float>*/ GetBurnValues();
}