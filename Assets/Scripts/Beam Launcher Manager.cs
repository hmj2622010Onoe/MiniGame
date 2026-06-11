using System.Drawing;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

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

			beamY1 = Random.Range(-1, 2);
			beamY2 = Random.Range(-1, 2);
			while (beamY1 == beamY2) beamY2 = Random.Range(-1, 2);

			GameObject launcherDown = Instantiate(launcherPrefab);
			launcherDown.transform.position = new Vector2(beamX1 * 2, -3.5f);
			launcherDown.transform.rotation = Quaternion.Euler(0, 0, 0);

			GameObject launcherUp = Instantiate(launcherPrefab);
			launcherUp.transform.position = new Vector2(beamX2 * 2, 3.5f);
			launcherUp.transform.rotation = Quaternion.Euler(0, 0, 180);

			GameObject launcherRight = Instantiate(launcherPrefab);
			launcherRight.transform.position = new Vector2(3.5f, beamY1 * 2);
			launcherRight.transform.rotation = Quaternion.Euler(0, 0, 90);

			GameObject launcherLeft = Instantiate(launcherPrefab);
			launcherLeft.transform.position = new Vector2(-3.5f, beamY2 * 2);
			launcherLeft.transform.rotation = Quaternion.Euler(0, 0, -90);

			state = State.Beam;
		}


		if (state == State.Beam)
		{
			delta += Time.deltaTime;
			if (delta > span)
			{
				GameObject beamDown = Instantiate(beamPrefab);
				beamDown.transform.position = new Vector2(beamX1 * 2, -3.5f);
				beamDown.transform.rotation = Quaternion.Euler(0, 0, 0);

				GameObject beamUp = Instantiate(beamPrefab);
				beamUp.transform.position = new Vector2(beamX2 * 2, 3.5f);
				beamUp.transform.rotation = Quaternion.Euler(0, 0, 180);

				GameObject beamRight = Instantiate(beamPrefab);
				beamRight.transform.position = new Vector2(3.5f, beamY1 * 2);
				beamRight.transform.rotation = Quaternion.Euler(0, 0, 90);

				GameObject beamLeft = Instantiate(beamPrefab);
				beamLeft.transform.position = new Vector2(-3.5f, beamY2 * 2);
				beamLeft.transform.rotation = Quaternion.Euler(0, 0, -90);

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
