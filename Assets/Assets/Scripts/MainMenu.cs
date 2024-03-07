using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject recordsPanel, menuPanel;
    [SerializeField] TMPro.TextMeshProUGUI textRecord, textRecord2, textRecord3, textRecord4, textRecord5;



    public void PlayButton()
    {
        SceneManager.LoadScene("Nivel1");
        audioSource.Play();
    }


    public void ShowRecords()
    {
        if (menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);
            recordsPanel.SetActive(true);
        }
        List<int> records = SaveManager.LoadRecord();

        if (records.Count >= 1)
            textRecord.text = "1: " + records[0].ToString();
        else
            textRecord.text = "1: 0";

        if (records.Count >= 2)
            textRecord2.text = "2: " + records[1].ToString();
        else
            textRecord2.text = "2: 0";

        if (records.Count >= 3)
            textRecord3.text = "3: " + records[2].ToString();
        else
            textRecord3.text = "3: 0";

        if (records.Count >= 4)
            textRecord4.text = "4: " + records[3].ToString();
        else
            textRecord4.text = "4: 0";

        if (records.Count >= 5)
            textRecord5.text = "5: " + records[4].ToString();
        else
            textRecord5.text = "5: 0";
    }

    public void ShowMenu()
    {
        if (recordsPanel.activeSelf)
        {
            recordsPanel.SetActive(false);
            menuPanel.SetActive(true);
        }
    }
}
