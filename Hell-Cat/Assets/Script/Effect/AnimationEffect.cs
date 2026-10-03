using UnityEngine;
using System.Collections;


public class AnimationEffect: MonoBehaviour
{
    public Animator DoorAnim;
    public float DoorCloseTimer = 2f;

    public void DoorOpen()
    {
        DoorAnim.SetTrigger("Open");
    }

    public void DoorClose()
    {
        StartCoroutine(DoorCloseCoroutine());
    }

    IEnumerator DoorCloseCoroutine()
    {
        yield return new WaitForSeconds(DoorCloseTimer);
        DoorAnim.SetTrigger("Close");

    }


}
