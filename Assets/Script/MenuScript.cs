using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI textComponent;
    public Image imageComponent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        textComponent.color = Color.black;
        imageComponent.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textComponent.color= Color.white;   
        imageComponent.enabled = false;
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
