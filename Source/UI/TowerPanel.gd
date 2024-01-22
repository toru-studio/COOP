extends Panel

var currImageCopy 
var towerScene
var cost

func _on_ready():
	var towerName = get_child(0).name
	towerScene = load("res://Source/Entities/Towers/"+towerName+".tscn")
	cost = get_child(3).get_child(0).text;
	
func _on_gui_input(event):
	if (get_tree().current_scene.GetCurrency() < int(cost)):
		return
	if event is InputEventMouseButton and event.button_mask == 1:
		currImageCopy = get_child(0).duplicate()
		currImageCopy.global_position = event.global_position
		get_tree().current_scene.add_child(currImageCopy)
	elif event is InputEventMouseMotion and event.button_mask == 1:
		#Dragging
		currImageCopy.global_position = event.global_position

	elif event is InputEventMouseButton and event.button_mask == 0:
		get_tree().current_scene.RemoveCurrency(cost)
		var tower = towerScene.instantiate()
		get_tree().current_scene.add_child(tower)
		tower.global_position = event.global_position
		currImageCopy.queue_free()
	

#func _on_gui_input(event):
#	print(get_child(0).name)
#	tempTower = flameTowerScene.instantiate()
#	if event is InputEventMouseButton and event.button_mask == 1:
#		currImageCopy = get_child(0).duplicate()
#		currImageCopy.global_position = event.global_position
#		get_tree().current_scene.add_child(currImageCopy)

