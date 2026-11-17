using UnityEngine;
using TMPro;

public class BotStateDisplay : MonoBehaviour
{
    [SerializeField] GameObject _canvas;
    [SerializeField] TMP_Text text;

    Quaternion targetRot = Quaternion.Euler(90, 0, 0);

    private void LateUpdate()
    {
        _canvas.transform.rotation = targetRot;
    }

    public void UpdateStateDisplay(IncompletoBot.State state)
    {
        text.text = state.ToString();
    }
}
