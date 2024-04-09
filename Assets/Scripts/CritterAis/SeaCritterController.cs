using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SeaCritterController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private GameObject PopPrefab;
    [SerializeField] private GameObject ShieldPrefab;
    [SerializeField] private Transform FiringPoint;
    [SerializeField] private Image CooldownDisplay;

    [Header("Attributes")]
    [SerializeField] private float Fcooldown;
    [SerializeField] private float RSpeed;
    [SerializeField] private float Speed;
    [SerializeField] private float BubbleSpeed;
    [SerializeField] private float Boost;
    [SerializeField] private Color SteathColor;
    [SerializeField] private bool LockedRotation;

    private bool Bboost;
    private SpriteRenderer sr;
    private PlayerController PC;
    private GameSystems Us;
    private ParticleSystem Pas;
    private Color Scolor;
    private bool Shot;
    private bool Spraying;
    private float Sspeed;
    private float STimer;

    public Vector3 Direction;
    public Rigidbody2D rb;
    public int SpecailIndicator = 0;
    public bool Power;
    public bool SPower;
    public bool KnockedBack;

    

    private void Awake()
    {
        PC = new PlayerController();
        PC.Enable();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        Us = FindObjectOfType<GameSystems>();
        Pas = GetComponent<ParticleSystem>();
        CooldownDisplay.fillAmount = 0;
        SpecailIndicator = 0;
        Scolor = sr.color;
        Sspeed = Speed;
    }

    private void OnEnable()
    {
        GameSystems.ActivateBoost += ABoost;
        GameSystems.DeActivateBoost += CBoost;
        PC.Player.Shoot.performed += BlowBubbles;
        PC.Player.Spray.performed += SprayBubbles;
        PC.Player.Lock.performed += LockSpin;
    }
    private void OnDisable()
    {
        GameSystems.ActivateBoost -= ABoost;
        GameSystems.DeActivateBoost -= CBoost;
        PC.Player.Shoot.performed -= BlowBubbles;
        PC.Player.Spray.performed -= SprayBubbles;
        PC.Player.Lock.performed -= LockSpin;
    }

    private void Update()
    {
        if (Us.BossState)
        {
            transform.rotation = Quaternion.identity;
        }
        if (!Spraying)
        {
            CooldownDisplay.fillAmount -= 1 / STimer * Time.deltaTime;
            if(CooldownDisplay.fillAmount <= 0)
            {
                CooldownDisplay.fillAmount = 0;
            }
        }

        Direction = new Vector3(PC.Player.Movement.ReadValue<Vector2>().x, PC.Player.Movement.ReadValue<Vector2>().y, 0).normalized;

        if(Us.SizeNumb == 0)
        {
            transform.localScale = Vector3.one;
        }
        else if(Us.SizeNumb == 1)
        {
            transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
        }
        else if(Us.SizeNumb == 2)
        {
            transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
        else if(Us.SizeNumb == 3)
        {
            transform.localScale = new Vector3(1.45f, 1.45f, 1.45f);
        }
        else if (Us.SizeNumb == 4)
        {
            transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
        }
        else if (Us.SizeNumb == 5)
        {
            transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
        }
        else if (Us.SizeNumb == 6)
        {
            transform.localScale = new Vector3(1.9f, 1.9f, 1.9f);
        }
        else if (Us.SizeNumb == 7)
        {
            transform.localScale = new Vector3(2.05f, 2.05f, 2.05f);
        }
        else if (Us.SizeNumb == 8)
        {
            transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
        }
        else if (Us.SizeNumb == 9)
        {
            transform.localScale = new Vector3(2.35f, 2.35f, 2.35f);
        }
        else if (Us.SizeNumb == 10)
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }
    }

    private void FixedUpdate()
    {
        if (Direction.x == 0 && Direction.y == 0 && !KnockedBack)
        {
            rb.drag += 0.5f;
        }
        else
        {
            rb.drag = 1f;
        }

        if (!LockedRotation && !Us.BossState)
        {
            if (Direction != Vector3.zero)
            {
                quaternion toRotation = quaternion.LookRotation(Vector3.forward, Direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RSpeed * Time.deltaTime);
            }
        }
        else if(Us.BossState)
        {
            transform.rotation = Quaternion.identity;
        }

        if (Bboost)
        {
            rb.AddForce(Direction * Speed * Boost);

            if (rb.velocity.x > 10.5f)
            {
                rb.velocity = new Vector2(10.5f, rb.velocity.y);
            }
            if (rb.velocity.x < -10.5f)
            {
                rb.velocity = new Vector2(-10.5f, rb.velocity.y);
            }

            if (rb.velocity.y > 10.5f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10.5f);
            }
            if (rb.velocity.y < -10.5f)
            {
                rb.velocity = new Vector2(rb.velocity.x, -10.5f);
            }
        }
        else
        {
            rb.AddForce(Direction * Speed);

            if (rb.velocity.x > 7f)
            {
                rb.velocity = new Vector2(7, rb.velocity.y);
            }
            if (rb.velocity.x < -7f)
            {
                rb.velocity = new Vector2(-7, rb.velocity.y);
            }

            if (rb.velocity.y > 7f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 7);
            }
            if (rb.velocity.y < -7f)
            {
                rb.velocity = new Vector2(rb.velocity.x, -7);
            }
        }
    }

    private void LockSpin(InputAction.CallbackContext Lock)
    {
        if(!LockedRotation)
        {
            LockedRotation = true;
        }
        else
        {
            LockedRotation = false;
        }
    }

    private void BlowBubbles(InputAction.CallbackContext Shoot)
    {
        if (!Shot)
        {
            Shot = true;
            if (Us.SizeNumb > 0)
            {
                Pas.Play();
                Us.DecreaseSize();

                Projectile bs;
                bs = Instantiate(ProjectilePrefab, FiringPoint).GetComponent<Projectile>();
                bs.transform.localPosition = Vector3.zero;
                bs.transform.parent = null;
                bs.Activate(BubbleSpeed, transform.up);
            }
            StartCoroutine(FireCoolDown());
        }
    }

    private void SprayBubbles(InputAction.CallbackContext S)
    {
        if (CooldownDisplay.fillAmount > 0)
        {
            return;
        }
        if(Spraying)
        {
            return;
        }
        if(SpecailIndicator == 0)
        {
            return;
        }
        Spraying = true;

        if (CooldownDisplay.gameObject.active == false)
        {
            CooldownDisplay.gameObject.SetActive(true);
        }

        if (SpecailIndicator == 1)
        {
            STimer = 0.5f;
            StartCoroutine(Spray());
        }
        else if (SpecailIndicator == 2)
        {
            STimer = 5f;
            StartCoroutine(ActivateShield());
        }
        else if (SpecailIndicator == 3)
        {
            STimer = 8f;
            StartCoroutine(Steath());
        }

        CooldownDisplay.fillAmount = 1;
    }

    public void ResetPower()
    {
        StopAllCoroutines();
        Shot = false;
    }

    private void ABoost()
    {
        Bboost = true;
    }
    private void CBoost()
    {
        Bboost = false;
    }
    public void SetSpecial(int I)
    {
        SpecailIndicator = I;
    }
    public void ResetSpeed()
    {
        Speed = (Speed / 2);
        rb.velocity = (rb.velocity/2f);
        Invoke("RS", 3f);
    }
    private void RS()
    {
        ResetColor();
        Speed = Sspeed;
    }
    public void ResetColor()
    {
        Invoke(nameof(RC),0.1f);
    }
    private void RC()
    {
        if (!Power)
        {
            sr.color = Scolor;
        }
    }
    private IEnumerator FireCoolDown()
    {
        yield return new WaitForSeconds(Fcooldown);
        Shot = false;
    }

    private IEnumerator Spray()
    {
        int x = Mathf.RoundToInt(Us.SizeNumb);
        if (x > 0)
        {
            Pas.Play();
        }
        for (int i = 0; i < x; i++)
        {
            if (Us.SizeNumb > 0)
            {
                Us.DecreaseSize();
            }
        }
        x = x * 5;
        for (int i = 0; i < x; i++)
        {
            if (x > 0)
            {
                GameObject bs;
                bs = Instantiate(ProjectilePrefab, new Vector3(FiringPoint.position.x + UnityEngine.Random.Range(-1f, 1f), FiringPoint.position.y + UnityEngine.Random.Range(-1f, 1f), FiringPoint.position.z), Quaternion.identity);
                Instantiate(PopPrefab, bs.transform);
                Projectile Ps = bs.GetComponent<Projectile>();
                bs.transform.parent = null;
                Ps.Activate(BubbleSpeed, Vector3.zero);
                yield return new WaitForSeconds(0.1f);
            }
        }
        Spraying = false;
    }

    private IEnumerator ActivateShield()
    {
        int x = Mathf.RoundToInt(Us.SizeNumb);

        if (x > 0)
        {
            Pas.Play();

            for (int i = 0; i < x; i++)
            {
                if (Us.SizeNumb > 0)
                {
                    Us.DecreaseSize();
                }
            }

            GameObject S = Instantiate(ShieldPrefab, transform);
            Power = true;
            yield return new WaitForSeconds(x);
            Destroy(S);
            Power = false;

            Pas.Play();
        }

        Spraying = false;
        yield return null;
    }

    private IEnumerator Steath()
    {
        int x = Mathf.RoundToInt(Us.SizeNumb);

        if (x > 0)
        {
            Pas.Play();

            for (int i = 0; i < x; i++)
            {
                if (Us.SizeNumb > 0)
                {
                    Us.DecreaseSize();
                }
            }

            x = (x * 2);

            sr.color = SteathColor;
            Power = true;
            SPower = true;
            yield return new WaitForSeconds(x);
            Power = false;
            SPower = false;
            sr.color = Scolor;

            Pas.Play();
        }

        Spraying = false;
        yield return null;
    }
}
