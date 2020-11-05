using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameText : MonoBehaviour
{
    public GameObject gameTextObject;

    public float waitingTime;

    public IEnumerator startText(float waitingTime)
    {
        gameTextObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Three");
        gameTextObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        yield return new WaitForSecondsRealtime(waitingTime);

        gameTextObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Two");

        yield return new WaitForSecondsRealtime(waitingTime);

        gameTextObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("One");

        yield return new WaitForSecondsRealtime(waitingTime);

        gameTextObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Go");

        yield return new WaitForSecondsRealtime(waitingTime);

        gameTextObject.GetComponent<Image>().sprite = null;
        gameTextObject.GetComponent<Image>().color = new Color (0, 0, 0, 0);
    }

    /*
    // removed from final
    public IEnumerator endText(float duration)
    {
        gameTextObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Times_Up");
        gameTextObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        yield return new WaitForSecondsRealtime(duration);

        gameTextObject.GetComponent<Image>().sprite = null;
        gameTextObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    } */
}
