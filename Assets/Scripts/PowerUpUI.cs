using TMPro;
using UnityEngine;

public class PowerUpUI : MonoBehaviour
{
    public TextMeshProUGUI doubleJumpText;
    public TextMeshProUGUI speedBoostText;

    private void OnEnable()
    {
        Player.OnPowerUpCollected += UpdateUI;
    }

    private void OnDisable()
    {
        Player.OnPowerUpCollected -= UpdateUI;
    }

    private void UpdateUI(PowerUp.PowerType type, int count)
    {
        if (type == PowerUp.PowerType.DoubleJump)
            doubleJumpText.text = count.ToString();
        else if (type == PowerUp.PowerType.SpeedBoost)
            speedBoostText.text = count.ToString();
    }
}
