 public var delay = 2;
function TextScroll() {
   var myString:String = "Scrolling text";
   var charIndex:int;
   var scene:int ;
   
   for (var i:int=0; i<myString.length; i++)
   {
    //  yield WaitForSeconds(.1);
 
      GetComponent(TextMesh).text += myString[charIndex++];     
   }
}
var speed =2;
var crawling = false;
function Update ()
{
//    if (!crawling)
//        return;
    transform.Translate(Vector3.forward * Time.deltaTime * speed);
    WaitAndDestroy();
    scene = Application.loadedLevel;
if (Input.anyKeyDown)
    {
  //  Application.LoadLevel("Level1");
    Application.LoadLevel(scene + 1);
    }
}

function WaitAndDestroy(){
   yield WaitForSeconds(delay);
   Destroy (gameObject);
}