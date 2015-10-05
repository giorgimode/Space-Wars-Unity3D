var speed =0.2f;
public var waitTime = 3.0f;
var crawling = false;
function TextScroll() {
   var myString:String = "Scrolling text";
   var charIndex:int;
 
   for (var i:int=0; i<myString.length; i++)
   {
      yield WaitForSeconds(1.0);
 
      GetComponent(TextMesh).text += myString[charIndex++];     
   }
}

function Update ()
{ 
    Wait();
//if (Input.anyKeyDown)
//    {
//       Application.LoadLevel("Level02");
//    }
}

function Wait()
{ yield WaitForSeconds(waitTime);
if (!crawling)
        return;
    transform.Translate(Vector3.up * Time.deltaTime * speed);

}