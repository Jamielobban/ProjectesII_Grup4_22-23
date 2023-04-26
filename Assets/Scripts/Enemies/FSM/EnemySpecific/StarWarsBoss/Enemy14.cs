using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy14 : Entity
{
    public E14_DeadState deadState { get; private set; }
    public E14_IdleState idleState { get; private set; }
    public E14_FiringState firingState { get; private set; }    

    [SerializeField]
    private D_FiringState firingStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_IdleState idleStateData;

    public GameObject flameWave;
    public GameObject multiSwords;

    public int mode = 1; //1
    public float waitBetweenAttacks = 2; //2
    public float lastTimeExitState = 0;

    public Color colorMode1;
    public Color colorMode2;

    public GameObject sword4waves;
    public GameObject idleSwords;
    public GameObject attack2Swords;
    public GameObject missileSwords;

    public Vector3[] posibleSpawnPoints = new Vector3[5];

    [HideInInspector]
    public GameObject idleSwordsInstance;

    public AudioClip swordSound;
    public int? swordSoundKey;
    public AudioClip energyexplosionSound;
    public int? energyexplosionSoundKey;
    public AudioClip explosionSound;
    public int? explosionSoundKey;
    public AudioClip slashSound;
    public int? slashSoundKey;
    public AudioClip appearSound;
    public int? appearSoundKey;
    public AudioClip disappearSound;
    public int? disappearSoundKey;
    public AudioClip swordAppearSound;
    public int? swordAppearSoundKey;
    public AudioClip deadSound;
    public int? deadSoundKey;

    public int? idelSwordSoundKey1;
    public int? idelSwordSoundKey2;
    public int? idelSwordSoundKey3;
    public int? idelSwordSoundKey4;

    public AudioClip backThemeSound;
    public int? backThemeKey;
    //public int? slashBurningSoundKey;

    public override void Start()
    {
        base.Start();

        firingState = new E14_FiringState(this, stateMachine, "fire", firingStateData, this);
        deadState = new E14_DeadState(this, stateMachine, "dead", deadStateData, this);
        idleState = new E14_IdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(idleState);

        backThemeKey = AudioManager.Instance.LoadSound(backThemeSound, player.transform, 0, true, false, MixerGroups.MUSIC,0.5f);

        mode = 1; //1
        waitBetweenAttacks = 2; //2
    }

    public override void Update()
    {
        base.Update();

        if (enemyHealth >= 4000)
        {
            mode = 1;
            sr.material.SetColor("_OutlineColor", colorMode1);
            waitBetweenAttacks = 2f;
        }
        else
        {
            if (mode != 2 && !firingState.doingAttack)
                mode = 2;

            sr.material.SetColor("_OutlineColor", colorMode2);
            waitBetweenAttacks = 1.5f;
        }

        if (isDead && stateMachine.currentState != deadState)
        {
            stateMachine.ChangeState(deadState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void GetDamage(float damageHit, HealthStateTypes damageType, float knockBackForce, Vector3 bulletPosition, TransformMovementType type)
    {
        base.GetDamage(damageHit, damageType, knockBackForce, bulletPosition, type);
    }

    protected override void GetDamage(float damageHit)
    {
        base.GetDamage(damageHit);
    }

    public override Transform GetBurnValues()
    {
        throw new System.NotImplementedException();
    }

    public void SearchFunction(string funcName)
    {
        if (this.gameObject == null || stateMachine.currentState == null || stateMachine.currentState == deadState)
            return;
        var exampleType = stateMachine.currentState.GetType();
        var exampleMethod = exampleType.GetMethod(funcName);
        try { exampleMethod.Invoke(stateMachine.currentState, null); } catch { /*Debug.Log(funcName); Debug.Log(stateMachine.currentState);*/ };
        if (this.gameObject == null)
        {
            CancelInvoke();
        }
    }

    public void CreateIdleSwordsSounds()
    {
        idelSwordSoundKey1 = AudioManager.Instance.LoadSound(swordSound, transform, 0, true, true, MixerGroups.ENEMIES,0.3f);
        if (idelSwordSoundKey1.HasValue)
            AudioManager.Instance.GetAudioFromDictionaryIfPossible(idelSwordSoundKey1.Value).transform.localPosition = new Vector3(-1.72f, -2.07f, 0);
        idelSwordSoundKey2 = AudioManager.Instance.LoadSound(swordSound, transform, 0f, true, true, MixerGroups.ENEMIES, 0.3f);
        if (idelSwordSoundKey2.HasValue)
            AudioManager.Instance.GetAudioFromDictionaryIfPossible(idelSwordSoundKey2.Value).transform.localPosition = new Vector3(1.72f, -2.07f, 0);
        idelSwordSoundKey3 = AudioManager.Instance.LoadSound(swordSound, transform, 0f, true, true, MixerGroups.ENEMIES, 0.3f);
        if (idelSwordSoundKey3.HasValue)
            AudioManager.Instance.GetAudioFromDictionaryIfPossible(idelSwordSoundKey3.Value).transform.localPosition = new Vector3(1.72f, -3.21f, 0);
        idelSwordSoundKey4 = AudioManager.Instance.LoadSound(swordSound, transform, 0f, true, true, MixerGroups.ENEMIES, 0.3f);
        if (idelSwordSoundKey4.HasValue)
            AudioManager.Instance.GetAudioFromDictionaryIfPossible(idelSwordSoundKey4.Value).transform.localPosition = new Vector3(-1.72f, -3.21f, 0);
    }

    public void RemoveIdleSwords()
    {

        if (idelSwordSoundKey1.HasValue)
            AudioManager.Instance.RemoveAudio(idelSwordSoundKey1.Value);
        
        if (idelSwordSoundKey2.HasValue)
            AudioManager.Instance.RemoveAudio(idelSwordSoundKey2.Value);
        
        if (idelSwordSoundKey3.HasValue)
            AudioManager.Instance.RemoveAudio(idelSwordSoundKey3.Value);
        
        if (idelSwordSoundKey4.HasValue)
            AudioManager.Instance.RemoveAudio(idelSwordSoundKey4.Value);
    }
}
