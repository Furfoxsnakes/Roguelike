using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnnouncementSystem : MonoBehaviour
{
    [SerializeField] private static TextMeshProUGUI _textMesh;

    private void Start()
    {
        _textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    public static void ShowMessage(string message, int delay = 0)
    {
        _textMesh.text = message;
        _textMesh.enabled = true;
    }

    public static void Hide()
    {
        _textMesh.enabled = false;
    }

    private IEnumerator DisplayTimedMessage(string message, int delay)
    {
        yield return new WaitForSeconds(delay);
        Hide();
    }
}
