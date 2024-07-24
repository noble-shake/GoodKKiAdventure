using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogoEffect : MonoBehaviour
{
    [SerializeField] Image Background;
    [SerializeField] GameObject Title;
    [SerializeField] TMP_Text EnterText;
    [SerializeField] float textEffect = 0.3f;
    [SerializeField] float textCurEffect;
    [SerializeField] bool textBool;
    [SerializeField] float alphaValue;

    IEnumerator Effect()
    { 
        Background.gameObject.SetActive(true);
        Title.gameObject.SetActive(true);

        Image img = Title.GetComponent<Image>();
        float alphaValue = 0f;
        Color alphaChange = new Color(0f, 0f, 0f, alphaValue);
        while (alphaValue < 1f)
        {
            alphaValue += (Time.deltaTime / 2f);
            if (alphaValue > 1f)
            {
                alphaValue = 1f;
            }
            alphaChange = new Color(alphaChange.r, alphaChange.g, alphaChange.b, alphaValue);
            img.color = alphaChange;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        EnterText.gameObject.SetActive(true);
        yield return new WaitUntil(() => Input.anyKeyDown);

        MainMenuManager.instance.OnMainMenu();
        EnterText.gameObject.SetActive(false);
        Background.gameObject.SetActive(false);

        while (alphaValue > 0f)
        {
            alphaValue -= (Time.deltaTime / 2f);
            if (alphaValue < 0f)
            {
                alphaValue = 0f;
            }
            alphaChange = new Color(alphaChange.r, alphaChange.g, alphaChange.b, alphaValue);
            img.color = alphaChange;
            yield return null;
        }

        Destroy(gameObject);
    }



    // Start is called before the first frame update
    void Start()
    {
         StartCoroutine(Effect());
    }

    // Update is called once per frame
    void Update()
    {
        if (EnterText.gameObject.activeSelf)
        {
            textCurEffect -= Time.deltaTime;

            if (textCurEffect < 0)
            {
                textCurEffect = 0;
                textCurEffect = textEffect;
                textBool = !textBool;
            }


            if (textBool)
            {
                alphaValue = textCurEffect / 0.3f;
                EnterText.color = new Color(255f, 255f, 255f, alphaValue);


            }
            else
            {
                alphaValue =  1 - (textCurEffect / 0.3f);
                EnterText.color = new Color(255f, 255f, 255f, alphaValue);
            }
        }
    }
}
