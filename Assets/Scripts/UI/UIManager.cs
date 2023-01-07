using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject finishPanel;

    [SerializeField] private InputField inputField;

    [SerializeField] private Text nameText;
    [SerializeField] private Image healthBarImage;

    [SerializeField] private List<Image> skillImages = new List<Image>();

    [SerializeField] private List<Text> nameTexts = new List<Text>();


    private void OnEnable()
    {
        //UpdateHealthBar();
        //UpdateSkills();
    }
    private void OnDisable()
    {
        
    }


    public void OnClickStart()
    {
        startPanel.SetActive(false);
        //start event
    }

    public void OnClickNext()
    {
        finishPanel.SetActive(false);
        //next event
    }

    public void OnClickRestart()
    {
        finishPanel.SetActive(false);
        //restart event
    }

    public void OnTypeInputField()
    {
        nameText.text = inputField.text;
    }

    private void UpdateHealthBar(float _v = 0)
    {
        healthBarImage.fillAmount = _v;
    }

    private void UpdateSkills()
    {

    }

    private void OnFinish()
    {
        //update name texts
    }

}
