using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class HudKeys
{
    public const string IDlocale = "LocaleKey";
}

public class Hud : MenuController
{
    [Header("Life")]
    [SerializeField] private Slider lifeBar;
    [SerializeField] private Text lifeText;

    [Header("Infomations")]
    [SerializeField] private Text bombCount;
    [SerializeField] private Image bombImage;
    [SerializeField] private Sprite[] bombSprites;
    [SerializeField] private Text fireCount;
    
    [Header("Level")]
    [SerializeField] private Text levelCount;

    [Header("Treasure")]
    [SerializeField] private Image treasure;

    [Header("Controls")]
    [SerializeField] private GameObject attackButtom;
    [SerializeField] private GameObject interactButtom;
    [SerializeField] private GameObject actionButtom;

    [Header("Screens")]
    [SerializeField] private GameObject hudGameplay;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject collectTreasureScreen;

    private PlayerController player;

    public GameObject AttackButtom { get => attackButtom; set => attackButtom = value; }
    public GameObject InteractButtom { get => interactButtom; set => interactButtom = value; }
    public GameObject HudGameplay { get => hudGameplay; set => hudGameplay = value; }
    public GameObject DeathScreen { get => deathScreen; set => deathScreen = value; }

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void Start()
    {
        levelCount.text = GameManager.Instance.Level + " - " + GameManager.Instance.World;
        hudGameplay.SetActive(true);
        deathScreen.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (player.TypeBomb == 3)
            actionButtom.SetActive(true);
        else
            actionButtom.SetActive(false);

        LifeHud();
        BombHud();
        FireHud();
        BombTypeHud(player.TypeBomb);

    }

    private void LifeHud()
    {
        lifeBar.value = player.Health;
        lifeText.text = player.Health.ToString();
    }
    private void BombHud()
    {
        bombCount.text = ":" + player.BombsAllowed.ToString();
    }
    private void BombTypeHud(int type)
    {
        bombImage.sprite = bombSprites[type];
    }
    private void FireHud()
    {
        fireCount.text = ":" + player.FireLength.ToString();
    }

    public void CollectTreasure(Sprite treasureCollected)
    {
        treasure.sprite = treasureCollected;

        HudGameplay.SetActive(false);
        collectTreasureScreen.SetActive(true);
    }
}
