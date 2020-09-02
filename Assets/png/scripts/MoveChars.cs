using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveChars : MonoBehaviour
{
  
    public Rigidbody2D rb;
    public Animator myanim;
    private Collider2D coli; 
    [SerializeField] private AudioSource coinCollect;
    private bool faceingRigt;
    public float jumpForce;
    public float Horizontal;
    [SerializeField] private LayerMask ground;
    [SerializeField] private TextMeshProUGUI CoinsText;
     private int coins = 0;
    private enum State { idle , running , jumping , falling , hurt}
    private State state = State.idle;
    private int move = 0;
	
    
   public float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
             faceingRigt = true;
             rb = GetComponent<Rigidbody2D>();
             myanim = GetComponent<Animator>();
             coli = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
      
        AnimationState(); 
          
        // Animation();
        myanim.SetInteger("state", (int)state);
       
    }


    

 

    private void handleMove(float Horizontal){
        rb.velocity = new Vector2(Horizontal * movementSpeed , rb.velocity.y);
        // myanim.SetFloat("speed" , Mathf.Abs(Horizontal));
        // Debug.Log(Mathf.Abs(Horizontal));
        // if(Mathf.Abs(Horizontal) == 1){
        //     state = State.running;
        // }
       

    }


  private void OnTriggerEnter2D(Collider2D coin)
    {
      if(coin.tag == "Coins")
      {
          coinCollect.Play();
          Destroy(coin.gameObject); 
          collectCoin();
      }
    }


    private void collectCoin(){
         coins += 1; 
	     CoinsText.text = coins.ToString();
    }

    private void Flip(float Horizontal){ 

        if (Horizontal > 0 && !faceingRigt || Horizontal < 0 && faceingRigt)
        {
            faceingRigt = !faceingRigt;
            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        //     if(Horizontal > 0 ){
        //       transform.localScale = new Vector2(0.3f , 0.3f);
        //     }else if(Horizontal < 0){
        //   transform.localScale = new Vector2(-0.3f , 0.3f);
        //     }
            
            
        }



    }

    public void Jump(){
        if(Input.GetButtonDown("Jump") && isGround() ){
            
            
            // rb.AddForce(new Vector2(0 , jumpForce));
             rb.velocity = new Vector2(rb.velocity.x , jumpForce);
            state = State.jumping;
           
        }
        
    }


        public void JumpTouch(){

        if(isGround()){
         
             rb.velocity = new Vector2(rb.velocity.x , jumpForce);
            state = State.jumping;
        }
        
    }

       public void OnKeyUp(){
       this.move = 0;
       }

        public void OnKeyDownLeft(){
           
           this.move = -1;
           Flip(-1f);
       }
        public void OnKeyDownRight(){
            this.move = 1;
             Flip(1f);

       }


    private void MoveTouchButtons(){
        if(this.move == -1){
          
         rb.velocity = new Vector2( -1 * movementSpeed , rb.velocity.y);
        }else if( this.move == 1){
             rb.velocity = new Vector2(1 * movementSpeed , rb.velocity.y); 
        }
    }
private void MovTouch(){
    // if()
}
    public bool isGround(){
        if(coli.IsTouchingLayers(ground)){
           
            return true;

        }else{

            return false;
        }
    }


    void FixedUpdate()
    {
         Horizontal = Input.GetAxisRaw("Horizontal");
           handleMove(Horizontal);
           Flip(Horizontal);
           Jump(); 
            MoveTouchButtons();
        //    MovTouch();
          
    }


private void Animation(){
    	if(state  == State.jumping){
	            if(rb.velocity.y < .1f){
				 state = State.falling;
				
				
			}

         }else if(Mathf.Abs(rb.velocity.x) > 2f){
			//moving  //Mathf.Abs(rb.velocity.x) > 2f
		              	state = State.running;
			
			
			} else if(state == State.falling){
			if(isGround()){
				state = State.idle;
            }
				}
}

    
    private void AnimationState(){
		
	if(state  == State.jumping){
		//Debug.Log("yes");
		
			if(rb.velocity.y < .1f){
				state = State.falling;
				
				
			}
			
			
		} else if(state == State.falling){
			if(coli.IsTouchingLayers(ground)){
				state = State.idle;
				
				}
			
			
			}else if(state == State.hurt){
				if(Mathf.Abs(rb.velocity.x) < .1f)
				{
					state = State.idle;
				}
				
				
				}else if(Mathf.Abs(rb.velocity.x) > 2f){
			//moving  //Mathf.Abs(rb.velocity.x) > 2f
			state = State.running;
			
			
			}else{
				
			state = State.idle;
				
			}
		
		
		
		
		}


}














