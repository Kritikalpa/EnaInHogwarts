extends Control


func _ready():
	pass 


func _process(delta):
	pass

func _input(event):
	if(event.is_action_pressed("ui_cancel")):
		get_tree().paused = not get_tree().paused
		visible = not visible


func _on_TextureButton_pressed():
	get_tree().paused = not get_tree().paused
	visible = not visible


func _on_TextureButton2_pressed():
	get_tree().change_scene("res://Scenes/TitleNew.tscn")
	get_tree().paused = not get_tree().paused
