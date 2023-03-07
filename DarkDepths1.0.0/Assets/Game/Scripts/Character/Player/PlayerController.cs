using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class PlayerControllerKeys
{
    public const string LayerBomb = "Bomb";
    public const string tagDoor = "Door";
    public const string tagPowerUps = "PowerUps";
}

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : CharacterController
{

    [Header("Movement")]
    [SerializeField][Min(0)] float speed = 10;
    Rigidbody2D rb;
    PlayerInput plInput;

    [Header("Sensor")]
    [SerializeField] LayerMask sensorLayer;
    bool insideObjects;

    [Header("Bomb")]
    [SerializeField] GameObject[] bombPrefab;
    [SerializeField] int typeBomb = 0;
    [SerializeField][Min(0)] int bombsAllowed = 1;
    [SerializeField][Min(0)] int fireLength = 1;
    List<GameObject> listBomb = new List<GameObject>();

    [Header("SFX")]
    [SerializeField] AudioClip sfxLose;
    [SerializeField] AudioClip sfxVictory;

    public Hud hud;
    IInteractable interactObject;
    bool interaction;

    public List<GameObject> ListBomb { get => listBomb; set => listBomb = value; }
    public int BombsAllowed { get => bombsAllowed; set => bombsAllowed = value; }
    public int FireLength { get => fireLength; set => fireLength = value; }
    public float Speed { get => speed; set => speed = value; }
    public int TypeBomb { get => typeBomb; set => typeBomb = value; }
    public PlayerInput PlInput { get => plInput; set => plInput = value; }
    public AudioClip SfxVictory { get => sfxVictory; set => sfxVictory = value; }

    protected override void Awake()
    {
        base.Awake();
        interaction = false;
        rb = GetComponent<Rigidbody2D>();
        hud = GetComponent<Hud>();
        plInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        GameManager.Instance.SetPlayerSettings(this);
    }

    private void FixedUpdate()
    {
        if (health > 0)
        {
            Move();
            HandleSensor();
            Controls();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Controls()
    {
        if(plInput.IsAttackButtonDown())
        {
            if(listBomb.Count < bombsAllowed && !insideObjects && health > 0)
            {
                PlaceBomb();
            }
        }

        if(plInput.IsActionButtonDown())
        {
            if (ListBomb.Count > 0)
            {
                if (ListBomb[0].GetComponent<MineBomb>() && health > 0)
                {
                    var test = ListBomb[0].GetComponent<MineBomb>();
                    test.activeMine = true;
                }
            }
        }

        if(plInput.IsInteractionButtonDown())
        {
            if (interactObject != null && health > 0 && !interaction)
            {
                interaction = true;
                interactObject.Interaction(this);
            }
        }
    }

    private void Move()
    {
        Vector2 moveinput = plInput.GetMovementInput();
        rb.velocity = moveinput * speed * Time.deltaTime;
    }

    public void DeathScreenEvent()
    {
        hud.HudGameplay.SetActive(false);
        hud.DeathScreen.SetActive(true);
        AudioController.Instance.PlaySound(sfxLose);
    }
    private void PlaceBomb()
    {
        Vector2 position = transform.position - new Vector3(0,0.2f,0);
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab[typeBomb], position, Quaternion.identity);
        var bombComponent = bomb.GetComponent<Bomb>();
        bombComponent.Controller = this;
        ListBomb.Add(bomb);
    }

    private void HandleSensor()
    {
        insideObjects = Physics2D.OverlapBox(gameObject.transform.position, new Vector2(0.6f, 0.6f), 0, sensorLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<IInteractable>() != null)
        {
            hud.InteractButtom.SetActive(true);
            hud.AttackButtom.SetActive(false);

            interactObject = collision.GetComponent<IInteractable>();
        }

        if (collision.gameObject.GetComponent<IPowerUp>() != null)
        {
            var power = collision.gameObject.GetComponent<IPowerUp>();
            var sound = collision.gameObject.GetComponent<PowerUps>();

            sound.PlaySound();
            power.SetUpgrade(this);
            
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(PlayerControllerKeys.LayerBomb))
        {
            collision.isTrigger = false;
        }

        if (collision.gameObject.GetComponent<IInteractable>() != null)
        {
            hud.InteractButtom.SetActive(false);
            hud.AttackButtom.SetActive(true);
        }
    }
}
