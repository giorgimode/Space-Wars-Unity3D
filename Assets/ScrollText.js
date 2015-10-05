var speed =0.2f;
public var waitTime = 3.0f;
var crawling = false;
var scene:int ;
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
    scene = Application.loadedLevel;
    if (Input.anyKeyDown)
    {
        //  Application.LoadLevel("Level1");
        Application.LoadLevel(scene + 1);
    }
}

function Wait()
{ yield WaitForSeconds(waitTime);
if (!crawling)
        return;
    transform.Translate(Vector3.up * Time.deltaTime * speed);

}