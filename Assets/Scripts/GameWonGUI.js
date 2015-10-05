@script ExecuteInEditMode()
var background : GUIStyle;
var gameOverText : GUIStyle;
var gameOverShadow : GUIStyle;
var gameOverScale = 1.5;
var gameOverShadowScale = 1.5;
function OnGUI()
{
GUI.Label ( Rect( (Screen.width - (Screen.height * 2)) * 0.75, 0, Screen.height * 2,
Screen.height), "", background);
GUI.matrix = Matrix4x4.TRS(Vector3(0, 0, 0), Quaternion.identity, Vector3.one *
gameOverShadowScale);
GUI.Label ( Rect( (Screen.width / (2 * gameOverShadowScale)) - 350,
(Screen.height / (2 * gameOverShadowScale)) - 40, 300, 100), "         YOU WON! \n Force Is Strong With You...",
gameOverShadow);

GUI.matrix = Matrix4x4.TRS(Vector3(0, 0, 0), Quaternion.identity, Vector3.one *
gameOverScale);
GUI.Label ( Rect( (Screen.width / (2 * gameOverScale)) - 350, (Screen.height / (2 *
gameOverScale)) - 40, 300, 100), "         YOU WON! \n Force Is Strong With You...", gameOverText);
}