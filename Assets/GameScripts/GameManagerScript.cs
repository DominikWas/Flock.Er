using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace flockingBehaviour
{
	public class GameManagerScript : MonoBehaviour
	{
		enum UIGameState {StartMenu, PanelClosed, PanelOpen}
		private UIGameState uiState;

		public Camera firstPersonCamera;
		public Camera startCamera;
		public GameObject fpsController;
		public GameObject canvas;
		public Dictionary<GameObject, GameObject> uiItems; // text, item dictionary
		public GameObject uiItemPointer;
		
		// Start is called before the first frame update
		void Start()
		{
			uiItems = new Dictionary<GameObject, GameObject>(3);
			GameObject parent = GameObject.Find("panelOpen");
			
			// Filling the dictionary of options/behaviours that can be cycled through
			uiItems.Add(parent.transform.Find("text_cohesion").gameObject, parent.transform.Find("slider_cohesion").gameObject);
			uiItems.Add(parent.transform.Find("text_alignment").gameObject, parent.transform.Find("slider_alignment").gameObject);
			uiItems.Add(parent.transform.Find("text_avoidance").gameObject, parent.transform.Find("slider_avoidance").gameObject);
			
			uiItemPointer = new GameObject("uiEmptyItem"); // defaults to an empty item - no selection (stops null error)

			firstPersonCamera = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>(); // get fps cam
			firstPersonCamera.enabled = false; // disable it
			fpsController = GameObject.Find("FPSController"); // get fps controller
			fpsController.SetActive(false); // turn off first-person shooter functions on start screen
			startCamera.enabled = true; // enable the starting screen cam
			
			canvas = GameObject.Find("Canvas");
			uiState = UIGameState.StartMenu;
			
			// UI default
			canvas.transform.Find("flockingPanelOpen").gameObject.SetActive(false); // turn off open panel
			canvas.transform.Find("flockingPanelClosed").gameObject.SetActive(false); // turn off closed panel
			canvas.transform.Find("startMenuView").gameObject.SetActive(true); // turn on start menu panel
			//
		
		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.X))
			{
				if (uiState == UIGameState.PanelClosed && uiState != UIGameState.StartMenu)
				{
					canvas.transform.Find("flockingPanelOpen").gameObject.SetActive(true);
					canvas.transform.Find("flockingPanelClosed").gameObject.SetActive(false); // menu adjustments
					uiState = UIGameState.PanelOpen;
				}
				else if (uiState == UIGameState.PanelOpen && uiState != UIGameState.StartMenu)
				{
					canvas.transform.Find("flockingPanelOpen").gameObject.SetActive(false);
					canvas.transform.Find("flockingPanelClosed").gameObject.SetActive(true); // menu adjustments
					uiState = UIGameState.PanelClosed;				
				}
			}
			
			// Go down in menu items
			else if (Input.GetKeyDown(KeyCode.R) && uiState == UIGameState.PanelOpen)
			{
				if (uiItemPointer.name == "uiEmptyItem")
				{
					uiItemPointer = uiItems[GameObject.Find("text_cohesion")]; // select slider
				
					GameObject.Find("text_cohesion").GetComponent<Text>().color = Color.red;
				}
				else if (uiItemPointer.name == "slider_cohesion") 
				{
					GameObject.Find("text_cohesion").GetComponent<Text>().color = Color.white;
				
					uiItemPointer = uiItems[GameObject.Find("text_alignment")];
					GameObject.Find("text_alignment").GetComponent<Text>().color = Color.red;
				}
				else if (uiItemPointer.name == "slider_alignment")
				{
					GameObject.Find("text_alignment").GetComponent<Text>().color = Color.white;
					
					uiItemPointer = uiItems[GameObject.Find("text_avoidance")];
					GameObject.Find("text_avoidance").GetComponent<Text>().color = Color.red;				
				}
				else if (uiItemPointer.name == "slider_avoidance")
				{
					GameObject.Find("text_avoidance").GetComponent<Text>().color = Color.white;
				
					uiItemPointer = GameObject.Find("uiEmptyItem");			
				}			
			}
			
			// Go up in menu items
			else if (Input.GetKeyDown(KeyCode.E) && uiState == UIGameState.PanelOpen)
			{
				if (uiItemPointer.name == "uiEmptyItem")
				{
					uiItemPointer = uiItems[GameObject.Find("text_avoidance")]; // select slider
					GameObject.Find("text_avoidance").GetComponent<Text>().color = Color.red;		
				}
				else if (uiItemPointer.name == "slider_cohesion") 
				{
					GameObject.Find("text_cohesion").GetComponent<Text>().color = Color.white;
					uiItemPointer = GameObject.Find("uiEmptyItem");	
				}
				else if (uiItemPointer.name == "slider_alignment")
				{
					GameObject.Find("text_alignment").GetComponent<Text>().color = Color.white;
					
					uiItemPointer = uiItems[GameObject.Find("text_cohesion")];
					GameObject.Find("text_cohesion").GetComponent<Text>().color = Color.red;				
				}
				else if (uiItemPointer.name == "slider_avoidance")
				{
					GameObject.Find("text_avoidance").GetComponent<Text>().color = Color.white;
				
					uiItemPointer = uiItems[GameObject.Find("text_alignment")];
					GameObject.Find("text_alignment").GetComponent<Text>().color = Color.red;		
				}
			}
			
			// Increase menu item weight
			else if (Input.GetKey(KeyCode.LeftShift) && uiState == UIGameState.PanelOpen)
			{
				if (uiItemPointer.name.Contains("slider"))
				{
					float val = uiItemPointer.GetComponent<Slider>().value;
					uiItemPointer.GetComponent<Slider>().value = val + 0.2f;
				}	
			}
			// Decrease menu item weight
			else if (Input.GetKey(KeyCode.LeftControl) && uiState == UIGameState.PanelOpen)
			{
				if (uiItemPointer.name.Contains("slider"))
				{
					float val = uiItemPointer.GetComponent<Slider>().value;
					uiItemPointer.GetComponent<Slider>().value = val - 0.2f;
				}	
			}

			// Quit simulation
			else if (Input.GetKeyDown(KeyCode.Q))
			{	
				#if UNITY_EDITOR
					UnityEditor.EditorApplication.isPlaying = false;
				#else
					Application.Quit();
				#endif
			}
		}
		
		// On pressing the play button - main menu
		public void ButtonPlay()
		{
			startCamera.enabled = false;
			firstPersonCamera.enabled = true; // camera changes
			fpsController.SetActive(true); // activate fps functions
			
			uiState = UIGameState.PanelClosed;
			
			canvas.transform.Find("startMenuView").gameObject.SetActive(false);
			canvas.transform.Find("flockingPanelOpen").gameObject.SetActive(false);
			canvas.transform.Find("flockingPanelClosed").gameObject.SetActive(true); // menu adjustments
		}
		
		// On pressing the quit button - main menu
		public void ButtonQuit()
		{
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#else
				Application.Quit();
			#endif
		}
	}
}