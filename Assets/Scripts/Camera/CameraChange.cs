using UnityEngine;
using System.Collections;

public class CameraChange : MonoBehaviour
	
{
	
	public Camera maincamera;
	
	public Camera frontcamera;
	
	public Camera downcamera;
	
	
	
	private Camera[] cameras;
	
	private int currentCameraIndex = 0;
	
	private Camera currentCamera;
	
	public Cockpit cockpitActivate; 

	
	void Start()
		
	{
		
		cameras = new Camera[] { maincamera, frontcamera, downcamera };
		
		currentCamera = maincamera;
		
		ChangeView();
		
	}
	
	
	
	void Update()
		
	{
		
		if(Input.GetKeyDown("c"))
			
		{ 
			
			currentCameraIndex++;
			if (currentCameraIndex ==2) cockpitActivate.showCockpit = true;
			else cockpitActivate.showCockpit = false;
			if (currentCameraIndex > 2)
				
				currentCameraIndex = 0;
			
			ChangeView();
			
		}
		
	}
	
	
	
	void ChangeView()
		
	{
		
		currentCamera.enabled = false;
		
		currentCamera = cameras[currentCameraIndex];
		
		currentCamera.enabled = true;
		
	}
	
}
