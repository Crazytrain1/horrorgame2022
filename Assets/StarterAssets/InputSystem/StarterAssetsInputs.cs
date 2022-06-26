using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		public ThirdPersonController player;
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public Vector2 crawl;
		public bool rotate;
		public bool jump;
		public bool sprint;
		public bool Interact;
		public bool Flashlight;
		public GameObject light;
		[Header("Movement Settings")]
		public bool analogMovement;
		

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		
		public void HandleAllInputs()
        {
			HandleInteractionInput();
        }


#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnCrawling(InputValue value)
		{
			CrawlingInput(value.Get<Vector2>());
		}

		public void OnRotate(InputValue value)
        {
			RotateInput(value.isPressed);
        }

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif
		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}

		public void OnFlashlight(InputValue value)
		{
			if (Flashlight == true)
			{
				FlashlightInput(!value.isPressed);
				light.SetActive(false);

			}
			else
			{
				FlashlightInput(value.isPressed);
				light.SetActive(true);
			}
		}
		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void CrawlingInput(Vector2 newCrawlingDirection)
        {
			crawl = newCrawlingDirection;
			Debug.Log("OnCrawling working");
        }
		public void RotateInput(bool newRotateDirection)
        {
			rotate = newRotateDirection;
        }

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}
		public void InteractInput(bool newInteractState)
        {
			Interact = newInteractState;
        }
		public void FlashlightInput(bool newFlashlightState)
		{
			Flashlight = newFlashlightState;
		}
		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			//SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
		private void HandleInteractionInput()
        {
			if (Interact)
            {
				if(!player.canInteract)
                {
					Interact = false;
                }
            }
        }
		
	}
	
}