using UnityEngine;
using TMPro;

public class BotStateDisplay : MonoBehaviour
{
    [SerializeField] GameObject _canvas;
    [SerializeField] TMP_Text text;

    void Update()
    {
        _canvas.transform.LookAt(Camera.main.transform);
    }

    public void UpdateStateDisplay(IncompletoBot.State state)
    {
        text.text = state.ToString();
    }
}
