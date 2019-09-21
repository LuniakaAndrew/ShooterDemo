using System.Collections;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

    // Store the PlayerPref Key to avoid typos
    private const string _playerNamePrefKey = "PlayerName";
    private Coroutine _emptyInput;

    private void Start()
    {
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(_playerNamePrefKey))
            {
                _inputField.text = PlayerPrefs.GetString(_playerNamePrefKey);
                Launcher.SetPlayerName(_inputField.text);
            }
        }
    }

    public void LaunchGame(string nickname)
    {
        if (string.IsNullOrEmpty(nickname))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }

        PlayerPrefs.SetString(_playerNamePrefKey, nickname);
        Launcher.SetPlayerName(nickname);
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        if (_inputField != null && string.IsNullOrEmpty(_inputField.text))
        {
            if (_emptyInput != null)
                StopCoroutine(_emptyInput);

            _emptyInput = StartCoroutine(EmptyInput());
            return;
        }

        LaunchGame(_inputField.text);
    }

    private IEnumerator EmptyInput()
    {
        WaitForSeconds seconds = new WaitForSeconds(0.01f);
        for (float i = 0; i < 1; i += 0.1f)
        {
            yield return seconds;
            _inputField.image.color = new Color(1f, i, i);
        }
    }

    public void ExitButton()
    {
        CloseApplication();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseApplication();
        }
    }
}