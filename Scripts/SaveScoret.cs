using Godot;
using System;

public class SaveScoret : Node2D
{
	public int HighScore = 0;
	public int DeadCount = 0;
	public const String SavePath = "user://save-file.cfg";
	ConfigFile Config = new ConfigFile();
	
	public override void _Ready()
	{
		Config.Load(SavePath);
	}
	
	public void SaveValue(String key, int val){
		
		Config.SetValue("SaveList", key, val);
		Config.Save(SavePath);
	}
	
	public void LoadValue(String key){
		var temp = (int)Config.GetValue("SaveList", key, HighScore);
		if(key.Equals("HighScore")){
			HighScore = temp;
		}
		else{
			DeadCount = temp;
		}
	}

	
}
