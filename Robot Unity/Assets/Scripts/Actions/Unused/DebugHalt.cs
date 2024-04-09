using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions {

	public class DebugHalt : ActionTask {

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			//EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}
	}
}