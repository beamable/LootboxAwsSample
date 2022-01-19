using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Beamable.Server.Clients;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootboxController : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    public Button ClaimButton;
    private double _startTimeLeft;
    private double _startTime;

    // Start is called before the first frame update
    async void Start()
    {
        var client = new LootBoxServiceClient();
        _startTimeLeft = await client.GetTimeLeft();
        _startTime = Time.realtimeSinceStartupAsDouble;

        ClaimButton.onClick.AddListener(Claim);
    }

    private async void Claim()
    {
        var client = new LootBoxServiceClient();
        _startTimeLeft = 120;
        await client.Claim();
        _startTimeLeft = await client.GetTimeLeft();
        _startTime = Time.realtimeSinceStartupAsDouble;
    }

    // Update is called once per frame
    void Update()
    {
        var now = Time.realtimeSinceStartupAsDouble;
        var delta = now - _startTime;

        var timeLeft = _startTimeLeft - (delta);
        timeLeft = timeLeft < 0 ? 0 : timeLeft;
        var isClaimable = timeLeft <= .001;

        TimerText.text = timeLeft.ToString("00");

        ClaimButton.interactable = isClaimable;
    }
}
