using UnityEngine;
using System;
using System.Collections;

public class playerController : MonoBehaviour {
    public float speed = 2f;
    public float spacing = 0.1f;
    public GameObject arrowPointer;
    private Vector3 pos;
    private Vector3 Liniepos;
    private GameObject player1;
    RaycastHit hit;

	void Start () {
        player1=GameObject.Find("Player1");
        pos = transform.position;
        //arrowPointer = Instantiate(Resources.Load("ArrowPointer/arrowpointer", typeof(GameObject))) as GameObject;

	}
	
	void Update () {
        //mouse
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                try{
                    pos=this.getScreenPosition(Input.mousePosition,transform.position.y);    
                }catch(System.ArgumentException ex){}
            }
        }
        else{
            Vector3 heading = pos - transform.position;
            //playerAnimator.Play();
            if(heading.x>0){
                player1.GetComponent<CharacterControl>().Move(1.0f, 0.0f);
            }else if(heading.x<0){
                player1.GetComponent<CharacterControl>().Move(-1.0f, 0.0f);
            }else{
                //player1.GetComponent<CharacterControl>().Move(0.0f, 0.0f);
            }
            if(heading.z>0){
                player1.GetComponent<CharacterControl>().Move(0.0f, 1.0f);
            }else if(heading.z<0){
                player1.GetComponent<CharacterControl>().Move(0.0f, -1.0f);
            }else{
                //player1.GetComponent<CharacterControl>().Move(0.0f, 0.0f);
            }


            Vector3 targetDir = pos - transform.position;
            float step = speed * Time.deltaTime;


        }
        //Touch
        for (int i = 0; i < Input.touchCount; ++i) {
            Touch touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
            {
                try
                {
                    pos = this.getScreenPosition(touch.position,transform.position.y);
                    arrowPointer.transform.position=new Vector3(pos.x,arrowPointer.transform.position.y,pos.z);
                    Renderer[] renderers = arrowPointer.GetComponentsInChildren<Renderer>();
                    foreach (Renderer r in renderers)
                    {
                        r.enabled = true;
                    }

                    Vector3 heading = pos - transform.position;
                    //playerAnimator.Play();
                    if(heading.x>0){
                        player1.GetComponent<CharacterControl>().Move(1.0f, 0.0f);
                    }else if(heading.x<0){
                        player1.GetComponent<CharacterControl>().Move(-1.0f, 0.0f);
                    }else{
                        //player1.GetComponent<CharacterControl>().Move(0.0f, 0.0f);
                    }
                    if(heading.z>0){
                        player1.GetComponent<CharacterControl>().Move(0.0f, 1.0f);
                    }else if(heading.z<0){
                        player1.GetComponent<CharacterControl>().Move(0.0f, -1.0f);
                    }else{
                        player1.GetComponent<CharacterControl>().Move(0.0f, 0.0f);
                    }
                }
                catch (System.ArgumentException ex)
                {
                    //arrowPointer.GetComponent<Renderer>().enabled=false;
                    Renderer[] renderers = arrowPointer.GetComponentsInChildren<Renderer>();
                    foreach (Renderer r in renderers)
                    {
                        r.enabled = false;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Liniepos=this.getScreenPosition(touch.position,transform.position.y);
            }
            else if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled){
            }
            else{
            }
        }

	}

    Vector3 getScreenPosition(Vector3 position,float currentY){
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out hit)&&(hit.transform.gameObject.tag.Equals("Player")||hit.transform.gameObject.tag.Equals("Stage")) )
        {
            return new Vector3(hit.point.x, currentY, hit.point.z);
        }else{
            throw new System.ArgumentException("No hit or no way", "original");
             return new Vector3(0,0,0);
        }
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Renderer[] renderers = arrowPointer.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }
        // Code to execute after the delay
    }
}
