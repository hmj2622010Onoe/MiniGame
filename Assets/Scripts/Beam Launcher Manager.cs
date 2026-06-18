using System;
using System.Drawing;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.WSA;

public class BeamlauncherManager : MonoBehaviour
{
	[SerializeField] GameObject launcherPrefab;
	[SerializeField] GameObject beamPrefab;

	[SerializeField] GameObject endScreen;

	[SerializeField] GameObject startText;
	[SerializeField] GameObject scoreText;
	[SerializeField] GameObject shellScoreText;
	[SerializeField] GameObject stageText;
	[SerializeField] GameObject levelText;
	[SerializeField] GameObject comboText;

	[SerializeField] GameObject resultsText;
	[SerializeField] GameObject rScoreText;
	[SerializeField] GameObject rComboText;
	[SerializeField] GameObject rClearText;
	[SerializeField] GameObject rMissText;
	[SerializeField] GameObject reStartText;

	[SerializeField] AudioClip countSE;
	[SerializeField] AudioClip beamSE;
	[SerializeField] AudioClip damageSE;
	[SerializeField] AudioClip startSE;
	AudioSource aud;
	int seCounter = 1;

	[SerializeField] float spanDefault = 4.0f;
	[SerializeField] float spanFirstChange = 3.0f;
	[SerializeField] float spanSecondChange = 2.0f;
	[SerializeField] float spanThirdChange = 1.5f;
	float span;


	[SerializeField] float endSpan = 0.5f;
	float delta = 0;

	[SerializeField] int finishLine = 25;
	[SerializeField] int thirdLine = 15;
	[SerializeField] int secondLine = 8;
	[SerializeField] int firstLine = 3;
	int nowStage = 0;

	int clear = 0;
	int miss = 0;
	int combo = 0;
	int maxCombo = 0;
	int score = 0;
	bool scoreChance = true;

	int level = 1;

	int beamX1;
	int beamX2;
	int beamY1;
	int beamY2;

	int typeX1;
	int typeX2;
	int typeY1;
	int typeY2;

	enum Game { start, play, over };
	Game game = Game.start;
	enum State { Launcher, Beam, End };
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
		AudioSource.PlayClipAtPoint(damageSE, transform.position);
		scoreChance = false;
	}
	// Update is called once per frame
	void Update()
	{
		if (game == Game.start)
		{
			endScreen.SetActive(false);
			startText.SetActive(true);

			stageText.SetActive(false);
			scoreText.SetActive(false);
			shellScoreText.SetActive(false);
			levelText.SetActive(false);
			comboText.SetActive(false);
			if (Keyboard.current.rKey.wasPressedThisFrame)
			{
				AudioSource.PlayClipAtPoint(startSE, transform.position);
				startText.SetActive(false);

				stageText.SetActive(true);
				scoreText.SetActive(true);
				shellScoreText.SetActive(true);
				levelText.SetActive(true);
				game = Game.play;
			}
		}

		if (game == Game.play)
		{
			stageText.GetComponent<TextMeshProUGUI>().text = nowStage + "/" + finishLine.ToString();
			shellScoreText.GetComponent<TextMeshProUGUI>().text = "SCORE".ToString();
			scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
			levelText.GetComponent<TextMeshProUGUI>().text = "Level" + level.ToString();
			if (combo > 1) { comboText.SetActive(true); comboText.GetComponent<TextMeshProUGUI>().text = combo + "combo!!".ToString(); }
			else comboText.SetActive(false);

			//if (gameScene == "launcher")
			if (state == State.Launcher)
			{

				// launcherÇÃç¿ïWÇê›íË
				beamX1 = UnityEngine.Random.Range(-1, 2);   // àÍå¬ñ⁄Çê›íË
				beamX2 = UnityEngine.Random.Range(-1, 2);
				while (beamX1 == beamX2) beamX2 = UnityEngine.Random.Range(-1, 2);  // ìÒå¬ñ⁄Ç™àÍå¬ñ⁄Ç∆îÌÇ¡ÇƒÇ¢ÇΩèÍçáîÌÇÁÇ»Ç¢Ç‹Ç≈ïœçX

				typeX1 = UnityEngine.Random.Range(0, 2);
				typeX2 = UnityEngine.Random.Range(0, 2);

				beamY1 = UnityEngine.Random.Range(-1, 2);
				beamY2 = UnityEngine.Random.Range(-1, 2);
				while (beamY1 == beamY2) beamY2 = UnityEngine.Random.Range(-1, 2);

				typeY1 = UnityEngine.Random.Range(0, 2);
				typeY2 = UnityEngine.Random.Range(0, 2);

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
				else if (typeX2 == 1)
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
				if (nowStage < firstLine) { span = spanDefault; level = 1; }
				if (nowStage >= firstLine && nowStage < secondLine) { span = spanFirstChange; level = 2; }
				if (nowStage >= secondLine && nowStage < thirdLine) { span = spanSecondChange; level = 3; }
				if (nowStage >= thirdLine && nowStage < finishLine) { span = spanThirdChange; level = 4; }
				levelText.GetComponent<TextMeshProUGUI>().text = "Level" + level.ToString();
				delta += Time.deltaTime;
				if (delta >= (span / 4) * seCounter && seCounter < 4)
				{
					AudioSource.PlayClipAtPoint(countSE, transform.position);
					seCounter += 1;
				}
				if (delta > span)
				{
					AudioSource.PlayClipAtPoint(beamSE, transform.position);

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
						clear++;
						if (combo > 5) score += 50;
						if (combo > 10) score += 150;
						combo++;
					}
					else
					{
						miss++;
						if (maxCombo < combo) maxCombo = combo;
						combo = 0;
					}
					GameObject[] prefabs = GameObject.FindGameObjectsWithTag("DestroyDevice");
					foreach (GameObject obj in prefabs)
					{
						Destroy(obj);
					}
					seCounter = 1;
					scoreChance = true;
					nowStage += 1;
					stageText.GetComponent<TextMeshProUGUI>().text = nowStage + "/" + finishLine.ToString();
					if (nowStage >= finishLine) game = Game.over;
					state = State.Launcher;
				}
			}
		}

		if (game == Game.over)
		{
			stageText.SetActive(false);
			scoreText.SetActive(false);
			shellScoreText.SetActive(false);
			levelText.SetActive(false);
			comboText.SetActive(false);

			resultsText.SetActive(true);
			if (maxCombo < combo) maxCombo = combo;
			endScreen.SetActive(true);
			delta += Time.deltaTime;
			span = spanDefault;
			if (delta >= (span / 4) * seCounter && seCounter < 4)
			{
				if (seCounter == 1) { rClearText.SetActive(true); rMissText.SetActive(true); }
				if (seCounter == 2) rComboText.SetActive(true);
				if (seCounter == 3) rScoreText.SetActive(true);
				rClearText.GetComponent<TextMeshProUGUI>().text = "Clear:" + clear.ToString();
				rMissText.GetComponent<TextMeshProUGUI>().text = "Miss:" + miss.ToString();
				rComboText.GetComponent<TextMeshProUGUI>().text = "Combo:" + maxCombo.ToString();
				rScoreText.GetComponent<TextMeshProUGUI>().text = "Score:" + score.ToString();
				AudioSource.PlayClipAtPoint(countSE, transform.position);
				seCounter += 1;
			}
			if (delta > span)
			{
				reStartText.SetActive(true);
				if (seCounter == 4) { AudioSource.PlayClipAtPoint(beamSE, transform.position); seCounter++; }
				if (Keyboard.current.rKey.wasPressedThisFrame)
				{
					AudioSource.PlayClipAtPoint(startSE, transform.position);

					endScreen.SetActive(false);
					resultsText.SetActive(false);
					rClearText.SetActive(false);
					rMissText.SetActive(false);
					rComboText.SetActive(false);
					rScoreText.SetActive(false);
					reStartText.SetActive(false);

					stageText.SetActive(true);
					scoreText.SetActive(true);
					shellScoreText.SetActive(true);
					levelText.SetActive(true);

					nowStage = 0;
					clear = 0;
					miss = 0;
					combo = 0;
					maxCombo = 0;
					score = 0;
					scoreChance = true;
					seCounter = 1;

					level = 1;

					delta = 0;
					state = State.Launcher;
					game = Game.play;
				}
			}

		}
	}
}
