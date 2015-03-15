using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public enum DType{
		COLOUR,
		FACE
	}

	public Image[] panels;

	public Sprite[] faces;
	public Color[] colours;

	private int turn_count = 0;
	private int current_dimension_image = 0;
	public Image dimension_image;
	public Dimension[] dimension_types;
	private DType curr_dim_type;
	public int turn_specifier = 10;


	public Text answer_field;
	public Text turn_counter; 
	public Text score_counter; 

	public int max_turns = 60;
	public int correct_ans = 0;
	private bool game_complete = false;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < panels.Length; i++) {
			panels[i].sprite = faces[Random.Range (0, faces.Length)];
			panels[i].color = colours[Random.Range (0, colours.Length)];
		}
		
		turn_counter.text = "" + (turn_count + 1) + "/" + max_turns;

		dimension_image.sprite = dimension_types [current_dimension_image].dimension_image;
		curr_dim_type  = dimension_types [current_dimension_image].dimension_type;

		score_counter.text = "" + correct_ans + "/" + max_turns;


	}
	
	// Update is called once per frame
	void Update () {
		bool question_answered = false;
		bool correct = true;
		// NO - To are they the same
		if(turn_count < max_turns && !game_complete){
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				// These are all the same and essentially checks what the type is
				// in this instance if the type is colour find the colour of the first panel
				// then check all of the rest of the panels against that and see if there is one
				// that is the same... if there is one then tell the game this and continue
				if(curr_dim_type == DType.COLOUR){
					Color panel_colour = panels[0].color;
					for(int i = 1; i < panels.Length; i++){
						if(panels[i].color == panel_colour){
							correct = false;
							break;
						}
					}
				}
				else{
					Sprite panel_image = panels[0].sprite;
					for(int i = 1; i < panels.Length; i++){
						if(panels[i].sprite == panel_image){
							correct = false;
							break;
						}
					}
				}
				question_answered = true;
			}
			// YES - To are they the same
			else if (Input.GetKeyDown (KeyCode.RightArrow)) {
				if(curr_dim_type == DType.COLOUR){
					Color panel_colour = panels[0].color;
					for(int i = 1; i < panels.Length; i++){
						if(panels[i].color != panel_colour){
							correct = false;
							break;
						}
					}
				}
				else{
					Sprite panel_image = panels[0].sprite;
					for(int i = 1; i < panels.Length; i++){
						if(panels[i].sprite != panel_image){
							correct = false;
							break;
						}
					}
				}
				question_answered = true;
			}


			// if the question has been answered
			if (question_answered == true){
				if(correct){
					answer_field.text = "Correct";
					correct_ans++;
					score_counter.text = "" + correct_ans + "/" + max_turns;
				}
				else{
					answer_field.text = "Incorrect";
				}
				moveToNextTurn ();
				moveToNextDimension();
			}
		}
		// should execute in same frame to compensate for last turn change
		if(turn_count >= max_turns){
			turn_count = max_turns;
			turn_counter.text = "" + turn_count + "/" + max_turns;
			game_complete = true;
		}
	}

	// randomizes the images and colours, and counts turns
	void moveToNextTurn(){
		for (int i = 0; i < panels.Length; i++) {
			panels[i].sprite = faces[Random.Range (0, faces.Length)];
			panels[i].color = colours[Random.Range (0, colours.Length)];
		}
		turn_count++;
		turn_counter.text = "" + (turn_count + 1) + "/" + max_turns;
	}

	// In the most basic terms flips from rainbow to smiley face after a certain count of turns
	void moveToNextDimension(){
		if(turn_count % turn_specifier == 0){
			current_dimension_image ++;
			if(current_dimension_image >= dimension_types.Length){
				current_dimension_image = 0;
			}
			dimension_image.sprite = dimension_types [current_dimension_image].dimension_image;
			curr_dim_type = dimension_types[current_dimension_image].dimension_type;
		}
	}
}

[System.Serializable]
public class Dimension{
	public GameController.DType dimension_type;
	public Sprite dimension_image;
}
