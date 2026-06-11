using System.Drawing;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.WSA;

public class BeamlauncherManager : MonoBehaviour
{
	[SerializeField] GameObject launcherPrefab;
	[SerializeField] GameObject beamPrefab;
	[SerializeField] GameObject scoreText;

	[SerializeField]float span = 3.0f;
	[SerializeField]float endSpan = 0.5f;
	float delta = 0;

	int score = 0;
	bool scoreChance = true;
	bool scoreDamage = true;

	int beamX1;
	int beamX2;
	int beamY1;
	int beamY2;

	int typeX1;
	int typeX2;
	int typeY1;
	int typeY2;
	
	enum State { Launcher, Beam, End}; 
	State state = State.Launcher;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
	}

	public void GetDamage()
	{
		if (score > 0) score--;
		scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
		scoreChance = false;
	}
	// Update is called once per frame
	void Update()
	{
		//if (gameScene == "launcher")
		if(state == State.Launcher)
		{

			// launcher‚ÌÀ•W‚ðÝ’è
			beamX1 = Random.Range(-1, 2);   // ˆêŒÂ–Ú‚ðÝ’è
			beamX2 = Random.Range(-1, 2);
			while (beamX1 == beamX2) beamX2 = Random.Range(-1, 2);  // “ñŒÂ–Ú‚ªˆêŒÂ–Ú‚Æ”í‚Á‚Ä‚¢‚½ê‡”í‚ç‚È‚¢‚Ü‚Å•ÏX

			typeX1 = Random.Range( 0, 2);
			typeX2 = Random.Range( 0, 2);

			beamY1 = Random.Range(-1, 2);
			beamY2 = Random.Range(-1, 2);
			while (beamY1 == beamY2) beamY2 = Random.Range(-1, 2);

			typeY1 = Random.Range( 0, 2);
			typeY2 = Random.Range( 0, 2);

			GameObject launcherX1 = Instantiate(launcherPrefab);
			if (typeX1 == 0)
			{
				launcherX1.transform.position = new Vector2(beamX1 * 2, -3.5f);
				launcherX1.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			else if (typeX1 == 1) 
			{
				launcherX1.transform.position = new Vector2(beamX1 * 2, 3.5f);
				launcherX1.transform.rotation = Quaternion.Euler(0, 0, 180);
			}

			GameObject launcherX2 = Instantiate(launcherPrefab);
			if (typeX2 == 0)
			{
				launcherX2.transform.position = new Vector2(beamX2 * 2, -3.5f);
				launcherX2.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			else if(typeX2 == 1) 
			{
				launcherX2.transform.position = new Vector2(beamX2 * 2, 3.5f);
				launcherX2.transform.rotation = Quaternion.Euler(0, 0, 180);
			}

			GameObject launcherY1 = Instantiate(launcherPrefab);
			if (typeY1 == 0)
			{
				launcherY1.transform.position = new Vector2(-3.5f, beamY1 * 2);
				launcherY1.transform.rotation = Quaternion.Euler(0, 0, -90);
			}
			else if (typeY1 == 1)
			{
				launcherY1.transform.position = new Vector2(3.5f, beamY1 * 2);
				launcherY1.transform.rotation = Quaternion.Euler(0, 0, 90);
			}

			GameObject launcherY2 = Instantiate(launcherPrefab);
			if (typeY2 == 0)
			{
				launcherY2.transform.position = new Vector2(-3.5f, beamY2 * 2);
				launcherY2.transform.rotation = Quaternion.Euler(0, 0, -90);
			}
			else if (typeY2 == 1)
			{
				launcherY2.transform.position = new Vector2(3.5f, beamY2 * 2);
				launcherY2.transform.rotation = Quaternion.Euler(0, 0, 90);
			}
			
			state = State.Beam;
		}


		if (state == State.Beam)
		{
			delta += Time.deltaTime;
			if (delta > span)
			{
				GameObject beam_X1 = Instantiate(beamPrefab);
				if (typeX1 == 0)
				{
					beam_X1.transform.position = new Vector2(beamX1 * 2, -3.5f);
					beam_X1.transform.rotation = Quaternion.Euler(0, 0, 0);
				}
				else if (typeX1 == 1)
				{
					beam_X1.transform.position = new Vector2(beamX1 * 2, 3.5f);
					beam_X1.transform.rotation = Quaternion.Euler(0, 0, 180);
				}

				GameObject beam_X2 = Instantiate(beamPrefab);
				if (typeX2 == 0)
				{
					beam_X2.transform.position = new Vector2(beamX2 * 2, -3.5f);
					beam_X2.transform.rotation = Quaternion.Euler(0, 0, 0);
				}
				else if (typeX2 == 1)
				{
					beam_X2.transform.position = new Vector2(beamX2 * 2, 3.5f);
					beam_X2.transform.rotation = Quaternion.Euler(0, 0, 180);
				}


				GameObject beam_Y1 = Instantiate(beamPrefab);
				if (typeY1 == 0) 
				{
					beam_Y1.transform.position = new Vector2(-3.5f, beamY1 * 2);
					beam_Y1.transform.rotation = Quaternion.Euler(0, 0, -90);
				}
				else if (typeY1 == 1) 
				{
					beam_Y1.transform.position = new Vector2(3.5f, beamY1 * 2);
					beam_Y1.transform.rotation = Quaternion.Euler(0, 0, 90);
				}

				GameObject beam_Y2 = Instantiate(beamPrefab);
				if (typeY2 == 0)
				{
					beam_Y2.transform.position = new Vector2(-3.5f, beamY2 * 2);
					beam_Y2.transform.rotation = Quaternion.Euler(0, 0, -90);
				}
				else if (typeY2 == 1)
				{
					beam_Y2.transform.position = new Vector2(3.5f, beamY2 * 2);
					beam_Y2.transform.rotation = Quaternion.Euler(0, 0, 90);
				}

					delta = 0;
				state = State.End;
			}
		}

		if (state == State.End)
		{
			delta += Time.deltaTime;
			if (delta > endSpan)
			{
				if (scoreChance == true) 
				{
				score += 100;
				scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
				scoreChance = false;
				}
				GameObject[] prefabs = GameObject.FindGameObjectsWithTag("DestroyDevice");
				foreach (GameObject obj in prefabs)
				{
					Destroy(obj);
				}
				scoreChance = true;
				state = State.Launcher;
			}
		}

	}
}
