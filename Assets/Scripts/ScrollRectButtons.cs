using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScrollRectButtons : MonoBehaviour
{
    public ScrollRect scrollRect;
    [SerializeField] private float scrollSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
	{
		Scroll(1);
	}
	if(Input.GetKey(KeyCode.DownArrow))
	{
		Scroll(-1);
	}
    }

    public void Scroll(int scrollFactor)
    {
	  if(scrollRect != null)
	  {
		  if(((scrollRect.verticalNormalizedPosition<=1f) && (scrollFactor>=0)) || ((scrollRect.verticalNormalizedPosition>=0f && (scrollFactor<=0))))
			  {
				  scrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition + (scrollFactor * scrollSpeed);
			  }
	  }
    }
}
