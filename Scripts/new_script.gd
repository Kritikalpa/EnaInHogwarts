extends Node




func _ready():
	pass





func _on_TextureButton_pressed():
	get_tree().change_scene("res://Scenes/StoryLine.tscn")


func _on_TextureButton2_pressed():
	get_tree().quit()
