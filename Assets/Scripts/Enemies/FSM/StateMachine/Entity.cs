using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Entity : MonoBehaviour
{
	[HideInInspector]
	public FiniteStateMachine stateMachine;

	[HideInInspector]
	public Transform player;
	[HideInInspector]
	public Vector3 vectorToPlayer;

	public Rigidbody2D rb { get; private set; }
	public Animator anim { get; private set; }
	public D_Entity enemyData;

	protected bool isDead;
	protected HealthStateTypes myHealthState;
	//protected float timeHealthStateEntered;
	protected float timeHealthStateExit;	
	protected float lastTimeDamageHealthStateApplied;

    private void Awake()
    {
		enemyData.firePoint = GetComponentsInChildren<Transform>().Where(t => t.tag == "FirePoint").ToArray()[0];
		myHealthState = HealthStateTypes.NORMAL;
		//timeHealthStateEntered = Time.time;
		timeHealthStateExit = 0;
		lastTimeDamageHealthStateApplied = 0;
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
		isDead = enemyData.enemyHealth.RuntimeValue <= 0;
		vectorToPlayer = player.position - transform.position;

		if (Time.time >= timeHealthStateExit)
		{
			myHealthState = HealthStateTypes.NORMAL;
		}

		if (myHealthState != HealthStateTypes.NORMAL)
        {
			switch (myHealthState)
			{
				case HealthStateTypes.BURNED:
					if(Time.time - lastTimeDamageHealthStateApplied >= 2)
                    {
						GetDamage(10);
						lastTimeDamageHealthStateApplied = Time.time;
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
		
        stateMachine.currentState.LogicUpdate();
	}
	public virtual void FixedUpdate()
	{
		stateMachine.currentState.PhysicsUpdate();
	}

	public virtual void GetDamage(float damageHit, HealthStateTypes damageType, float knockBackForce, Vector3 bulletPosition)
    {
		
		if(enemyData.enemyHealth.RuntimeValue - damageHit >= 0)
        {
			enemyData.enemyHealth.RuntimeValue -= damageHit;

		}
        else
        {
			enemyData.enemyHealth.RuntimeValue = 0;
		}

		if(damageType == HealthStateTypes.NORMAL)
			AudioManager.Instance.PlaySound(enemyData.hitSound, this.transform);
		
		GameObject hitDamageParticles = Instantiate(enemyData.hitParticles, bulletPosition, this.transform.rotation);
		FunctionTimer.Create(() => { Destroy(hitDamageParticles.gameObject); },0.5f);

		if(myHealthState != damageType)
        {
			myHealthState = damageType;
			//timeHealthStateEntered = Time.time;
            switch (damageType)
            {
                case HealthStateTypes.BURNED:
					timeHealthStateExit = Time.time + 5;
					GetDamage(10);
					lastTimeDamageHealthStateApplied = Time.time;
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
		if (enemyData.enemyHealth.RuntimeValue - damageHit >= 0)
		{
			enemyData.enemyHealth.RuntimeValue -= damageHit;

		}
		else
		{
			enemyData.enemyHealth.RuntimeValue = 0;
		}
	}
}