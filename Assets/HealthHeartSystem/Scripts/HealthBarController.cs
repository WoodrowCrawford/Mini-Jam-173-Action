/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public PlayerBehavior playerBehavior;
    private GameObject[] heartContainers;
    
    private Image[] heartFills;

    public Transform heartsParent;
    public GameObject heartContainerPrefab;

    private void Start()
    {
        playerBehavior = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>();

        // Should I use lists? Maybe :)
        heartContainers = new GameObject[(int)playerBehavior.maxTotalHealth];
        heartFills = new Image[(int)playerBehavior.maxTotalHealth];

        PlayerBehavior.onHealthChangedCallback += UpdateHeartsHUD;
        InstantiateHeartContainers();
        UpdateHeartsHUD();
    }

    public void UpdateHeartsHUD()
    {
        SetHeartContainers();
        SetFilledHearts();
    }

    void SetHeartContainers()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < playerBehavior.maxHealth)
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }

    void SetFilledHearts()
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < playerBehavior.health)
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }

        if (playerBehavior.health % 1 != 0)
        {
            int lastPos = Mathf.FloorToInt(playerBehavior.health);
            heartFills[lastPos].fillAmount = playerBehavior.health % 1;
        }
    }

    void InstantiateHeartContainers()
    {
        for (int i = 0; i < playerBehavior.maxTotalHealth; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
        }
    }
}
