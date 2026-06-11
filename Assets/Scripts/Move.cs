using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Move : MonoBehaviour
{
	[SerializeField] GameObject manager;

	float moveSpeed = 15/100f;	// 動くスピード
	float moveRange = 2;	// 動ける範囲

	float x = 0;
	float y = 0;


	// ボタンが押されているか確認用
	bool pressD = false;
	bool pressA = false;
	bool pressW = false;
	bool pressS = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		Application.targetFrameRate = 60;
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		Debug.Log("ビームに当たった");
		manager.GetComponent<BeamlauncherManager >().GetDamage();
		
	}
	

	// Update is called once per frame
	void Update()
	{
		// 移動
		if (Keyboard.current.dKey.IsPressed()) 
		{ 
			x += moveSpeed;
			pressD = true;　// キーが押されたことを記録
		}
		if (Keyboard.current.dKey.wasReleasedThisFrame) pressD = false;　// キーが押されていないことを記録

		if (Keyboard.current.aKey.IsPressed()) 
		{
			x += -moveSpeed; 
			pressA = true;
		}
		if (Keyboard.current.aKey.wasReleasedThisFrame) pressA = false;

		if (Keyboard.current.wKey.IsPressed()) 
		{
			y += moveSpeed;
			pressW = true;
		}
		if (Keyboard.current.wKey.wasReleasedThisFrame) pressW = false;

		if (Keyboard.current.sKey.IsPressed()) 
		{ 
			y += -moveSpeed; 
			pressS = true;
		}
		if (Keyboard.current.sKey.wasReleasedThisFrame) pressS = false;

		// 反対なキーが両方押されていたorどちらのキーも押されていなかった場合、中心へ戻す
		if ((pressD == true && pressA == true) || (pressD == false && pressA == false))  
		{
			if (x > 0)
			{
				if (moveSpeed * 2 > x) x = 0;　// 中心に近づいたまま終わらないように、近づけば強制的に中心へ
				else x += -moveSpeed;
			}
			if (x < 0)
			{
				if (-moveSpeed * 2 < x) x = 0;
				else x += moveSpeed;
			}
		}
		if ((pressW == true && pressS == true) || (pressW == false && pressS == false)) 
		{
			if (y > 0)
			{
				if (moveSpeed * 2 > y) y = 0;
				else y += -moveSpeed;
			}
			if (y < 0)
			{
				if (-moveSpeed * 2 < y) y = 0;
				else y += moveSpeed;
			}
		}

		// 範囲を越えていればそこで止める
		if (x > moveRange) x = moveRange;
		if (x < -moveRange) x = -moveRange;
		if (y > moveRange)  y = moveRange;
		if (y < -moveRange) y = -moveRange;

		transform.position = new Vector3(x, y, 0);	// 座標を更新する
	}
}
